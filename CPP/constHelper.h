//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2024 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2024.08 崔艳龙 创建
//============================================

#ifndef CONSTHELPER_H
#define CONSTHELPER_H

namespace GeoLib
{
    /// <summary>
    /// 公共变量定义
    /// </summary>
    class constHelper
    {
    public:
        // 定义抽象点里中心点距离(基准位)
        static const float POSNUM;

        static const float ZERO;
        static const float OBLIQUE;
        static const float OBANGLE;
        static const float FLATANGLE;

        /// <summary>
        /// 集合体类型
        /// </summary>
        enum class GeometryType
        {
            /// <summary>
            /// 长方体
            /// </summary>
            Cuboid,
            /// <summary>
            /// 圆柱
            /// </summary>
            Cylinder,
            /// <summary>
            /// 线段
            /// </summary>
            Line,
            /// <summary>
            /// 点
            /// </summary>
            Point
        };
    };
}

#endif // CONSTHELPER_H
