//-----------------------------------------------------------------------
// <copyright file="MsiInstallerService.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace MSIInstallerService
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using MSIInstallerService.UDP;
    using MSIInstallerService.ServiceBase;

    /// <summary>
    /// This receives the command and execute the MSI
    /// </summary>
    public class MsiInstallerService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            UdpListener listener = new UdpListener();
            while (!cancellationToken.IsCancellationRequested)
            {
                Response response = await listener.Receive();
                string file = Path.Combine(Constants.VolumePath, response.Message);
                if (ProcessManager.IsExecuteCompleted(file))
                {
                    listener.Reply("Success", response.Sender);
                }
                else
                {
                    listener.Reply("Failure", response.Sender);
                }
            }
        }
    }
}
