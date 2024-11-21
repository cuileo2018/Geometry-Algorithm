//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================



using System;


namespace GeoLib
{
    /// <summary>
    /// 圆柱体
    /// </summary>
    public class CylinderGeometry : BaseGeometry
    {
        private CylinderSize _size;
        public CylinderSize size
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
        /// 圆柱体
        /// </summary>
        /// <param name="_centerPoint">中心点</param>
        /// <param name="_rotateAngle">旋转角度</param>
        /// <param name="_size">尺寸</param>
        public CylinderGeometry(PointModel _centerPoint, AngleModel _rotateAngle, CylinderSize _size) : 
            base(_centerPoint, _rotateAngle, constHelper.GeometryType.Clylinder.ToString())
        {
            this._size = _size;
        }

        //public CylinderGeometry(string jsonStr = "") :
        //   base(new PointModel(), new AngleModel(), constHelper.GeometryType.Clylinder.ToString())
        //{
        //    if (!string.IsNullOrEmpty(jsonStr)) FromJsonString(jsonStr);
        //}

        /// <summary>
        /// 生成关键点
        /// </summary>

        public void CreateCylinderKeyPoint(float unit = 0)
        {
            float XC = constHelper.POSNUM;      
            float CXC = constHelper.POSNUM * -1;
            float OB = constHelper.OBANGLE;   
            float ZC = constHelper.ZERO;
            float radinAngle = (float)Math.PI / constHelper.FLATANGLE;

            base.CreateKeyPoint();

            if(unit >= 0)//unit = 0 时，返回16个初始关键点 ; unit > 0 时，返回插值后的自定义关键点和16个初始关键点;
            {
                for (int i = 0; i < 8; i++)
                {
                    float xCoorDinate = XC * (float)Math.Cos(i * OB * radinAngle);
                    float yCoorDinate = XC * (float)Math.Sin(i * OB * radinAngle);
                    _keyList.Add(new PointGeometry(new PointModel(xCoorDinate, yCoorDinate, XC))); //上底面边缘点
                    _keyList.Add(new PointGeometry(new PointModel(xCoorDinate, yCoorDinate, CXC)));//下底面边缘点
                }

                
                if (unit > 0)
                {
                    //分割圆柱体初始边缘点;
                    CutCircleEdgePoint(unit);

                    //分割圆柱体底面;
                    CutBottomSurfaceArea(unit);

                    //分割圆柱体侧面;
                    CutSideSurfaceArea(unit);
                }

                //追加圆柱体上下底面中心点为关键点;
                _keyList.Add(new PointGeometry(new PointModel(ZC, ZC, XC)));
                _keyList.Add(new PointGeometry(new PointModel(ZC, ZC, CXC)));
            }
            
        }

        #region 分割圆柱体初始边缘点
        private void CutCircleEdgePoint(float unit)
        {
            float OB = constHelper.OBANGLE; 
            float FL = constHelper.FLATANGLE;
            if (unit < Math.PI * size.r * OB / FL) cutEdge(unit, size.r); // unit 小于弧长时进行分割 条件：unit < PI * r * 45.0 / 180.0 ; 
        }

        private void cutEdge(float unit, float r)
        {
            float OB = constHelper.OBANGLE; 
            float FL = constHelper.FLATANGLE; 
            float XC = constHelper.POSNUM; 
            float CXC = constHelper.POSNUM * -1;
            float radinAngle = (float)Math.PI / constHelper.FLATANGLE;
            float relativeRadius = XC * r / size.r;

            //计算边缘点间内插点个数 n = (round(Pi * r * 45.0 / 180.0) / unit) - 1
            int cutNum = ConvertLib.GetInt32(Math.Round((Math.PI * r * OB / FL) / unit) - 1); 

            if (cutNum > 0)
            {   
                float averageAngle = OB / (cutNum + 1); //插值点间的角度;
                
                for (int i = 1; i <= cutNum; i++)
                {
                    for(int j = 0; j < 8; j++)
                    {
                        float x = relativeRadius * (float)Math.Cos(((averageAngle * i) + (j * OB)) * radinAngle);
                        float y = relativeRadius * (float)Math.Sin(((averageAngle * i) + (j * OB)) * radinAngle);
                        _keyList.Add(new PointGeometry(new PointModel(x, y, XC)));     //上底面关键点
                        _keyList.Add(new PointGeometry(new PointModel(x, y, CXC)));    //下底面关键点
                    }
                }
            }

        }
        #endregion

