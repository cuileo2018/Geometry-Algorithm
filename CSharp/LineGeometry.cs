//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================


using System;
using System.Collections.Generic;

namespace GeoLib
{
    /// <summary>
    /// 线段
    /// </summary>
    public class LineGeometry : CuboidGeometry
    {
        private LineSize _size;
        public new LineSize size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
            }
        }

        /// <summary>
        /// 线段
        /// </summary>
        /// <param name="_centerPoint">中心点</param>
        /// <param name="_rotateAngle">旋转角度</param>
        /// <param name="_size">尺寸</param>
        public LineGeometry(PointModel _centerPoint, AngleModel _rotateAngle, LineSize _size) : 
            base(_centerPoint, _rotateAngle)
        {
            this._size = _size;
        }

        //public LineGeometry(string jsonStr = "") :
        //  base(new PointModel(), new AngleModel())
        //{
        //    if (!string.IsNullOrEmpty(jsonStr)) FromJsonString(jsonStr);
        //}

        /// <summary>
        /// 生成关键点
        /// </summary>
        public void CreateLineKeyPoint(float unit = 0)
        {
            float R = constHelper.POSNUM;
            float FR = constHelper.POSNUM * -1;

            List<PointGeometry> keyList = new List<PointGeometry>();
            if(unit <= 0 || unit >= size.l)
            {
                keyList.Add(new PointGeometry(new PointModel(0, 0, FR)));
                keyList.Add(new PointGeometry(new PointModel(0, 0, R)));
            }else
            {
                int forCn = ConvertLib.GetInt32(Math.Round(size.l / unit) - 1);
                float step = (R - FR) / (forCn + 1);
                keyList.Add(new PointGeometry(new PointModel(0, 0, FR)));
                for (int ix = 1; ix <= forCn; ix++)
                {
                    float z = FR + (float)(ix * step);
                    keyList.Add(new PointGeometry(new PointModel(0, 0, ConvertLib.GetFloat(z.ToString("0.00")))));

                }
                keyList.Add(new PointGeometry(new PointModel(0, 0, R)));
            }

            base.CreateKeyPointForLine(keyList);

        }
    }

}
