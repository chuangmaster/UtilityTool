using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityTool.Enum;

namespace UtilityTool.Cache.Interface
{
    public interface ICache
    {

        /// <summary>
        /// 儲存快取
        /// </summary>
        /// <param name="key">快取Key</param>
        /// <param name="data">儲存資料</param>
        /// <param name="type">Cache型態</param>
        /// <param name="seconds">秒數</param>
        void Save(string key, object data, CacheTypeEnum type, int seconds);

        /// <summary>
        /// 清除快取
        /// </summary>
        /// <param name="key"></param>
        void Clear(string key);

        /// <summary>
        /// 取得快取內容
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TResult Get<TResult>(string key);
    }
}
