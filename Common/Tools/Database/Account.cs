using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Tools.Database
{
    public class Account
    {
        public string id { get; set; }
        public string access_token { get; set; }
        public string password { get; set; }
        public int is_valid { get; set; }
    }
}
