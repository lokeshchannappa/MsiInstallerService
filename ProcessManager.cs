//-----------------------------------------------------------------------
// <copyright file="ProcessManager.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace MSIInstallerService
{
    using System.Diagnostics;

    /// <summary>
    /// Execute file using command.exe
    /// </summary>
    public static class ProcessManager
    {
        /// <summary>
        /// Execute *.msi file using "cmd.exe" in passive mode, which is received from docker
        /// <param name="file">path of the file</param>
        /// <returns>return execution completed status</returns>
        public static bool IsExecuteCompleted(string file)
        {
            bool status = false;
            using (var p = new Process())
            {
                p.StartInfo = new ProcessStartInfo(Constants.Executor, "/c" + file + " /passive")
                {
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    Verb = Constants.RunAs,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                p.Start();
                p.WaitForExit();

                status = p.ExitCode == 0;
            }

            return status;
        }
    }
}
