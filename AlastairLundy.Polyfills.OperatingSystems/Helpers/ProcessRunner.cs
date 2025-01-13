using System.Diagnostics;

namespace AlastairLundy.Polyfills.OperatingSystems.Helpers
{
    internal class ProcessRunner
    {
        internal static Process CreateProcess(string targetFileName, string arguments)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = targetFileName,
                Arguments = arguments,
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process output = new Process
            {
                StartInfo = processStartInfo
            };

            return output;
        }

        internal static string RunProcess(Process process)
        {
            process.Start();

            process.WaitForExit();

            return process.StandardOutput.ReadToEnd();
        }
    }
}