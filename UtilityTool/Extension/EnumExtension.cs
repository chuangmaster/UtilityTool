using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityTool.Extension
{
    public static class EnumExtension
    {
        /// <summary>
        /// 取得Enum的Attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static A GetAttribute<T, A>(this T e)
        where T : struct
        where A : Attribute
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("Type is not enum");
            }

            var type = e.GetType();
            var member = type.GetMember(e.ToString());
            if (member.Length > 0)
            {
                var attr = member[0].GetCustomAttributes(typeof(A), false);
                return attr[0] as A;
            }
            else
            {
                return null;
            }
        }
    }
}
