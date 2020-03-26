using System.Collections.Generic;

namespace Common.Services
{
    public class ServiceResults
    {
        public bool IsSuccessed { get; set; } = false;
        public Dictionary<string, string> Result { get; set; } = new Dictionary<string, string>();

    }
}
