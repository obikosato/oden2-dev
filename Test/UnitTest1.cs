using Common.Tools.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using IDbAccessor db = new DbAccessor();
            var list = db.GetActiveAccounts();
            Console.WriteLine("aa");

            foreach (Account a in list)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", a.id, a.access_token, a.password, a.is_valid);
            }

        }
    }
}
