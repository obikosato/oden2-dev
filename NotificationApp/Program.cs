using System;
using Common.Services;
using Common.Tools.Database;
using Common.Tools.LineNotify;
using Common.Tools.WebSite;
using Oden.Common.Factory;

namespace NotificationApp
{
    public class Program
    {
        public static void Main()
        {
            IDbAccessor db = new DbAccessor();
            IEventInfoConverter ei = new EventInfoConverter();
            ILineMessenger lm = new LineMessenger();
            ServiceFactory factory = new ServiceFactory(db, ei, lm);

            try
            {
                using IService service = factory.Create(ServiceFactory.ServiceName.NOTIFY);
                string[] inputData = { };
                service.DoService(inputData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
