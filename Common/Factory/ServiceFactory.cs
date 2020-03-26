using Common.Services;
using Common.Tools.Database;
using Common.Tools.LineNotify;
using Common.Tools.WebSite;

namespace Oden.Common.Factory
{
    public class ServiceFactory
    {
        private readonly IDbAccessor dbAccessor;
        private readonly ILineMessenger lineMessenger;
        private readonly IEventInfoConverter eventInfoConverter;

        public ServiceFactory(IDbAccessor dbAccessor = null, IEventInfoConverter eventInfoConverter = null, ILineMessenger lineMessenger = null)
        {
            this.dbAccessor = dbAccessor;
            this.eventInfoConverter = eventInfoConverter;
            this.lineMessenger = lineMessenger;
        }

        public enum ServiceName { REGIST, AUTH, UNREGIST, NOTIFY }

        public IService Create(ServiceName serviceName)
        {
            IService service = null;
            switch (serviceName) {
                case ServiceName.REGIST:
                    service = new RegistService(dbAccessor, lineMessenger);
                    break;
                case ServiceName.AUTH:
                    service = new AuthService(dbAccessor);
                    break;
                case ServiceName.UNREGIST:
                    service = new UnregistService(dbAccessor);
                    break;
                case ServiceName.NOTIFY:
                    service = new NotifyService(dbAccessor, eventInfoConverter, lineMessenger);
                    break;
            }    
            return service;
        }
    }
}
