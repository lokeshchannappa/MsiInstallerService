//-----------------------------------------------------------------------
// <copyright file="BackgroundService.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace MSIInstallerService.ServiceBase
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// This will take care of start and stop service
    /// </summary>
    public abstract class BackgroundService : IHostedService, IDisposable
    {
        /// <summary>
        ///  execute task instance
        /// </summary>
        private Task executingTask;

        /// <summary>
        /// cancellation token instance
        /// </summary>
        private readonly CancellationTokenSource cancellationToken = new CancellationTokenSource();

        /// <summary>
        /// Execute task
        /// </summary>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>returns task</returns>
        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Start task execution
        /// </summary>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Returns a task</returns>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            // Store the task we're executing
            executingTask = ExecuteAsync(this.cancellationToken.Token);

            // If the task is completed then return it, this will bubble cancellation and failure to the caller
            if (executingTask.IsCompleted)
            {
                return executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stop execution based on cancellation token
        /// </summary>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Returns a task</returns>
        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (executingTask == null)
            {
                return;
            }
            try
            {
                // Signal cancellation to the executing method
                this.cancellationToken.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        /// <summary>
        /// Cancel task 
        /// </summary>
        public void Dispose()
        {
            cancellationToken.Cancel();
        }
    }
}
