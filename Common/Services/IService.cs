using System;

namespace Common.Services
{
    public interface IService : IDisposable
    {
        public ServiceResults DoService(string[] inputData);
    }
}
