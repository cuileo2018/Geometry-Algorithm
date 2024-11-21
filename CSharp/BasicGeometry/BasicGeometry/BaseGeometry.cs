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
    /// 基本几何体
    /// </summary>
    public class BaseGeometry
    {
        /// <summary>
        /// 类型
        /// </summary>
        private string _type;
        /// <summary>
        /// 唯一标识
        /// </summary>
        private string _id;

        /// <summary>
        /// 中心点
        /// </summary>
        private PointModel _centerPoint;

        /// <summary>
        /// 旋转角度
        /// </summary>
        private AngleModel _rotateAngle;

        /// <summary>
        /// 关键点
        /// </summary>
        protected List<PointGeometry> _keyList;

        private object _fsize;       

        public string id
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
                return _centerPoint;
            }

            set
            {
                _centerPoint = value;
            }
        }

        public AngleModel rotation
        {
            get
            {
                return _rotateAngle;
            }

            set
            {
                _rotateAngle = value;
            }
        }

        public List<PointGeometry> keypoints
        {
            get
            {
                return _keyList;
            }
        }

        protected object FSize
        {
            get
            {
                return _fsize;
            }

            set
            {
                _fsize = value;
            }
        }

        public string type
        {
            get
            {
                return _type;
            }           
        }

        /// <summary>
        /// 基本集合体
        /// </summary>
        /// <param name="_id">唯一标识</param>
        /// <param name="_centerPoint">中心点</param>
        /// <param name="_rotateAngle">旋转角度</param>
        /// <param name="_keyList">关键点</param>
        public BaseGeometry(PointModel _centerPoint, AngleModel _rotateAngle, string _type)
        {            
            this._id = Guid.NewGuid().ToString();
            this.position = _centerPoint;
            this.rotation = _rotateAngle;
            _keyList = new List<PointGeometry>();           
            this.FSize = null;
            this._type = _type;
        }

        /// <summary>
        /// 生成关键点
        /// </summary>
        virtual public void CreateKeyPoint()
        {
            if (_keyList == null)
            {
                _keyList = new List<PointGeometry>();
            }
            else
            {
                _keyList.Clear();
            }
        }


        /// <summary>
        /// 位移
        /// </summary>
        virtual public void Shifting()
        {

        }

        /// <summary>
        /// 旋转
        /// </summary>
        virtual public void Rotate()
        {

        }

        /// <summary>
        /// 同另一个几何体距离
        /// </summary>
        /// <returns></returns>
        virtual public float ToGeometryDistance()
        {
            return 0;
        }
    }
}
