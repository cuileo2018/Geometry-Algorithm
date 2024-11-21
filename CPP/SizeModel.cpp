#include "SizeModel.h"

namespace GeoLib
{
    // CuboidSize 构造函数
    CuboidSize::CuboidSize(float l, float w, float h) : _l(l), _w(w), _h(h) {}

    // 获取和设置长
    float CuboidSize::getL() const { return _l; }
    void CuboidSize::setL(float l) { _l = l; }

    // 获取和设置宽
    float CuboidSize::getW() const { return _w; }
    void CuboidSize::setW(float w) { _w = w; }

    // 获取和设置高
    float CuboidSize::getH() const { return _h; }
    void CuboidSize::setH(float h) { _h = h; }

    // CylinderSize 构造函数
    CylinderSize::CylinderSize(float r, float h) : _r(r), _h(h) {}

    // 获取和设置半径
    float CylinderSize::getR() const { return _r; }
    void CylinderSize::setR(float r) { _r = r; }

    // 获取和设置高度
    float CylinderSize::getH() const { return _h; }
    void CylinderSize::setH(float h) { _h = h; }

    // LineSize 构造函数
    LineSize::LineSize(float l) : _l(l) {}

    // 获取和设置长
    float LineSize::getL() const { return _l; }
    void LineSize::setL(float l) { _l = l; }
}

