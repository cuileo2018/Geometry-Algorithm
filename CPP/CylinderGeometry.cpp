//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================

#include "CylinderGeometry.h"
#include <cmath> // 添加这个头文件

#ifndef M_PI
#define M_PI 3.14159265358979323846 // 手动定义M_PI
#endif

namespace GeoLib {
    CylinderGeometry::CylinderGeometry(std::shared_ptr<PointModel> _centerPoint, std::shared_ptr<AngleModel> _rotateAngle, std::shared_ptr <CylinderSize> _size)
        : BaseGeometry(_centerPoint, _rotateAngle, "Cylinder") {
        this->_size = _size;
    }

    void CylinderGeometry::CreateCylinderKeyPoint(float unit) {
        float XC = constHelper::POSNUM;
        float CXC = constHelper::POSNUM * -1;
        float OB = constHelper::OBANGLE;
        float ZC = constHelper::ZERO;
        double radinAngle = M_PI / constHelper::FLATANGLE;

        BaseGeometry::CreateKeyPoint();

        if (unit >= 0) {
            for (int i = 0; i < 8; i++) {
                double xCoorDinate = XC * cos(i * OB * radinAngle);
                double yCoorDinate = XC * sin(i * OB * radinAngle);
                _keyList.push_back(PointGeometry(PointModel(xCoorDinate, yCoorDinate, XC))); // 上底面边缘点
                _keyList.push_back(PointGeometry(PointModel(xCoorDinate, yCoorDinate, CXC))); // 下底面边缘点
            }

            if (unit > 0) {
                CutCircleEdgePoint(unit);
                CutBottomSurfaceArea(unit);
                CutSideSurfaceArea(unit);
            }

            _keyList.push_back(PointGeometry(PointModel(ZC, ZC, XC))); // 上底面中心点
            _keyList.push_back(PointGeometry(PointModel(ZC, ZC, CXC))); // 下底面中心点

        }
    }

    void CylinderGeometry::CutCircleEdgePoint(float unit) {
        float OB = constHelper::OBANGLE;
        float FL = constHelper::FLATANGLE;
        if (unit < static_cast<float>(M_PI) * static_cast<float>(_size->getR()) * static_cast<float>(OB) / static_cast<float>(FL)) cutEdge(static_cast<float>(unit), static_cast<float>(_size->getR()));
    }

    void CylinderGeometry::cutEdge(float unit, float r) {
        float OB = constHelper::OBANGLE;
        float FL = constHelper::FLATANGLE;
        float XC = constHelper::POSNUM;
        float CXC = constHelper::POSNUM * -1;
        double radinAngle = M_PI / constHelper::FLATANGLE;
        float relativeRadius = XC * r / static_cast<float>(_size->getR());

        int cutNum = ConvertLib::roundToInt(round((static_cast<float>(M_PI) * r * OB / FL) / unit) - 1);

        if (cutNum > 0) {
            float averageAngle = OB / (cutNum + 1);

            for (int i = 1; i <= cutNum; i++) {
                for (int j = 0; j < 8; j++) {
                    float x = static_cast<float>(relativeRadius * cos(static_cast<float>((averageAngle * i) + (j * OB)) * radinAngle));
                    float y = static_cast<float>(relativeRadius * sin(static_cast<float>(((averageAngle * i) + (j * OB)) * radinAngle)));
                    _keyList.push_back(PointGeometry(PointModel(x, y, XC)));
                    _keyList.push_back(PointGeometry(PointModel(x, y, CXC)));
                }
            }
        }
    }

    void CylinderGeometry::CutBottomSurfaceArea(float unit) {
        int cutRNum = ConvertLib::roundToInt(round(static_cast<float>(_size->getR()) / static_cast<float>(unit)) - 1);

        if (cutRNum > 0) {
            float averageR = static_cast<float>(_size->getR()) / (cutRNum + 1);
            for (int i = 1; i <= cutRNum; i++) {
                cutEdge(unit, i * averageR);
                cutCircleR(unit);
            }
        }
    }

    void CylinderGeometry::cutCircleR(float unit) {
        float OB = constHelper::OBANGLE;
        float XC = constHelper::POSNUM;
        float CXC = constHelper::POSNUM * -1;
        double radinAngle = M_PI / constHelper::FLATANGLE;
        int cutRNum = ConvertLib::roundToInt(round(static_cast<float>(_size->getR()) / unit) - 1);

        if (cutRNum > 0) {
            float averageR = XC / (cutRNum + 1);
            for (int i = 1; i <= cutRNum; i++) {
                for (int j = 0; j < 8; j++) {
                    float xCoordinate = static_cast<float>((i * averageR) * static_cast<float>(cos(j * OB * radinAngle)));
                    float yCoordinate = static_cast<float>((i * averageR) * static_cast<float>(sin(j * OB * radinAngle)));
                    _keyList.push_back(PointGeometry(PointModel(xCoordinate, yCoordinate, XC)));
                    _keyList.push_back(PointGeometry(PointModel(xCoordinate, yCoordinate, CXC)));
                }
            }
        }
    }

    void CylinderGeometry::CutSideSurfaceArea(float unit) {
        if (unit < _size->getH()) CutSideSurface(unit);
    }

    void CylinderGeometry::CutSideSurface(float unit) {
        float OB = constHelper::OBANGLE;
        float FL = constHelper::FLATANGLE;
        float XC = constHelper::POSNUM;
        float CXC = constHelper::POSNUM * -1;
        float ZC = constHelper::ZERO;
        float OBC = constHelper::OBLIQUE;
        float COBC = constHelper::OBLIQUE * -1;

        int cutHNum = ConvertLib::roundToInt(round(static_cast<float>(_size->getH()) / unit) - 1);

        if (cutHNum > 0) {
            float cutNum = static_cast<float>((M_PI * _size->getR() * OB / FL) / unit - 1);
            float averageH = 1.0f / (cutHNum + 1);

            for (int i = 1; i <= cutHNum; i++) {
                _keyList.push_back(PointGeometry(PointModel(XC, ZC, XC - (i * averageH))));
                _keyList.push_back(PointGeometry(PointModel(OBC, OBC, XC - (i * averageH))));
                _keyList.push_back(PointGeometry(PointModel(ZC, XC, XC - (i * averageH))));
                _keyList.push_back(PointGeometry(PointModel(COBC, OBC, XC - (i * averageH))));
                _keyList.push_back(PointGeometry(PointModel(CXC, ZC, XC - (i * averageH))));
                _keyList.push_back(PointGeometry(PointModel(COBC, COBC, XC - (i * averageH))));
                _keyList.push_back(PointGeometry(PointModel(ZC, CXC, XC - (i * averageH))));
                _keyList.push_back(PointGeometry(PointModel(OBC, COBC, XC - (i * averageH))));

            }

            if (cutNum >= 1) {
                float averageAngle = OB / (cutNum + 1);
                double radinAngle = M_PI / constHelper::FLATANGLE;

                for (int i = 1; i <= cutNum; i++) {
                    for (int j = 0; j < 8; j++) {
                        float x = static_cast<float>(XC * cos(static_cast<float>((averageAngle * i) + (j * OB)) * radinAngle));
                        float y = static_cast<float>(XC * sin(static_cast<float>(((averageAngle * i) + (j * OB)) * radinAngle)));
                        for (int k = 1; k <= cutHNum; k++) {
                            _keyList.push_back(PointGeometry(PointModel(x, y, XC - (k * averageH))));
                        }
                    }
                }
            }
        }
    }
}
