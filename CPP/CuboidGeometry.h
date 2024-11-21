//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================

#ifndef CUBOIDGEOMETRY_H
#define CUBOIDGEOMETRY_H

#include "BaseGeometry.h"
#include "PointModel.h"
#include "AngleModel.h"
#include "SizeModel.h"
#include "PointGeometry.h"
#include "constHelper.h"
#include "ConvertLib.h"
#include <vector>

namespace GeoLib {
    class CuboidGeometry : public BaseGeometry {
    private:
        std::shared_ptr<CuboidSize> _size;
        std::vector<PointGeometry> _keyList;

        void CutSideLength(float unit);
        void cutX(float unit);
        void cutY(float unit);
        void cutZ(float unit);
        void CutSurfaceArea(float unit);
        void CutXOY(float unit);
        void CutXOZ(float unit);
        void CutYOZ(float unit);

    public:
        CuboidGeometry(std::shared_ptr<PointModel> _centerPoint, std::shared_ptr<AngleModel> _rotateAngle, std::shared_ptr<CuboidSize> _size);
        CuboidGeometry(std::shared_ptr<PointModel> _centerPoint, std::shared_ptr<AngleModel> _rotateAngle);
        CuboidSize getSize() const;
        void setSize(const CuboidSize& size);
        void CreateCuboidKeyPoint(float unit = 0);
        void CreateKeyPointForLine(std::vector<PointGeometry> keyList);
    };
}
#endif // CUBOIDGEOMETRY_H
