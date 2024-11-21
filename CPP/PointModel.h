//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2024 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2024.08 崔艳龙 创建
//============================================

#ifndef POINTMODEL_H
#define POINTMODEL_H

namespace GeoLib
{
    class PointModel
    {
    private:
        float _x;
        float _y;
        float _z;

    public:
        bool isValid() const {
			if (_x != 0.0) return true;
            // 实现有效性检查逻辑，例如检查某些成员变量是否初始化
            return false; // 根据实际情况返回true或false
        }

        // 构造函数
        PointModel(float x, float y, float z);

        // 默认构造函数
        PointModel();

        // 获取和设置X坐标
        float getX() const;
        void setX(float x);

        // 获取和设置Y坐标
        float getY() const;
        void setY(float y);

        // 获取和设置Z坐标
        float getZ() const;
        void setZ(float z);
    };
}

#endif // POINTMODEL_H
