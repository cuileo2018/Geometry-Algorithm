//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 范洪达 创建
//============================================

using System;

namespace GeoLib
{
    /// <summary>
    /// 旋转平移矩阵
    /// </summary>
    public class RTMatrix
    {
        public const float onehe = 180;

        //构造方法
        public RTMatrix(int row)
        {
            rt_data = new double[row, row]; 
        }
        public RTMatrix(int row, int col)
        {
            rt_data = new double[row, col];
        }

        //构造关键点齐次矩阵
        /// <summary>
        ///                   x                       x
        ///将关键点相对坐标 [ y ]转化成齐次矩阵形式 [ y ]
        ///                   z                       z
        ///                                           1
        /// </summary>
        /// <param name="keyPoint"></param>
        public RTMatrix(PointModel keyPoint)
        {
            rt_data = new double[4, 1];
            if (keyPoint != null)
            {
                rt_data[0, 0] = keyPoint.x;
                rt_data[1, 0] = keyPoint.y;
                rt_data[2, 0] = keyPoint.z;
                rt_data[3, 0] = 1;
            }
            else
                throw new Exception("Input keyPoint error");
        }

        /// <summary>
        ///                   x                                 x
        ///将关键点相对坐标 [ y ]转化成绝对坐标的齐次矩阵形式 [ y ]
        ///                   z                                 z
        ///                                                     1
        /// </summary>
        /// <param name="keyPoint"></param>
        /// <param name="cuboidSize"></param>
        public RTMatrix(PointModel keyPoint, CuboidSize cuboidSize)
        {
            rt_data = new double[4, 1];
            if (keyPoint != null)
            {
                rt_data[0, 0] = keyPoint.x * cuboidSize.l;
                rt_data[1, 0] = keyPoint.y * cuboidSize.w;
                rt_data[2, 0] = keyPoint.z * cuboidSize.h;
                rt_data[3, 0] = (double)1;
            }
            else
                throw new Exception("Input keyPoint error");
        }
        public RTMatrix(CuboidSize cuboidSize, PointModel keyPoint)
        {
            rt_data = new double[4, 1];
            if (keyPoint != null)
            {
                rt_data[0, 0] = keyPoint.x * cuboidSize.l;
                rt_data[1, 0] = keyPoint.y * cuboidSize.w;
                rt_data[2, 0] = keyPoint.z * cuboidSize.h;
                rt_data[3, 0] = (double)1;
            }
            else
                throw new Exception("Input keyPoint error");
        }
        public RTMatrix(PointModel keyPoint, CylinderSize cylinderSize)
        {
            rt_data = new double[4, 1];
            if (keyPoint != null)
            {
                rt_data[0, 0] = keyPoint.x * cylinderSize.r * 2;
                rt_data[1, 0] = keyPoint.y * cylinderSize.r * 2;
                rt_data[2, 0] = keyPoint.z * cylinderSize.h;
                rt_data[3, 0] = (double)1;
            }
            else
                throw new Exception("Input keyPoint error");
        }
        public RTMatrix(CylinderSize cylinderSize, PointModel keyPoint)
        {
            rt_data = new double[4, 1];
            if (keyPoint != null)
            {
                rt_data[0, 0] = keyPoint.x * cylinderSize.r * 2;
                rt_data[1, 0] = keyPoint.y * cylinderSize.r * 2;
                rt_data[2, 0] = keyPoint.z * cylinderSize.h;
                rt_data[3, 0] = (double)1;
            }
            else
                throw new Exception("Input keyPoint error");
        }
        public RTMatrix(PointModel keyPoint, LineSize lineSize)
        {
            rt_data = new double[4, 1];
            if (keyPoint != null)
            {
                rt_data[0, 0] = keyPoint.x;
                rt_data[1, 0] = keyPoint.y;
                rt_data[2, 0] = keyPoint.z * lineSize.l;
                rt_data[3, 0] = (double)1;
            }
            else
                throw new Exception("Input keyPoint error");
        }
        public RTMatrix(LineSize lineSize, PointModel keyPoint)
        {
            rt_data = new double[4, 1];
            if (keyPoint != null)
            {
                rt_data[0, 0] = keyPoint.x;
                rt_data[1, 0] = keyPoint.y;
                rt_data[2, 0] = keyPoint.z * lineSize.l;
                rt_data[3, 0] = (double)1;
            }
            else
                throw new Exception("Input keyPoint error");
        }


