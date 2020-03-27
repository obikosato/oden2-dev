using System.Collections.Generic;
using Common.Tools.LineNotify;

namespace Test
{
    public class MockLineMessenger : ILineMessenger
    {
        public List<(string token, string message)> InputList { get; } = new List<(string token, string message)>();

        public bool SendMessage(string token, string message)
        {
            if(token == "false") 
            {
                return false;
            }
            InputList.Add((token, message));
            return true;
        }
    }
}
