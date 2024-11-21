//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================

#include "CuboidGeometry.h"
#include <math.h>
namespace GeoLib {
    CuboidGeometry::CuboidGeometry(std::shared_ptr<PointModel> _centerPoint, std::shared_ptr<AngleModel> _rotateAngle, std::shared_ptr<CuboidSize> _size)
        : BaseGeometry(_centerPoint, _rotateAngle, "Cuboid") {
        this->_size = _size;
    }

    CuboidGeometry::CuboidGeometry(std::shared_ptr<PointModel> _centerPoint, std::shared_ptr<AngleModel> _rotateAngle)
        : BaseGeometry(_centerPoint, _rotateAngle, "Line") {
        this->_size = _size;
    }

    CuboidSize CuboidGeometry::getSize() const {
        return *_size;
    }

    void CuboidGeometry::setSize(const CuboidSize& size) {
        _size = std::shared_ptr<CuboidSize>(new CuboidSize(size));;
    }

    void CuboidGeometry::CreateCuboidKeyPoint(float unit) {
        float R = constHelper::POSNUM;
        float FR = constHelper::POSNUM * -1;

        BaseGeometry::CreateKeyPoint();
        _keyList.push_back(PointGeometry(PointModel(FR, FR, FR)));   // A
        _keyList.push_back(PointGeometry(PointModel(R, FR, FR)));    // B
        _keyList.push_back(PointGeometry(PointModel(FR, R, FR)));    // C
        _keyList.push_back(PointGeometry(PointModel(R, R, FR)));     // D
        _keyList.push_back(PointGeometry(PointModel(FR, FR, R)));    // A'
        _keyList.push_back(PointGeometry(PointModel(R, FR, R)));     // B'
        _keyList.push_back(PointGeometry(PointModel(FR, R, R)));     // C'
        _keyList.push_back(PointGeometry(PointModel(R, R, R)));      // D'

        // 分割边缘点
        CutSideLength(unit);
        // 分割表面积
        CutSurfaceArea(unit);
    }

    void CuboidGeometry::CutSideLength(float unit) {
        if (unit < _size->getL()) cutX(unit);
        if (unit < _size->getW()) cutY(unit);
        if (unit < _size->getH()) cutZ(unit);
    }

    void CuboidGeometry::cutX(float unit) {
        float R = constHelper::POSNUM;
        float FR = constHelper::POSNUM * -1;

        int forCn = ConvertLib::roundToInt(round(_size->getL() / unit) - 1);
        float step = (R - FR) / (forCn + 1);
        for (int ix = 1; ix <= forCn; ix++) {
            float x = FR + (float)(ix * step);
            _keyList.push_back(PointGeometry(PointModel(ConvertLib::GetFloat(std::to_string(x)), FR, FR)));
            _keyList.push_back(PointGeometry(PointModel(ConvertLib::GetFloat(std::to_string(x)), R, FR)));
            _keyList.push_back(PointGeometry(PointModel(ConvertLib::GetFloat(std::to_string(x)), FR, R)));
            _keyList.push_back(PointGeometry(PointModel(ConvertLib::GetFloat(std::to_string(x)), R, R)));

        }
    }

    void CuboidGeometry::cutY(float unit) {
        float R = constHelper::POSNUM;
        float FR = constHelper::POSNUM * -1;

        int forCn = ConvertLib::roundToInt(round(_size->getW() / unit) - 1);
        float step = (R - FR) / (forCn + 1);
        for (int iy = 1; iy <= forCn; iy++) {
            float y = FR + (float)(iy * step);
            _keyList.push_back(PointGeometry(PointModel(FR, ConvertLib::GetFloat(std::to_string(y)), FR)));
            _keyList.push_back(PointGeometry(PointModel(R, ConvertLib::GetFloat(std::to_string(y)), FR)));
            _keyList.push_back(PointGeometry(PointModel(FR, ConvertLib::GetFloat(std::to_string(y)), R)));
            _keyList.push_back(PointGeometry(PointModel(R, ConvertLib::GetFloat(std::to_string(y)), R)));


        }
    }