        //算法坐标系旋转平移矩阵   旋转顺序为Z-X-Y,详细推导见《Unity3D几何体坐标旋转计算》
        //COS(Y)*COS(Z)-SIN(X)*SIN(Y)*SIN(Z)    COS(Y)*SIN(Z)+SIN(X)*SIN(Y)*COS(Z)    -COS(X)*SIN(Y)    Position.X
        //-COS(X)*SIN(Z)                        COS(X)*COS(Z)                         SIN(X)            Position.Y
        //SIN(Y)*COS(Z)+SIN(X)*COS(Y)*SIN(Z)    SIN(Y)*SIN(Z)-SIN(X)*COS(Y)*COS(Z)    COS(X)*COS(Y)     Position.Z
        //0	                                    0	                                    0	                1

        /// <summary>
        /// 构造 4*4 大小的旋转平移矩阵 
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <param name="Position"></param>
        public RTMatrix(AngleModel rotationAngle, PointModel Position)
        {           
            if (rotationAngle != null && Position != null)
            {
                rt_data = new double[4, 4];

                //角度转化为弧度 公式: 弧度 = （角度 * PI）/ 180
                AngleModel angle = new AngleModel((float)(rotationAngle.x * Math.PI / onehe), (float)(rotationAngle.y * Math.PI / onehe), (float)(rotationAngle.z * Math.PI / onehe));

                //计算sin和cos值
                double sx = Math.Sin(angle.x);
                double sy = Math.Sin(angle.y);
                double sz = Math.Sin(angle.z);
                double cx = Math.Cos(angle.x);
                double cy = Math.Cos(angle.y);
                double cz = Math.Cos(angle.z);

                //旋转平移矩阵第一行;
                rt_data[0, 0] = cy * cz - sx * sy * sz;
                rt_data[0, 1] = cy * sz + sx * sy * cz;
                rt_data[0, 2] = -cx * sy;
                rt_data[0, 3] = Position.x;

                //旋转平移矩阵第二行;
                rt_data[1, 0] = -cx * sz;
                rt_data[1, 1] = cx * cz;
                rt_data[1, 2] = sx;
                rt_data[1, 3] = Position.y;

                //旋转平移矩阵第三行;
                rt_data[2, 0] = sy * cz + sx * cy * sz;
                rt_data[2, 1] = sy * sz - sx * cy * cz;
                rt_data[2, 2] = cx * cy;
                rt_data[2, 3] = Position.z;

                //旋转平移矩阵第四行;
                rt_data[3, 0] = 0;
                rt_data[3, 1] = 0;
                rt_data[3, 2] = 0;
                rt_data[3, 3] = 1;
            }
            else
                throw new Exception("Input rotationAngle or Position error");
        }

