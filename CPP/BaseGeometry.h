//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2024 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2024.08 崔艳龙 创建
//============================================

#ifndef BASEGEOMETRY_H
#define BASEGEOMETRY_H

#include <string>
#include <vector>
#include <memory>
#include "PointModel.h"
#include "AngleModel.h"
#include "PointGeometry.h"
#include "PointModel.h"

namespace GeoLib {
    class BaseGeometry {
    private:
        std::string _type;
        std::string _id;
        std::shared_ptr<PointModel> _centerPoint;
        std::shared_ptr<AngleModel> _rotateAngle;
        std::vector<PointGeometry> _keyList; // 使用shared_ptr管理PointGeometry对象
        std::shared_ptr<void> _fsize;

    public:
        BaseGeometry(std::shared_ptr<PointModel> centerPoint, std::shared_ptr<AngleModel> rotateAngle, const std::string& type);

        std::string getId() const;
        void setId(const std::string& id);

        std::shared_ptr<PointModel> getPosition() const;
        void setPosition(std::shared_ptr<PointModel> centerPoint);

        std::shared_ptr<AngleModel> getRotation() const;
        void setRotation(std::shared_ptr<AngleModel> rotateAngle);

        std::vector<PointGeometry> getKeypoints() const;

    protected:
        std::shared_ptr<void> getFSize() const;
        void setFSize(std::shared_ptr<void> fsize);

    public:
        std::string getType() const;

        virtual void CreateKeyPoint();
        virtual void Shifting();
        virtual void Rotate();
        virtual float ToGeometryDistance();
    };
}
#endif // BASEGEOMETRY_H
