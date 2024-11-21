//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================

namespace GeoLib
{
    /// <summary>
    /// 公共变量定义
    /// </summary>
    public static class constHelper
    {
        // 定义抽象点里中心点距离(基准位)
        public static float POSNUM = (float)0.5;

        public static float ZERO = (float)0.0;
        public static float OBLIQUE = (float)0.3535;
        public static float OBANGLE = (float)45.0;
        public static float FLATANGLE = (float)180.0;

        /// <summary>
        /// 集合体类型
        /// </summary>
        public enum GeometryType
        {
            /// <summary>
            /// 长方体
            /// </summary>
            Cuboid,
            /// <summary>
            /// 圆柱
            /// </summary>
            Clylinder,
            /// <summary>
            /// 线段
            /// </summary>
            Line,
            /// <summary>
            /// 点
            /// </summary>
            Point
        }

    }
}
