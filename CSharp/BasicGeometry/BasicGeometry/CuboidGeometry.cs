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
    /// 长方体
    /// </summary>
    public class CuboidGeometry : BaseGeometry
    {
        private CuboidSize _size;
        public CuboidSize size
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
        /// 长方体
        /// </summary>
        /// <param name="_centerPoint">中心点</param>
        /// <param name="_rotateAngle">旋转角度</param>
        /// <param name="_size">尺寸</param>
        public CuboidGeometry(PointModel _centerPoint, AngleModel _rotateAngle, CuboidSize _size) : 
            base(_centerPoint, _rotateAngle, constHelper.GeometryType.Cuboid.ToString())
        {
            this._size = _size;    
        }

        /// <summary>
        /// 线段继承用
        /// </summary>
        /// <param name="_centerPoint">中心点</param>
        /// <param name="_rotateAngle">旋转角度</param>
        public CuboidGeometry(PointModel _centerPoint, AngleModel _rotateAngle) :
           base(_centerPoint, _rotateAngle, constHelper.GeometryType.Line.ToString())
        {
            this._size = new CuboidSize(0, 0, 0);
        }

        //public CuboidGeometry(string jsonStr = "") : base(new PointModel(), new AngleModel(), constHelper.GeometryType.Cuboid.ToString())
        //{
        //    if (!string.IsNullOrEmpty(jsonStr)) FromJsonString(jsonStr);
        //}

        /// <summary>
        /// 生成关键点
        /// </summary>
        public void CreateCuboidKeyPoint(float unit = 0)
        {
            float R = constHelper.POSNUM;
            float FR = constHelper.POSNUM * -1;

            base.CreateKeyPoint();
            _keyList.Add(new PointGeometry(new PointModel(FR, FR, FR)));   // A
            _keyList.Add(new PointGeometry(new PointModel(R, FR, FR)));    // B
            _keyList.Add(new PointGeometry(new PointModel(FR, R, FR)));    // C
            _keyList.Add(new PointGeometry(new PointModel(R, R, FR)));     // D
            _keyList.Add(new PointGeometry(new PointModel(FR, FR, R)));    // A'
            _keyList.Add(new PointGeometry(new PointModel(R, FR, R)));     // B'
            _keyList.Add(new PointGeometry(new PointModel(FR, R, R)));     // C'
            _keyList.Add(new PointGeometry(new PointModel(R, R, R)));      // D'

            // 分割边缘点
            CutSideLength(unit);
            // 分割表面积
            CutSurfaceArea(unit);
        }

        #region 分割边缘点
        private void CutSideLength(float unit)
        {
            if (unit < size.l) cutX(unit);
            if (unit < size.w) cutY(unit);
            if (unit < size.h) cutZ(unit);
        }
        /// <summary>
        /// 分割长（X轴方向）
        /// </summary>
        private void cutX(float unit)
        {
            float R = constHelper.POSNUM;
            float FR = constHelper.POSNUM * -1;

            // AB CD A'B' C'D'四条线
            int forCn = ConvertLib.GetInt32(Math.Round(size.l / unit) - 1);
            float step = (R - FR) / (forCn + 1);
            for (int ix = 1; ix <= forCn; ix++)
            {
                float x = FR + (float)(ix * step);
                _keyList.Add(new PointGeometry(new PointModel(ConvertLib.GetFloat(x.ToString("0.00")), FR, FR)));
                _keyList.Add(new PointGeometry(new PointModel(ConvertLib.GetFloat(x.ToString("0.00")), R, FR)));
                _keyList.Add(new PointGeometry(new PointModel(ConvertLib.GetFloat(x.ToString("0.00")), FR, R)));
                _keyList.Add(new PointGeometry(new PointModel(ConvertLib.GetFloat(x.ToString("0.00")), R, R)));
            }
        }

        /// <summary>
        /// 分割宽（Y轴方向）
        /// </summary>
        /// <param name="unit"></param>
        private void cutY(float unit)
        {
            float R = constHelper.POSNUM;
            float FR = constHelper.POSNUM * -1;

            // AC BD A'C' B'D'四条线
            int forCn = ConvertLib.GetInt32(Math.Round(size.w / unit) - 1);
            float step = (R - FR) / (forCn + 1);
            for (int iy = 1; iy <= forCn; iy++)
            {
                float y = FR + (float)(iy * step);
                _keyList.Add(new PointGeometry(new PointModel(FR, ConvertLib.GetFloat(y.ToString("0.00")), FR)));
                _keyList.Add(new PointGeometry(new PointModel(R, ConvertLib.GetFloat(y.ToString("0.00")), FR)));
                _keyList.Add(new PointGeometry(new PointModel(FR, ConvertLib.GetFloat(y.ToString("0.00")), R)));
                _keyList.Add(new PointGeometry(new PointModel(R, ConvertLib.GetFloat(y.ToString("0.00")), R)));
            }
        }

        /// <summary>
        /// 分割高（Z轴方向）
        /// </summary>
        /// <param name="unit"></param>
        private void cutZ(float unit)
        {
            float R = constHelper.POSNUM;
            float FR = constHelper.POSNUM * -1;

            // AA' BB' CC' DD'四条线
            int forCn = ConvertLib.GetInt32(Math.Round(size.h / unit) - 1);
            float step = (R - FR) / (forCn + 1);
            for (int iz = 1; iz <= forCn; iz++)
            {
                float y = FR + (float)(iz * step);
                _keyList.Add(new PointGeometry(new PointModel(FR, FR, ConvertLib.GetFloat(y.ToString("0.00")))));
                _keyList.Add(new PointGeometry(new PointModel(R, FR, ConvertLib.GetFloat(y.ToString("0.00")))));
                _keyList.Add(new PointGeometry(new PointModel(FR, R, ConvertLib.GetFloat(y.ToString("0.00")))));
                _keyList.Add(new PointGeometry(new PointModel(R, R, ConvertLib.GetFloat(y.ToString("0.00")))));
            }
        }
        #endregion

        #region 分割表面积
        private void CutSurfaceArea(float unit)
        {
            // XOY平面(分割X、Y方向)
            if (unit < size.l && unit < size.w) CutXOY(unit);
            // XOZ平面(分割X、Z方向)
            if (unit < size.l && unit < size.h) CutXOZ(unit);
            // YOZ平面(分割Y、Z方向)
            if (unit < size.w && unit < size.h) CutYOZ(unit);

        }

        /// <summary>
        /// 分割XOY平面
        /// </summary>
        /// <param name="unit"></param>
        private void CutXOY(float unit)
        {
            float R = constHelper.POSNUM;
            float FR = constHelper.POSNUM * -1;

            // 计算X方向内插点个数 nx = round(l / unit) - 1
            // 计算Y方向内插点个数 ny = round(w / unit) - 1
            int nx = ConvertLib.GetInt32(Math.Round(size.l / unit) - 1);
            int ny = ConvertLib.GetInt32(Math.Round(size.w / unit) - 1);

            float stepX = (R - FR) / (nx + 1);
            float stepY = (R - FR) / (ny + 1);
            for (int ix = 1; ix <= nx; ix++) 
            {
                for (int iy = 1; iy <= ny; iy++)
                {
                    float x = ConvertLib.GetFloat((FR + (float)(ix * stepX)).ToString("0.00"));
                    float y = ConvertLib.GetFloat((FR + (float)(iy * stepY)).ToString("0.00"));
                    _keyList.Add(new PointGeometry(new PointModel(x, y, FR)));
                    _keyList.Add(new PointGeometry(new PointModel(x, y, R)));
                }
            }
        }

        // <summary>
        /// 分割XOZ平面
        /// </summary>
        /// <param name="unit"></param>
        private void CutXOZ(float unit)
        {
            float R = constHelper.POSNUM;
            float FR = constHelper.POSNUM * -1;

            // 计算X方向内插点个数 nx = round(l / unit) - 1
            // 计算Z方向内插点个数 nz = round(h / unit) - 1
            int nx = ConvertLib.GetInt32(Math.Round(size.l / unit) - 1);
            int nz = ConvertLib.GetInt32(Math.Round(size.h / unit) - 1);

            float stepX = (R - FR) / (nx + 1);
            float stepZ = (R - FR) / (nz + 1);
            for (int ix = 1; ix <= nx; ix++)
            {
                for (int iz = 1; iz <= nz; iz++)
                {
                    float x = ConvertLib.GetFloat((FR + (float)(ix * stepX)).ToString("0.00"));
                    float z = ConvertLib.GetFloat((FR + (float)(iz * stepZ)).ToString("0.00"));
                    _keyList.Add(new PointGeometry(new PointModel(x, FR, z)));
                    _keyList.Add(new PointGeometry(new PointModel(x, R, z)));
                }
            }
        }

        // <summary>
        /// 分割YOZ平面
        /// </summary>
        /// <param name="unit"></param>
        private void CutYOZ(float unit)
        {
            float R = constHelper.POSNUM;
            float FR = constHelper.POSNUM * -1;

            // 计算Y方向内插点个数 ny = round(w / unit) - 1
            // 计算Z方向内插点个数 nz = round(h / unit) - 1
            int ny = ConvertLib.GetInt32(Math.Round(size.w / unit) - 1);
            int nz = ConvertLib.GetInt32(Math.Round(size.h / unit) - 1);

            float stepY = (R - FR) / (ny + 1);
            float stepZ = (R - FR) / (nz + 1);
            for (int iy = 1; iy <= ny; iy++)
            {
                for (int iz = 1; iz <= nz; iz++)
                {
                    float y = ConvertLib.GetFloat((FR + (float)(iy * stepY)).ToString("0.00"));
                    float z = ConvertLib.GetFloat((FR + (float)(iz * stepZ)).ToString("0.00"));
                    _keyList.Add(new PointGeometry(new PointModel(FR, y, z)));
                    _keyList.Add(new PointGeometry(new PointModel(R, y, z)));
                }
            }
        }
        #endregion

        protected void CreateKeyPointForLine(List<PointGeometry> keyList)
        {
            base.CreateKeyPoint();
            _keyList = keyList;
        }

    }

    
}
