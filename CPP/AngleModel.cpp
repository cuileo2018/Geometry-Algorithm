#include "AngleModel.h"

namespace GeoLib
{
    // 角度构造函数
    AngleModel::AngleModel(float x, float y, float z) : _x(x), _y(y), _z(z) {}

    // 默认构造函数
    AngleModel::AngleModel() : _x(0), _y(0), _z(0) {}

    // 获取和设置X轴旋转角度
    float AngleModel::getX() const { return _x; }
    void AngleModel::setX(float x) { _x = x; }

    // 获取和设置Y轴旋转角度
    float AngleModel::getY() const { return _y; }
    void AngleModel::setY(float y) { _y = y; }

    // 获取和设置Z轴旋转角度
    float AngleModel::getZ() const { return _z; }
    void AngleModel::setZ(float z) { _z = z; }
}
