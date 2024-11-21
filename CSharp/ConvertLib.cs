//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2019 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2019.6.14 崔艳龙 创建
//============================================

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace GeoLib
{
    public static class ConvertLib
    {
        public static bool GetBoolean(object val)
        {
            return GetBoolean(val, false);
        }
        public static bool GetBoolean(object val, bool defaultResult)
        {
            if (val == null || Convert.IsDBNull(val)) return defaultResult;
            switch (Convert.ToString(val).Trim().ToUpper())
            {
                case "T":
                case "Y":
                case "TRUE":
                case "YES":
                case "1":
                    return true;

                case "F":
                case "N":
                case "FALSE":
                case "NO":
                case "0":
                default:
                    return false;
            }
        }

        public static decimal GetDecimal(object val)
        {
            if (val == null || Convert.IsDBNull(val)) return 0M;

            decimal result = 0M;
            bool sucess = Decimal.TryParse(Convert.ToString(val), out result);
            return sucess ? result : 0M;
        }

        public static int GetInt32(object val)
        {
            return GetInt32(val, 0);
        }

        public static int GetInt32(object val, int defaultValue)
        {
            if (val == null || Convert.IsDBNull(val)) return defaultValue;

            int result = defaultValue;
            bool sucess = Int32.TryParse(Convert.ToString(val), out result);
            return sucess ? result : defaultValue;
        }

        public static double GetDouble(object val)
        {
            if (val == null || Convert.IsDBNull(val)) return 0;

            double result = 0;
            bool sucess = Double.TryParse(Convert.ToString(val), out result);
            return sucess ? result : 0;
        }

        public static float GetFloat(object val)
        {
            if (val == null || Convert.IsDBNull(val)) return 0;

            float result = 0;
            bool sucess = Single.TryParse(Convert.ToString(val), out result);
            return sucess ? result : 0;
        }

        public static DateTime GetDateTime(object val)
        {
            return GetDateTime(val, DateTime.Now);
        }

        public static DateTime GetDateTime(object val, DateTime defaultValue)
        {
            if (val == null || Convert.IsDBNull(val)) return defaultValue;

            DateTime result = defaultValue;
            bool sucess = DateTime.TryParse(Convert.ToString(val), out result);
            return sucess ? result : defaultValue;
        }

        public static string GetString(object val)
        {
            return GetString(val, "");
        }

        public static string GetString(object val, string defaultValue)
        {
            if (val == null || Convert.IsDBNull(val)) return defaultValue;

            return Convert.ToString(val);
        }

        /// <summary>
        /// 对象转换成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject">需要格式化的对象</param>
        /// <returns>Json字符串</returns>
        public static string DataContractJsonSerialize(this object jsonObject)
        {
            if (jsonObject == null) return null;
            return JsonConvert.SerializeObject(jsonObject);
        }

        /// <summary>
        /// json字符串转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">要转换成对象的json字符串</param>
        /// <returns></returns>
        public static T DataContractJsonDeserialize<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>    
        /// 将集合类转换成DataTable    
        /// </summary>    
        /// <param name="list">集合</param>    
        /// <returns></returns>    
        private static DataTable ToDataTableTow(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                foreach (object t in list)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(t, null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>    
        /// DataTable 转换为List 集合    
        /// </summary>    
        /// <typeparam name="TResult">类型</typeparam>    
        /// <param name="dt">DataTable</param>    
        /// <returns></returns>    
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            //创建一个属性的列表    
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口    

            Type t = typeof(T);

            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表     
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });

            //创建返回的集合    

            List<T> oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例    
                T ob = new T();
                //找到对应的数据  并赋值    
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.    
                oblist.Add(ob);
            }
            return oblist;
        }

        /// <summary>
        /// DataRow转换为DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strWhere">筛选的条件</param>
        /// <returns></returns>
        public static DataTable SreeenDataTable(DataTable dt, string strWhere)
        {
            if (dt.Rows.Count <= 0) return dt;        //当数据为空时返回
            DataTable dtNew = dt.Clone();         //复制数据源的表结构
            DataRow[] dr = dt.Select(strWhere);  //strWhere条件筛选出需要的数据！
            for (int i = 0; i < dr.Length; i++)
            {
                dtNew.Rows.Add(dr[i].ItemArray);  // 将DataRow添加到DataTable中
            }
            return dtNew;
        }


        public static DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone();  // 复制DataRow的表结构  
            foreach (DataRow row in rows)
                tmp.Rows.Add(row.ItemArray);  // 将DataRow添加到DataTable中  
            return tmp;
        }
    }
}
