//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2024 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2024.08 崔艳龙 创建
//============================================

#ifndef ANGLEMODEL_H
#define ANGLEMODEL_H

namespace GeoLib
{
    class AngleModel
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
        // 角度构造函数
        AngleModel(float x, float y, float z);

        // 默认构造函数
        AngleModel();

        // 获取和设置X轴旋转角度
        float getX() const;
        void setX(float x);

        // 获取和设置Y轴旋转角度
        float getY() const;
        void setY(float y);

        // 获取和设置Z轴旋转角度
        float getZ() const;
        void setZ(float z);
    };
}

#endif // ANGLEMODEL_H
