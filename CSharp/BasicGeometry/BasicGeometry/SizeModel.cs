//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================

namespace GeoLib
{
    /// <summary>
    /// 长方体尺寸
    /// </summary>
    public class CuboidSize
    {
        private float _l;
        private float _w;
        private float _h;

        /// <summary>
        /// 尺寸
        /// </summary>
        /// <param name="_l">长</param>
        /// <param name="_w">宽</param>
        /// <param name="_h">高</param>
        public CuboidSize(float _l, float _w, float _h)
        {
            this._l = _l;
            this._w = _w;
            this._h = _h;
        }

        public float l
        {
            get
            {
                return _l;
            }

            set
            {
                _l = value;
            }
        }

        public float w
        {
            get
            {
                return _w;
            }

            set
            {
                _w = value;
            }
        }

        public float h
        {
            get
            {
                return _h;
            }

            set
            {
                _h = value;
            }
        }
    }

    /// <summary>
    /// 圆柱体尺寸
    /// </summary>
    public class CylinderSize
    {
        private float _r;
        private float _h;

        /// <summary>
        /// 尺寸
        /// </summary>
        /// <param name="_r">半径</param>
        /// <param name="_h">高度</param>
        public CylinderSize(float _r, float _h)
        {
            this._r = _r;
            this._h = _h;
        }

        public float r
        {
            get
            {
                return _r;
            }

            set
            {
                _r = value;
            }
        }

        public float h
        {
            get
            {
                return _h;
            }

            set
            {
                _h = value;
            }
        }
    }

    /// <summary>
    /// 线段尺寸
    /// </summary>
    public class LineSize
    {
        private float _l;

        /// <summary>
        /// 尺寸
        /// </summary>
        /// <param name="_l">长</param>
        public LineSize(float _l)
        {
            this._l = _l;
        }

        public float l
        {
            get
            {
                return _l;
            }

            set
            {
                _l = value;
            }
        }
    }
}
