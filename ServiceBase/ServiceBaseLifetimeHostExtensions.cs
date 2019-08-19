//-----------------------------------------------------------------------
// <copyright file="ServiceBaseLifetimeHostExtensions.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace MSIInstallerService.ServiceBase
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Service base lifetime host extensions
    /// </summary>
    public static class ServiceBaseLifetimeHostExtensions
    {
        /// <summary>
        /// Use service base lifetime
        /// </summary>
        /// <param name="hostBuilder">host builder</param>
        /// <returns>returns IHostBuilder object</returns>
        public static IHostBuilder UseServiceBaseLifetime(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) => services.AddSingleton<IHostLifetime, ServiceBaseLifetime>());
        }

        /// <summary>
        /// Run as service
        /// </summary>
        /// <param name="hostBuilder">host builder</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>returns a Task</returns>
        public static Task RunAsServiceAsync(this IHostBuilder hostBuilder, CancellationToken cancellationToken = default)
        {
            return hostBuilder.UseServiceBaseLifetime().Build().RunAsync(cancellationToken);
        }
    }
}
