//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================

#include "LineGeometry.h"

namespace GeoLib {
    LineGeometry::LineGeometry(std::shared_ptr<PointModel> centerPoint, std::shared_ptr<AngleModel> rotateAngle, LineSize size)
        : CuboidGeometry(centerPoint, rotateAngle), _size(size) {}

    LineSize LineGeometry::getSize() const {
        return _size;
    }

    void LineGeometry::setSize(LineSize size) {
        _size = size;
    }

    void LineGeometry::CreateLineKeyPoint(float unit) {
        float R = constHelper::POSNUM;
        float FR = constHelper::POSNUM * -1;

        std::vector<std::shared_ptr<PointGeometry>> keyList;
        if (unit <= 0 || unit >= _size.getL()) {
            keyList.push_back(std::make_shared<PointGeometry>(PointModel(0, 0, FR)));
            keyList.push_back(std::make_shared<PointGeometry>(PointModel(0, 0, R)));
        }
        else {
            int forCn = ConvertLib::roundToInt(_size.getL() / unit) - 1;
            float step = (R - FR) / (forCn + 1);
            keyList.push_back(std::make_shared<PointGeometry>(PointModel(0, 0, FR)));
            for (int ix = 1; ix <= forCn; ix++) {
                float z = FR + (ix * step);
                keyList.push_back(std::make_shared<PointGeometry>(PointModel(0, 0, ConvertLib::GetFloat(std::to_string(z)))));
            }
            keyList.push_back(std::make_shared<PointGeometry>(PointModel(0, 0, R)));
        }

        // 将 std::vector<std::shared_ptr<PointGeometry>> 转换为 std::vector<PointGeometry>
        std::vector<PointGeometry> keyListConverted;
        for (const auto& ptr : keyList) {
            keyListConverted.push_back(*ptr);
        }

        CuboidGeometry::CreateKeyPointForLine(keyListConverted);
    }
}
