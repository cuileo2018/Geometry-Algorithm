//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 范洪达 创建
//============================================

#ifndef RTMATRIX_H
#define RTMATRIX_H

#include <stdexcept>
#include <cmath>
#include "PointModel.h"
#include "SizeModel.h"
#include "AngleModel.h"

namespace GeoLib {
    class RTMatrix {
    public:
        static const float onehe;

        RTMatrix(int row);
        RTMatrix(int row, int col);
        RTMatrix(const GeoLib::PointModel& keyPoint);
        RTMatrix(const GeoLib::PointModel& keyPoint, const GeoLib::CuboidSize& cuboidSize);
        RTMatrix(const  GeoLib::AngleModel& rotationAngle, const  GeoLib::PointModel& Position);
        RTMatrix(const RTMatrix& m);

        int Row() const;
        int Col() const;

        double operator()(int row) const;
        void operator()(int row, double value);

        double operator()(int row, int col) const;
        void operator()(int row, int col, double value);

        RTMatrix operator*(const RTMatrix& rm) const; // 修改为非静态成员函数

    private:
        double** rt_data;
        int rows;
        int cols;
    };
}
#endif // RTMATRIX_H
