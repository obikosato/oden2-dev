using Common.Services;
using Common.Tools.Database;
using Common.Tools.LineNotify;
using Common.Tools.WebSite;

namespace BlazorApp0124.Models.Factory
{
    public class ServiceFactory
    {
        private readonly IDbAccessor da;
        private readonly Common.Tools.LineNotify.ILineMessenger lm;
        private readonly IEventInfoConverter ei;

        public ServiceFactory(IDbAccessor da, IEventInfoConverter ei, ILineMessenger lm)
        {
            this.da = da;
            this.ei = ei;
            this.lm = lm;
        }

        public ServiceFactory(IDbAccessor da, ILineMessenger lm)
        {
            this.da = da;
            this.lm = lm;
        }

        public ServiceFactory(IDbAccessor da)
        {
            this.da = da;
        }

        public enum ServiceName { REGIST, AUTH, UNREGIST, NOTIFY }

        public IService Create(ServiceName serviceName)
        {
            IService service = null;
            switch (serviceName) {
                case ServiceName.REGIST:
                    service = new RegistService(da, lm);
                    break;
                case ServiceName.AUTH:
                    service = new AuthService(da);
                    break;
                case ServiceName.UNREGIST:
                    service = new UnregistService(da);
                    break;
                case ServiceName.NOTIFY:
                    service = new NotifyService(da, ei, lm);
                    break;
            }    
            return service;
        }
    }
}
