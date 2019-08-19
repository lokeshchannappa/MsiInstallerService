//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace MSIInstallerService
{
    /// <summary>
    /// This class will hold all constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Docker volume path
        /// </summary>
        public static readonly string VolumePath = @"C:\ProgramData\Docker\volumes\data1\_data\";

        /// <summary>
        /// Executor name
        /// </summary>
        public static readonly string Executor = "cmd.exe";

        /// <summary>
        /// runas command
        /// </summary>
        public static readonly string RunAs = "runas";
    }
}
