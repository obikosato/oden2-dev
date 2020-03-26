using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Tools.Database
{
    public interface IDbAccessor : IDisposable
    {
        public Account AddAccount(string id, string password, string accessToken);
        
        public void DeleteAccount(string id);
        
        public bool IsVerified(string id, string password);

        public void ActivateAccount(string id);

        public List<Account> GetActiveAccounts();
    }
}
