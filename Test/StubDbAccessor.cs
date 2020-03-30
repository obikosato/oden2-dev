using System;
using System.Collections.Generic;
using Common.Tools.Database;

namespace Test
{
    public class StubDbAccessor : IDbAccessor
    {
        private readonly List<Account> accounts;

        public StubDbAccessor(List<Account> accounts) 
        {
            this.accounts = accounts;
        }

        public void ActivateAccount(string id)
        {
            throw new NotImplementedException();
        }

        public Account AddAccount(string id, string password, string accessToken)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccount(string id)
        {
            throw new NotImplementedException();
        }

        public void Dispose() { }

        public List<Account> GetActiveAccounts()
        {
            return accounts;
        }

        public bool IsVerified(string id, string password)
        {
            throw new NotImplementedException();
        }
    }
}
