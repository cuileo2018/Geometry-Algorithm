//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2024 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2024.08 崔艳龙 创建
//============================================

#ifndef SIZEMODEL_H
#define SIZEMODEL_H

namespace GeoLib
{
    /// <summary>
    /// 长方体尺寸
    /// </summary>
    class CuboidSize
    {
    private:
        float _l;
        float _w;
        float _h;

    public:
        // 构造函数
        CuboidSize(float l, float w, float h);

        // 获取和设置长
        float getL() const;
        void setL(float l);

        // 获取和设置宽
        float getW() const;
        void setW(float w);

        // 获取和设置高
        float getH() const;
        void setH(float h);
    };

    /// <summary>
    /// 圆柱体尺寸
    /// </summary>
    class CylinderSize
    {
    private:
        float _r;
        float _h;

    public:
        // 构造函数
        CylinderSize(float r, float h);

        // 获取和设置半径
        float getR() const;
        void setR(float r);

        // 获取和设置高度
        float getH() const;
        void setH(float h);
    };

    /// <summary>
    /// 线段尺寸
    /// </summary>
    class LineSize
    {
    private:
        float _l;

    public:
        // 构造函数
        LineSize(float l);

        // 获取和设置长
        float getL() const;
        void setL(float l);
    };
}

#endif // SIZEMODEL_H

