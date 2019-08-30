using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityTool.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        /// 取得所有Enum成員的Description attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetAllMemberDescription<T>(T t)
        where T : Type
        {
            if (!t.IsEnum)
            {
                throw new Exception("Is Not Enum");
            }
            var result = new Dictionary<int, string>();
            
            var enumArr = System.Enum.GetValues(t);
            foreach (var e in enumArr)
            {
                var members = t.GetMember(e.ToString());
                foreach (var m in members)
                {
                    var attr = m.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attr != null)
                    {
                        result.Add((int)e, attr.ToString());
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 將字串解析成Enum Type
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="key">欲轉換的key</param>
        /// <param name="ignoreCase">是否忽略字串大小寫</param>
        /// <param name="defaultType">轉換失敗轉換的預設型別</param>
        /// <returns></returns>
        public static T ParseTo<T>(string key, bool ignoreCase, T defaultType)
            where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("Target is not enum type");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("Key is null or empty");
            }
            var success = System.Enum.TryParse<T>(key, ignoreCase, out T result);
            if (success)
            {
                return result;
            }
            else
            {
                return defaultType;
            }
        }
    }
}
