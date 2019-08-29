using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UtilityTool.Helper
{
    /// <summary>
    /// Async Helper
    /// </summary>
    public class AsyncHelper
    {
        private static readonly TaskFactory _factory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        /// <summary>
        /// 執行非同步無回傳值
        /// </summary>
        /// <param name="func"></param>
        public static void RunSync(Func<Task> func)
        {
            _factory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 執行非同步有回傳值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _factory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }

    }
}
