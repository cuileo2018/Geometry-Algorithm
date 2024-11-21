#include "PointModel.h"

namespace GeoLib
{
    // 构造函数
    PointModel::PointModel(float x, float y, float z) : _x(x), _y(y), _z(z) {}

    // 默认构造函数
    PointModel::PointModel() : _x(0), _y(0), _z(0) {}

    // 获取和设置X坐标
    float PointModel::getX() const { return _x; }
    void PointModel::setX(float x) { _x = x; }

    // 获取和设置Y坐标
    float PointModel::getY() const { return _y; }
    void PointModel::setY(float y) { _y = y; }

    // 获取和设置Z坐标
    float PointModel::getZ() const { return _z; }
    void PointModel::setZ(float z) { _z = z; }
}
