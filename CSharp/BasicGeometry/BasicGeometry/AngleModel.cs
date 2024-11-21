//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================

namespace GeoLib
{
    public class AngleModel
    {
        private float _x;
        private float _y;
        private float _z;

        /// <summary>
        /// 角度构造函数
        /// </summary>
        /// <param name="x">围绕X轴旋转角度</param>
        /// <param name="y">围绕Y轴旋转角度</param>
        /// <param name="z">围绕Z轴旋转角度</param>
        public AngleModel(float x, float y, float z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }
        public AngleModel()
        {
            this._x = 0;
            this._y = 0;
            this._z = 0;
        }

        public float x
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public float y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }

        public float z
        {
            get
            {
                return _z;
            }

            set
            {
                _z = value;
            }
        }
    }
}
