//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================


using System;

namespace GeoLib
{
    public class PointGeometry
    {
        private string _type;
        private string _id;

        private PointModel _position;

        /// <summary>
        /// 点构造函数
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="z">Z坐标</param>
        public PointGeometry(PointModel point)
        {
            this._id = Guid.NewGuid().ToString();
            this._type = constHelper.GeometryType.Point.ToString();

            if (point == null) point = new PointModel();
            _position = point;
        }

        //public PointGeometry(string jsonStr = "")
        //{
        //    this._id = Guid.NewGuid().ToString();
        //    if (!string.IsNullOrEmpty(jsonStr)) FromJsonString(jsonStr);
        //}

        public string type
        {
            get
            {
                return _type;
            }            
        }

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            } 
        }

        public PointModel position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }

        /// <summary>
        /// 同另一个点距离
        /// </summary>
        /// <param name="point">另一个点坐标</param>
        /// <returns></returns>
        public float ToPointDistance(PointModel point)
        {
            float distance = 0;
            distance = (float)Math.Sqrt((Math.Pow((point.x - _position.x), 2) + Math.Pow((point.y - _position.y), 2) + Math.Pow((point.x - _position.z), 2)));

            return distance;
        }
    }
}
