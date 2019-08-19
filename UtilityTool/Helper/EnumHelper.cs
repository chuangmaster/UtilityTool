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
    }
}