    void CuboidGeometry::cutZ(float unit) {
        float R = constHelper::POSNUM;
        float FR = constHelper::POSNUM * -1;

        int forCn = ConvertLib::roundToInt(round(_size->getH() / unit) - 1);
        float step = (R - FR) / (forCn + 1);
        for (int iz = 1; iz <= forCn; iz++) {
            float z = FR + (float)(iz * step);
            _keyList.push_back(PointGeometry(PointModel(FR, FR, ConvertLib::GetFloat(std::to_string(z)))));
            _keyList.push_back(PointGeometry(PointModel(R, FR, ConvertLib::GetFloat(std::to_string(z)))));
            _keyList.push_back(PointGeometry(PointModel(FR, R, ConvertLib::GetFloat(std::to_string(z)))));
            _keyList.push_back(PointGeometry(PointModel(R, R, ConvertLib::GetFloat(std::to_string(z)))));
        }
    }

    void CuboidGeometry::CutSurfaceArea(float unit) {
        if (unit < _size->getL() && unit < _size->getW()) CutXOY(unit);
        if (unit < _size->getL() && unit < _size->getH()) CutXOZ(unit);
        if (unit < _size->getW() && unit < _size->getH()) CutYOZ(unit);
    }

    void CuboidGeometry::CutXOY(float unit) {
        float R = constHelper::POSNUM;
        float FR = constHelper::POSNUM * -1;

        int nx = ConvertLib::roundToInt(round(_size->getL() / unit) - 1);
        int ny = ConvertLib::roundToInt(round(_size->getW() / unit) - 1);

        float stepX = (R - FR) / (nx + 1);
        float stepY = (R - FR) / (ny + 1);
        for (int ix = 1; ix <= nx; ix++) {
            for (int iy = 1; iy <= ny; iy++) {
                float x = ConvertLib::GetFloat(std::to_string(FR + (float)(ix * stepX)));
                float y = ConvertLib::GetFloat(std::to_string(FR + (float)(iy * stepY)));
                _keyList.push_back(PointGeometry(PointModel(x, y, FR)));
                _keyList.push_back(PointGeometry(PointModel(x, y, R)));
            }
        }
    }

    void CuboidGeometry::CutXOZ(float unit) {
        float R = constHelper::POSNUM;
        float FR = constHelper::POSNUM * -1;

        int nx = ConvertLib::roundToInt(round(_size->getL() / unit) - 1);
        int nz = ConvertLib::roundToInt(round(_size->getH() / unit) - 1);

        float stepX = (R - FR) / (nx + 1);
        float stepZ = (R - FR) / (nz + 1);
        for (int ix = 1; ix <= nx; ix++) {
            for (int iz = 1; iz <= nz; iz++) {
                float x = ConvertLib::GetFloat(std::to_string(FR + (float)(ix * stepX)));
                float z = ConvertLib::GetFloat(std::to_string(FR + (float)(iz * stepZ)));
                _keyList.push_back(PointGeometry(PointModel(x, FR, z)));
                _keyList.push_back(PointGeometry(PointModel(x, R, z)));
            }
        }
    }

    void CuboidGeometry::CutYOZ(float unit) {
        float R = constHelper::POSNUM;
        float FR = constHelper::POSNUM * -1;

        int ny = ConvertLib::roundToInt(round(_size->getW() / unit) - 1);
        int nz = ConvertLib::roundToInt(round(_size->getH() / unit) - 1);

        float stepY = (R - FR) / (ny + 1);
        float stepZ = (R - FR) / (nz + 1);
        for (int iy = 1; iy <= ny; iy++) {
            for (int iz = 1; iz <= nz; iz++) {
                float y = ConvertLib::GetFloat(std::to_string(FR + (float)(iy * stepY)));
                float z = ConvertLib::GetFloat(std::to_string(FR + (float)(iz * stepZ)));
                _keyList.push_back(PointGeometry(PointModel(FR, y, z)));
                _keyList.push_back(PointGeometry(PointModel(R, y, z)));
            }
        }
    }

    // 函数定义
    void CuboidGeometry::CreateKeyPointForLine(std::vector<PointGeometry> keyList) {
        // 调用基类的 CreateKeyPoint 方法
        BaseGeometry::CreateKeyPoint();

        // 清空 _keyList 以确保没有旧数据
        _keyList.clear();

        // 遍历 keyList 中的每个元素
        for (const auto& key : keyList) {
            // 将每个 PointGeometry 对象添加到 _keyList 中
            _keyList.push_back(PointGeometry(key));
        }
    }
}
