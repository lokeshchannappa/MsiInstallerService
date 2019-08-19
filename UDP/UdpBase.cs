//-----------------------------------------------------------------------
// <copyright file="UdpBase.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace MSIInstallerService.UDP
{
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Create instance of UDP client and receive message from sender
    /// </summary>
    public abstract class UdpBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UdpBase" /> class.
        /// </summary>
        protected UdpBase()
        {
            this.Client = new UdpClient();
        }

        /// <summary>
        /// Gets or sets client
        /// </summary>
        protected UdpClient Client { get; set; }

        /// <summary>
        /// Receive message from Sender
        /// </summary>
        /// <returns>Response Object</returns>
        public async Task<Response> Receive()
        {
            var result = await this.Client.ReceiveAsync();
            return new Response()
            {
                Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }
    }
}