        #region 分割圆柱体底面
        private void CutBottomSurfaceArea(float unit)
        {
            int cutRNum = ConvertLib.GetInt32(Math.Round(size.r / unit) - 1); //半径上的插值点个数 n = round(r / unit) - 1;

            if (cutRNum > 0)
            {
                float averageR = size.r / (cutRNum + 1); //半径上关键点间的间距
                for (int i = 1; i <= cutRNum; i++)
                {
                    cutEdge(unit, i * averageR); //分割各同心圆的边缘点；各同心圆半径(i * averageR);
                    cutCircleR(unit);            //分割各同心圆相对的16个关键点;
                }
            }

        }

        private void cutCircleR(float unit) 
        {
            float OB = constHelper.OBANGLE; 
            float XC = constHelper.POSNUM;  
            float CXC = constHelper.POSNUM * -1;
            float radinAngle = (float)Math.PI / constHelper.FLATANGLE; // 角度转弧度系数: PI / 180.0；
            int cutRNum = ConvertLib.GetInt32(Math.Round(size.r / unit) - 1);

            if (cutRNum > 0)
            {
                float averageR = XC / (cutRNum + 1);
                for (int i = 1; i <= cutRNum; i++)
                {
                    for(int j = 0; j < 8; j++)
                    {
                        float xCoordinate = (i * averageR) * (float)Math.Cos(j * OB * radinAngle);
                        float yCoordinate = (i * averageR) * (float)Math.Sin(j * OB * radinAngle);                        
                        _keyList.Add(new PointGeometry(new PointModel(xCoordinate, yCoordinate, XC)));       //上底面
                        _keyList.Add(new PointGeometry(new PointModel(xCoordinate, yCoordinate, CXC)));      //下底面
                    }

                }

            }

        }
        #endregion

        #region 分割圆柱体侧面
        private void CutSideSurfaceArea(float unit)
        {
            if (unit < size.h) CutSideSurface(unit); // unit < h 圆柱体侧面进行切割;
        }

        private void CutSideSurface(float unit)
        {
            float OB = constHelper.OBANGLE;         
            float FL = constHelper.FLATANGLE;       
            float XC = constHelper.POSNUM;          
            float CXC = constHelper.POSNUM * -1;    
            float ZC = constHelper.ZERO;            
            float OBC = constHelper.OBLIQUE;        
            float COBC = constHelper.OBLIQUE * -1;  

            int cutHNum = ConvertLib.GetInt32(Math.Round(size.h / unit) - 1);//圆柱体侧面插值点的个数: n = round(h / unit) - 1;

            if (cutHNum > 0)//侧面可以插值;
            {
                int cutNum = ConvertLib.GetInt32(Math.Round((Math.PI * size.r * OB / FL) / unit) - 1);
                float averageH = 1.0f / (cutHNum + 1);  //计算相对坐标时默认 h = 1.0f;

                //圆柱体底面初始边缘点间没有插值时侧面的表面点插值
                for (int i = 1; i <= cutHNum; i++)
                {
                    _keyList.Add(new PointGeometry(new PointModel(XC, ZC, XC - (i * averageH))));          //0
                    _keyList.Add(new PointGeometry(new PointModel(OBC, OBC, XC - (i * averageH))));    //1
                    _keyList.Add(new PointGeometry(new PointModel(ZC, XC, XC - (i * averageH))));          //2
                    _keyList.Add(new PointGeometry(new PointModel(COBC, OBC, XC - (i * averageH))));   //3   
                    _keyList.Add(new PointGeometry(new PointModel(CXC, ZC, XC - (i * averageH))));         //4
                    _keyList.Add(new PointGeometry(new PointModel(COBC, COBC, XC - (i * averageH))));  //5
                    _keyList.Add(new PointGeometry(new PointModel(ZC, CXC, XC - (i * averageH))));         //6
                    _keyList.Add(new PointGeometry(new PointModel(OBC, COBC, XC - (i * averageH))));   //7
                }

                //底面初始边缘点间有插值时侧面的表面点插值
                if (cutNum >= 1)
                {
                    float averageAngle = OB / (cutNum + 1);
                    float radinAngle = (float)Math.PI / constHelper.FLATANGLE;

                    for (int i = 1; i <= cutNum; i++) 
                    {
                        for(int j = 0; j < 8; j++)
                        {
                            float x = XC * (float)Math.Cos(((averageAngle * i) + (j * OB)) * radinAngle);
                            float y = XC * (float)Math.Sin(((averageAngle * i) + (j * OB)) * radinAngle);
                            for (int k = 1; k <= cutHNum; k++)
                            {
                                _keyList.Add(new PointGeometry(new PointModel(x, y, XC - (k * averageH))));    //侧面关键点;
                            }
                        }
                        
                    }
                }
            }
        }
        #endregion

    }
    
}
