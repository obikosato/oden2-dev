using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Tools.LineNotify
{
    public interface ILineMessenger
    {
        public bool SendMessage(string accessToken, string message);
    }
}
