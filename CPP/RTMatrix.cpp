#include "RTMatrix.h"
#include "SizeModel.h"
#include <cmath> // 添加这个头文件

#ifndef M_PI
#define M_PI 3.14159265358979323846 // 手动定义M_PI
#endif

namespace GeoLib {
    const float RTMatrix::onehe = 180.0f;

    RTMatrix::RTMatrix(int row) : rows(row), cols(row) {
        rt_data = new double* [row];
        for (int i = 0; i < row; ++i) {
            rt_data[i] = new double[row]();
        }
    }

    RTMatrix::RTMatrix(int row, int col) : rows(row), cols(col) {
        rt_data = new double* [row];
        for (int i = 0; i < row; ++i) {
            rt_data[i] = new double[col]();
        }
    }

    RTMatrix::RTMatrix(const GeoLib::PointModel& keyPoint) : rows(4), cols(1) {
        rt_data = new double* [4];
        for (int i = 0; i < 4; ++i) {
            rt_data[i] = new double[1]();
        }
        if (keyPoint.isValid()) { // 使用isValid()方法检查对象是否有效
            rt_data[0][0] = keyPoint.getX();
            rt_data[1][0] = keyPoint.getY();
            rt_data[2][0] = keyPoint.getZ();
            rt_data[3][0] = 1;
        }
        else {
            throw std::invalid_argument("Input keyPoint error");
        }
    }

    RTMatrix::RTMatrix(const  GeoLib::PointModel& keyPoint, const  GeoLib::CuboidSize& cuboidSize) : rows(4), cols(1) {
        rt_data = new double* [4];
        for (int i = 0; i < 4; ++i) {
            rt_data[i] = new double[1]();
        }
        if (keyPoint.isValid()) {
            rt_data[0][0] = keyPoint.getX() * cuboidSize.getL();
            rt_data[1][0] = keyPoint.getY() * cuboidSize.getW();
            rt_data[2][0] = keyPoint.getZ() * cuboidSize.getH();
            rt_data[3][0] = 1;
        }
        else {
            throw std::invalid_argument("Input keyPoint error");
        }
    }

    // Implement other constructors similarly...

    RTMatrix::RTMatrix(const  GeoLib::AngleModel& rotationAngle, const  GeoLib::PointModel& Position) : rows(4), cols(4) {
        if (rotationAngle.isValid() && Position.isValid()) {
            rt_data = new double* [4];
            for (int i = 0; i < 4; ++i) {
                rt_data[i] = new double[4]();
            }

            GeoLib::AngleModel angle(rotationAngle.getX() * M_PI / onehe, rotationAngle.getY() * M_PI / onehe, rotationAngle.getZ() * M_PI / onehe);

            double sx = sin(angle.getX());
            double sy = sin(angle.getY());
            double sz = sin(angle.getZ());
            double cx = cos(angle.getX());
            double cy = cos(angle.getY());
            double cz = cos(angle.getZ());

            rt_data[0][0] = cy * cz - sx * sy * sz;
            rt_data[0][1] = cy * sz + sx * sy * cz;
            rt_data[0][2] = -cx * sy;
            rt_data[0][3] = Position.getX();

            rt_data[1][0] = -cx * sz;
            rt_data[1][1] = cx * cz;
            rt_data[1][2] = sx;
            rt_data[1][3] = Position.getY();

            rt_data[2][0] = sy * cz + sx * cy * sz;
            rt_data[2][1] = sy * sz - sx * cy * cz;
            rt_data[2][2] = cx * cy;
            rt_data[2][3] = Position.getZ();

            rt_data[3][0] = 0;
            rt_data[3][1] = 0;
            rt_data[3][2] = 0;
            rt_data[3][3] = 1;
        }
        else {
            throw std::invalid_argument("Input rotationAngle or Position error");
        }
    }

    // Implement other constructors similarly...

    RTMatrix::RTMatrix(const RTMatrix& m) : rows(m.rows), cols(m.cols) {
        rt_data = new double* [rows];
        for (int i = 0; i < rows; ++i) {
            rt_data[i] = new double[cols];
            for (int j = 0; j < cols; ++j) {
                rt_data[i][j] = m.rt_data[i][j];
            }
        }
    }

    int RTMatrix::Row() const {
        return rows;
    }

    int RTMatrix::Col() const {
        return cols;
    }

    double RTMatrix::operator()(int row) const {
        return rt_data[row][0];
    }

    void RTMatrix::operator()(int row, double value) {
        rt_data[row][0] = value;
    }

    double RTMatrix::operator()(int row, int col) const {
        return rt_data[row][col];
    }

    void RTMatrix::operator()(int row, int col, double value) {
        rt_data[row][col] = value;
    }

    RTMatrix RTMatrix::operator*(const RTMatrix& rm) const {
        if (this->Col() != rm.Row()) {
            throw std::invalid_argument("相乘的两个矩阵的行列数不匹配");
        }

        RTMatrix ret(this->Row(), rm.Col());

        for (int i = 0; i < this->Row(); ++i) {
            for (int j = 0; j < rm.Col(); ++j) {
                double tmpData = 0.0; // 初始化临时变量
                for (int k = 0; k < this->Col(); ++k) {
                    tmpData += (*this)(i, k) * rm(k, j);
                }
                ret(i, j, tmpData); // 设置结果矩阵的值
            }
        }
        return ret;
    }
}