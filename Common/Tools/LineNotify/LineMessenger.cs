using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Common.Tools.LineNotify
{
    public class LineMessenger : ILineMessenger
    {        
        private readonly string pythonInterpreterPath = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python37_64\python.exe";
        private readonly string pythonScriptPath;

        public LineMessenger(string pyscript = @"C:\Users\z00s600151\source\repos\BlazorApp0124\BlazorApp0124.Models\Tools\LineMessenger.py")
        {
            pythonScriptPath = pyscript;
        }
        public bool SendMessage(string accessToken, string message)
        {
            if (File.Exists(pythonScriptPath))
            {
                var arguments = new List<string>
                {
                    pythonScriptPath,
                    accessToken,
                    message.Replace(" ", "　")
                };

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo(pythonInterpreterPath)
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        Arguments = string.Join(" ", arguments),
                    },
                };

                try
                {
                    process.Start();

                    var sr = process.StandardOutput;
                    string status = sr.ReadLine();

                    process.WaitForExit();
                    process.Close();

                    return Equals(status, "200");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Generic Exception Handler: {e}");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
