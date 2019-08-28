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

        public static void RunSync(Func<Task> func)
        {
            _factory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _factory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }

    }
}
