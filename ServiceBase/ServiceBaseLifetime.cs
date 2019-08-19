//-----------------------------------------------------------------------
// <copyright file="ServiceBaseLifetime.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace MSIInstallerService.ServiceBase
{
    using System;
    using System.ServiceProcess;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Service base lifetime instance
    /// </summary>
    public class ServiceBaseLifetime : ServiceBase, IHostLifetime
    {
        /// <summary>
        /// set instance of TaskCompletionSource.
        /// </summary>
        private readonly TaskCompletionSource<object> delayStart = new TaskCompletionSource<object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBaseLifetime" /> class.
        /// </summary>
        /// <param name="applicationLifetime">application Lifetime</param>
        public ServiceBaseLifetime(IApplicationLifetime applicationLifetime)
        {
            ApplicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        }

        private IApplicationLifetime ApplicationLifetime { get; }

        /// <summary>
        /// Wait for start
        /// </summary>
        /// <param name="cancellationToken">cancellation token</param>
        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => delayStart.TrySetCanceled());
            ApplicationLifetime.ApplicationStopping.Register(Stop);

            new Thread(Run).Start(); // Otherwise this would block and prevent IHost.StartAsync from finishing.
            return delayStart.Task;
        }

        private void Run()
        {
            try
            {
                Run(this); // This blocks until the service is stopped.
                delayStart.TrySetException(new InvalidOperationException("Stopped without starting"));
            }
            catch (Exception ex)
            {
                delayStart.TrySetException(ex);
            }
        }

        /// <summary>
        /// Stop Task
        /// </summary>
        /// <param name="cancellationToken">cancellation Token</param>
        /// <returns>returns a Task</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Stop();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called by base.Run when the service is ready to start.
        /// </summary>
        /// <param name="args">array of arguments</param>
        protected override void OnStart(string[] args)
        {
            delayStart.TrySetResult(null);
            base.OnStart(args);
        }

        /// <summary>
        ///  Called by base.Stop. This may be called multiple times by service Stop, ApplicationStopping, and StopAsync.
        ///  That's OK because StopApplication uses a CancellationTokenSource and prevents any recursion.
        /// </summary>
        protected override void OnStop()
        {
            ApplicationLifetime.StopApplication();
            base.OnStop();
        }
    }
}
