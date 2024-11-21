// PointGeometry.h
#pragma once
#include <memory>
#include <string>
#include "constHelper.h" // 基础类先引用
#include "PointModel.h"
#include "UUIDGenerator.h"


namespace GeoLib {
    class PointGeometry {
    public:
        PointGeometry(std::shared_ptr<PointModel> point);
        PointGeometry(PointModel point);
        PointGeometry();
        std::string getType(constHelper::GeometryType _type); // 添加方法参数
        std::string getId() const;
        void setId(const std::string& id);
        PointModel getPosition();
        void setPosition(const PointModel& position);
        float toPointDistance(const PointModel& point) const;



    private:
        std::string _id;
        constHelper::GeometryType _type;
        PointModel _position;
    };
}