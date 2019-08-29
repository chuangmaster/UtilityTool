using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityTool.Enum
{
    /// <summary>
    /// enum CacheTypeEnum
    /// </summary>
    public enum CacheTypeEnum
    {
        //絕對效期: 時間到就結束
        AbsoluteExpiration = 1,
        //滑動效期: 設定一段時間，若沒有人使用就清除
        SlidingExpiration = 2
    }
}
