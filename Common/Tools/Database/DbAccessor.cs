using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Common.Tools.Database
{
    public class DbAccessor : IDbAccessor
    {
        private readonly OdenDbContext con;
        public DbAccessor()
        {
            con = new OdenDbContext();
        }
        public void Dispose()
        {
            con.Dispose();
        }

        public Account AccountAddAccount(string id, string password, string accessToken)
        {
            Account newAccount = new Account
            {
                id = id,
                access_token = accessToken,
                password = password,
                is_valid = 0
            };
            con.Accounts.Add(newAccount);
            con.SaveChanges();
            return con.Accounts.AsNoTracking().FirstOrDefault(x => x.id == id);
        }

        public void ActivateAccount(string id)
        {
            Account account = con.Accounts.FirstOrDefault(x => x.id == id);
            if (account != null)
            {
                account.is_valid = 1;
                con.SaveChanges();
            }
        }

        public void DeleteAccount(string id)
        {
            Account token = con.Accounts.FirstOrDefault(x => x.id == id);
            if (token != null)
            {
                con.Accounts.Remove(token);
                con.SaveChanges();
            }
        }

        public List<Account> GetActiveAccounts()
        {
            var ret = con.Accounts.AsNoTracking();
            return ret.Where(x => x.is_valid == 1).ToList();
        }

        public bool IsVerified(string id, string password)
        {
            return con.Accounts.AsNoTracking().FirstOrDefault(x => x.id == id && x.password == password) != null;
        }
    }
}
