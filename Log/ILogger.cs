using System;
namespace Log
{
    public interface ILogger
    {
        public void Log(string id, string message);
    }
}
