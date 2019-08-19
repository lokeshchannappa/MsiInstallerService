//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace MSIInstallerService
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MSIInstallerService.ServiceBase;

    /// <summary>
    /// Main class of the service
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main Method of the service to start service.
        /// </summary>
        /// <param name="args">list of arguments</param>
        /// <returns>returns a Task</returns>
        private static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<MsiInstallerService>();
                });

            if (isService)
            {
                await builder.RunAsServiceAsync();
            }
            else
            {
                await builder.RunConsoleAsync();
            }
        }
    }
}
