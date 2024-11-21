// CMakeProject1.cpp: 定义应用程序的入口点。
//
#include <iostream> // C++ 自带的先引用

#include "AngleModel.h"
#include "PointModel.h"
#include "SizeModel.h" // 删掉CuboidModel


using namespace GeoLib;

int main()
{
    std::cout << "Hello CMake." << std::endl;

    double x = 30.0;
    double y = 45.0;
    double z = 60.0;

    GeoLib::AngleModel angle(x, y,z);

    std::cout << "X轴旋转角度: " << angle.getX() << std::endl;
    std::cout << "Y轴旋转角度: " << angle.getY() << std::endl;
    std::cout << "Z轴旋转角度: " << angle.getZ() << std::endl;

    // 测试设置方法
    angle.setX(90.0);
    angle.setY(180.0);
    angle.setZ(270.0);

    std::cout << "更新后的X轴旋转角度: " << angle.getX() << std::endl;
    std::cout << "更新后的Y轴旋转角度: " << angle.getY() << std::endl;
    std::cout << "更新后的Z轴旋转角度: " << angle.getZ() << std::endl;

    GeoLib::PointModel point(1.0, 2.0, 3.0);

    std::cout << "X坐标: " << point.getX() << std::endl;
    std::cout << "Y坐标: " << point.getY() << std::endl;
    std::cout << "Z坐标: " << point.getZ() << std::endl;

    // 测试设置方法
    point.setX(4.0);
    point.setY(5.0);
    point.setZ(6.0);

    std::cout << "更新后的X坐标: " << point.getX() << std::endl;
    std::cout << "更新后的Y坐标: " << point.getY() << std::endl;
    std::cout << "更新后的Z坐标: " << point.getZ() << std::endl;

    // 测试 CuboidSize
    GeoLib::CuboidSize cuboid(1.0, 2.0, 3.0);
    std::cout << "Cuboid - 长: " << cuboid.getL() << ", 宽: " << cuboid.getW() << ", 高: " << cuboid.getH() << std::endl;

    return 0;
}