        public RTMatrix(PointModel Position, AngleModel rotationAngle)
        {
            if (rotationAngle != null && Position != null)
            {
                rt_data = new double[4, 4];

                //角度转化为弧度 公式: 弧度 = （角度 * PI）/ 180
                AngleModel angle = new AngleModel((float)(rotationAngle.x * Math.PI / onehe), (float)(rotationAngle.y * Math.PI / onehe), (float)(rotationAngle.z * Math.PI / onehe));

                //计算角度的sin和cos值
                double sx = Math.Sin(angle.x);
                double sy = Math.Sin(angle.y);
                double sz = Math.Sin(angle.z);
                double cx = Math.Cos(angle.x);
                double cy = Math.Cos(angle.y);
                double cz = Math.Cos(angle.z);

                //旋转平移矩阵第一行;
                rt_data[0, 0] = cy * cz - sx * sy * sz;
                rt_data[0, 1] = cy * sz + sx * sy * cz;
                rt_data[0, 2] = -cx * sy;
                rt_data[0, 3] = Position.x;

                //旋转平移矩阵第二行;
                rt_data[1, 0] = -cx * sz;
                rt_data[1, 1] = cx * cz;
                rt_data[1, 2] = sx;
                rt_data[1, 3] = Position.y;

                //旋转平移矩阵第三行;
                rt_data[2, 0] = sy * cz + sx * cy * sz;
                rt_data[2, 1] = sy * sz - sx * cy * cz;
                rt_data[2, 2] = cx * cy;
                rt_data[2, 3] = Position.z;

                //旋转平移矩阵第四行;
                rt_data[3, 0] = 0;
                rt_data[3, 1] = 0;
                rt_data[3, 2] = 0;
                rt_data[3, 3] = 1;
            }
            else
                throw new Exception("Input rotationAngle or Position error");
        }

        //绕轴旋转 (暂未使用)
        //public RTMatrix(AxisConnector axis)
        //{
        //    //axis.direction 中x,y,z值为1，表示绕该轴旋转;目前只支持绕单轴旋转，即 x,y,z 只能有一项为1，剩余两项为 0；
        //    //如 axis.direction.x = 0, axis.direction.y = 0, axis.direction.z = 1 时，offsetAngle表示绕 Z 轴旋转的角度；

        //    if (axis.direction.x != on && axis.direction.y != on && axis.direction.z != on)
        //    {
        //        throw new Exception("AxisConnector error");
        //    }

        //    rt_data = new double[fo, fo];

        //    //绕x轴旋转
        //    if (axis.direction.x == on && axis.direction.y == ze && axis.direction.z == ze)
        //    {
        //        AngleModel angle = new AngleModel((float)(axis.offsetAngle * Math.PI / 180), ze, ze);
        //        rt_data[ze, ze] = on;
        //        rt_data[on, on] = Math.Cos(angle.x);
        //        rt_data[on, tw] = Math.Sin(angle.x);
        //        rt_data[tw, on] = -rt_data[on, tw];
        //        rt_data[tw, tw] = rt_data[on, on];

        //        rt_data[ze, th] = axis.position.x;
        //        rt_data[on, th] = axis.position.y;
        //        rt_data[tw, th] = axis.position.z;
        //        rt_data[th, th] = on;
        //    }

        //    //绕y轴旋转
        //    if (axis.direction.x == ze && axis.direction.y == on && axis.direction.z == ze)
        //    {
        //        AngleModel angle = new AngleModel(ze, (float)(axis.offsetAngle * Math.PI / 180), ze);
        //        rt_data[ze, ze] = Math.Cos(angle.y);
        //        rt_data[ze, tw] = Math.Sin(-angle.y);
        //        rt_data[on, on] = on;

        //        rt_data[tw, ze] = -rt_data[ze, tw];
        //        rt_data[tw, tw] = rt_data[ze, ze];

        //        rt_data[ze, th] = axis.position.x;
        //        rt_data[on, th] = axis.position.y;
        //        rt_data[tw, th] = axis.position.z;
        //        rt_data[th, th] = on;
        //    }

        //    //绕z轴旋转
        //    if (axis.direction.x == ze && axis.direction.y == ze && axis.direction.z == on)
        //    {
        //        AngleModel angle = new AngleModel(ze, ze, (float)(axis.offsetAngle * Math.PI / 180));
        //        rt_data[ze, ze] = Math.Cos(angle.z);
        //        rt_data[ze, on] = Math.Sin(angle.z);
        //        rt_data[tw, tw] = on;

        //        rt_data[on, ze] = -rt_data[ze, on];
        //        rt_data[on, on] = rt_data[ze, ze];

