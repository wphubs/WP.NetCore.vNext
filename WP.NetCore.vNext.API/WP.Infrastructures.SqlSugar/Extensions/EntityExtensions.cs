using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Infrastructures.SqlSugar.Extensions
{
    public static class EntityExtensions
    {
        public static Dictionary<string, string> GetProperties<T>(T t)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (t == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return null;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;                                                  //实体类字段名称
                string value = item.GetValue(t, null) + "";                //该字段的值

                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ret.Add(name, value);        //在此可转换value的类型
                }
            }

            return ret;
        }

        public static List<string> GetPropertiesUpdateArrary<T>(T t)
        {
            var arrary = GetPropertiesArrary(t);
            arrary.Add("ModifyBy");
            arrary.Add("ModifyTime");
            return arrary;
        }


        public static List<string> GetPropertiesArrary<T>(T t)
        {
            List<string> property = new List<string>();
            if (t == null)
            {
                return property;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return property;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                    property.Add(item.Name);
            }
            return property;
        }

        public static string[] GetPropertiesArrary<T>(T t, List<string> ignoreColumns)
        {
            List<string> property = new List<string>();
            if (t == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return null;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                if (!ignoreColumns.Contains(item.Name))
                    property.Add(item.Name);
            }

            return property.ToArray();
        }

    }
}
