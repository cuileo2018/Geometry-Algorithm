//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2024 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2024.08 崔艳龙 创建
//============================================


#ifndef LINEGEOMETRY_H
#define LINEGEOMETRY_H

#include "PointModel.h"
#include "AngleModel.h"
#include "PointGeometry.h"
#include "constHelper.h"
#include "ConvertLib.h"
#include <vector>
#include <memory>
#include "SizeModel.h"
#include "CuboidGeometry.h"

namespace GeoLib {
    class LineGeometry : public CuboidGeometry {
    private:
        LineSize _size;

    public:
        LineGeometry(std::shared_ptr<PointModel> centerPoint, std::shared_ptr<AngleModel> rotateAngle, LineSize size);

        LineSize getSize() const;
        void setSize(LineSize size);

        void CreateLineKeyPoint(float unit = 0);
    };
}
#endif // LINEGEOMETRY_H
