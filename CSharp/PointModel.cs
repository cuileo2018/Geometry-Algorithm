//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================


namespace GeoLib
{
    public class PointModel
    {
        private float _x;
        private float _y;
        private float _z;

        public PointModel(float _x, float _y, float _z)
        {
            this._x = _x;
            this._y = _y;
            this._z = _z;
        }

        public PointModel()
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
