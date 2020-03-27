using System;
using System.Collections.Generic;
using Common.Tools.Database;

namespace Test
{
    public class StubDbAccessor : IDbAccessor
    {
        private readonly List<Account> tokens;

        public StubDbAccessor(List<Account> tokens) 
        {
            this.tokens = tokens;
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
            return tokens;
        }

        public bool IsVerified(string id, string password)
        {
            throw new NotImplementedException();
        }
    }
}
