//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================

#ifndef CYLINDERGEOMETRY_H
#define CYLINDERGEOMETRY_H

#include "BaseGeometry.h"
#include "PointModel.h"
#include "AngleModel.h"
#include "constHelper.h"
#include "ConvertLib.h"
#include "PointGeometry.h"
#include <vector>
#include <cmath>
#include "SizeModel.h"

namespace GeoLib {
    class CylinderGeometry : public BaseGeometry {
    private:
        std::shared_ptr<CylinderSize> _size;
        std::vector<PointGeometry> _keyList;

        void CutCircleEdgePoint(float unit);
        void cutEdge(float unit, float r);
        void CutBottomSurfaceArea(float unit);
        void cutCircleR(float unit);
        void CutSideSurfaceArea(float unit);
        void CutSideSurface(float unit);

    public:
        CylinderGeometry(std::shared_ptr<PointModel> centerPoint, std::shared_ptr<AngleModel> rotateAngle, std::shared_ptr <CylinderSize> _size);
        void CreateCylinderKeyPoint(float unit = 0);
    };
}
#endif // CYLINDERGEOMETRY_H
