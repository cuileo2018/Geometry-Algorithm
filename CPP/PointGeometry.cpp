// PointGeometry.cpp

#include <cmath> // C/C++ 自带库先引用

#include "ConvertLib.h"
#include "PointGeometry.h"
#include "UUIDGenerator.h"

namespace GeoLib
{
    PointGeometry::PointGeometry(std::shared_ptr<PointModel> point) {
        this->_id = UUIDGenerator::generateUUID();
        this->_type = constHelper::GeometryType::Point;

        if (!point) {
            point = std::make_shared<PointModel>();
        }
        _position = *point;
    }

    PointGeometry::PointGeometry(PointModel point) {
        this->_id = UUIDGenerator::generateUUID();
        this->_type = constHelper::GeometryType::Point;
        _position = point;
    }

    PointGeometry::PointGeometry() {
        this->_id = UUIDGenerator::generateUUID();
        this->_type = constHelper::GeometryType::Point;
    }

    std::string PointGeometry::getType(constHelper::GeometryType _type) { // 添加方法参数
        return ConvertLib::geometryTypeToString(_type);
    }

    std::string PointGeometry::getId() const {
        return _id;
    }

    void PointGeometry::setId(const std::string& id) {
        _id = id;
    }

    PointModel PointGeometry::getPosition() {
        return _position;
    }

    void PointGeometry::setPosition(const PointModel& position) {
        _position = position;
    }

    float PointGeometry::toPointDistance(const PointModel& point) const {
        float distance = 0;
        distance = std::sqrt(std::pow((point.getX() - _position.getX()), 2) + std::pow((point.getY() - _position.getY()), 2) + std::pow((point.getZ() - _position.getZ()), 2));
        return distance;
    }
}
