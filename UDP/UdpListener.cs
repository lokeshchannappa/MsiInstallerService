//-----------------------------------------------------------------------
// <copyright file="UdpListener.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace MSIInstallerService.UDP
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// UDP Listener
    /// </summary>
    public class UdpListener : UdpBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UdpListener" /> class. Listen for any IP, if no IP passed
        /// </summary>
        public UdpListener() : this(new IPEndPoint(IPAddress.Any, 32123))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpListener" /> class.
        /// </summary>
        /// <param name="endpoint">IP endpoint</param>
        public UdpListener(IPEndPoint endpoint)
        {
            this._listenOn = endpoint;
            this.Client = new UdpClient(this._listenOn);
            Socket socket = Client.Client;
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 0);
        }

        /// <summary>
        /// Gets or sets listenOn
        /// </summary>
        private IPEndPoint _listenOn { get; set; }

        /// <summary>
        /// Send replay to client
        /// </summary>
        /// <param name="message">message to replay</param>
        /// <param name="endpoint">IP address</param>
        public void Reply(string message, IPEndPoint endpoint)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length, endpoint);
        }
    }
}
