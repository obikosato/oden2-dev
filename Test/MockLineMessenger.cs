using System.Collections.Generic;
using Common.Tools.LineNotify;

namespace Test
{
    public class MockLineMessenger : ILineMessenger
    {
        public List<(string accessToken, string message)> InputList { get; } = new List<(string accessToken, string message)>();

        public bool SendMessage(string accessToken, string message)
        {
            if(accessToken == "false") 
            {
                return false;
            }
            InputList.Add((accessToken, message));
            return true;
        }
    }
}