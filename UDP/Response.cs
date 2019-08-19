//-----------------------------------------------------------------------
// <copyright file="Response.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace MSIInstallerService.UDP
{
    using System.Net;

    /// <summary>
    /// Response class holds sender and message properties
    /// </summary>
    public struct Response
    {
        /// <summary>
        /// Gets or sets sender
        /// </summary>
        public IPEndPoint Sender { get; set; }

        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message { get; set; }
    }
}
