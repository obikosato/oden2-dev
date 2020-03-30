using System;
using System.IO;

namespace Log
{
    public class Logger : ILogger
    {
        public void Log(string id = "None", string message = "No data.")
        {
            string path = @"oden_log.csv";
            using StreamWriter sw = File.AppendText(path);
            sw.WriteLine("{0},{1},{2}", DateTime.Now, id, message);
        }
    }
}