        //        rt_data[ze, th] = axis.position.x;
        //        rt_data[on, th] = axis.position.y;
        //        rt_data[tw, th] = axis.position.z;
        //        rt_data[th, th] = on;
        //    }
        //}

        /*
        public RTMatrix(AxisConnector axis)
        {
            //axis.direction 中x,y,z值为向量的坐标           
            if (axis.direction.x == ze && axis.direction.y == ze && axis.direction.z == ze)
            {
                throw new Exception("AxisConnector error");
            }

            rt_data = new double[fo, fo];

            //1-cos(angle)
            float offangle = axis.offsetAngle * (float)Math.PI / onehe;
            double cs = Math.Cos(offangle);
            double sn = Math.Sin(offangle);
            double c_1 = on - cs;

            //旋转的向量转化为单位向量
            double n = Math.Sqrt(axis.direction.x * axis.direction.x + axis.direction.y * axis.direction.y + axis.direction.z * axis.direction.z);
            double nx = axis.direction.x / n;
            double ny = axis.direction.y / n;
            double nz = axis.direction.z / n;
            double nxz = nx * nz * c_1;
            double nxy = nx * ny * c_1;
            double nyz = ny * nz * c_1;
            double nxs = nx * sn;
            double nys = ny * sn;
            double nzs = nz * sn;

            rt_data[ze, ze] = nx * nx * c_1 + cs;
            rt_data[ze, on] = nxy + nzs;
            rt_data[ze, tw] = nxz - nys;
            rt_data[on, ze] = nxy - nzs;
            rt_data[on, on] = ny * ny * c_1 + cs;
            rt_data[on, tw] = nyz + nxs;
            rt_data[tw, ze] = nxz + nys;
            rt_data[tw, on] = nyz - nxs;
            rt_data[tw, tw] = nz * nz * c_1 + cs;

            rt_data[ze, th] = axis.position.x;
            rt_data[on, th] = axis.position.y;
            rt_data[tw, th] = axis.position.z;
            rt_data[th, th] = on;
        }
        public RTMatrix(FixedConnector fix)
        {
            if (fix != null)
            {
                rt_data = new double[fo, fo];

                rt_data[ze, ze] = on;
                rt_data[on, on] = on;
                rt_data[tw, tw] = on;
                rt_data[th, th] = on;

                rt_data[ze, th] = fix.position.x;
                rt_data[on, th] = fix.position.y;
                rt_data[tw, th] = fix.position.z;
            }
            else
                throw new Exception("Input FixedConnector error");
            
        }
        */


        //矩阵乘法
        public static RTMatrix operator * (RTMatrix lm, RTMatrix rm)
        {

            if (lm.Col != rm.Row)    //异常
            {
                throw new Exception("相乘的两个矩阵的行列数不匹配");
            }

            RTMatrix ret = new RTMatrix(lm.Row, rm.Col);

            for (int i = 0; i < lm.Row; i++)
            {
                for (int j = 0; j < rm.Col; j++)
                {
                    for (int k = 0; k < lm.Col; k++)
                    {
                        ret[i, j] += lm[i, k] * rm[k, j];
                    }
                }
            }
            return ret;
        }

        //复制构造函数
        public RTMatrix(RTMatrix m)
        {
            int row = m.Row;
            int col = m.Col;
            rt_data = new double[row, col];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    rt_data[i, j] = m[i, j];
        }

        public int Row
        {
            get
            {
                return rt_data.GetLength(0);
            }
        }
        //返回列数
        public int Col
        {
            get
            {
                return rt_data.GetLength(1);
            }
        }
        //重载索引
        //存取数据成员

        public double this[int row]
        {
            get
            {
                return rt_data[row, 0];
            }
            set
            {
                rt_data[row, 0] = value;
            }
        }
        
        /// <summary>
        /// 通过下标索引取二维
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public double this[int row, int col]
        {
            get
            {
                return rt_data[row, col];
            }
            set
            {
                rt_data[row, col] = value;
            }
        }


        private double[,] rt_data;
    }
}
