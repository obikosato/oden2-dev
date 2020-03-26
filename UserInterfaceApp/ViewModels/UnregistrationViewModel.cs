using System;
using System.Text.RegularExpressions;
using Common.Services;
using Common.Tools.Database;
using Oden.Common.Factory;

namespace UserInterfaceApp.ViewModels
{
    public class UnregistrationViewModel
    {
        public string Id { get; set; }
        public string Pwd { get; set; }
        public string Message { get; set; }

        public bool DoUnregist()
        {
            string[] inputData = { Id, Pwd };
            IDbAccessor db = new DbAccessor();
            ServiceFactory factory = new ServiceFactory(dbAccessor:db);

            try
            {
                using IService service = factory.Create(ServiceFactory.ServiceName.UNREGIST);
                var res = service.DoService(inputData);

                if (res.Result["MESSAGE"] != null)
                {
                    Message = res.Result["MESSAGE"].ToString();
                }
                return res.IsSuccessed;
            }
            catch (Exception e)
            {
                Message = Messages.GetErrorMessage(e);
                return false;
            }
        }

        public bool InputIsValid()
        {
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(Pwd))
            {
                Message = Messages.EM11;
                return false;
            }
            else if (!Regex.IsMatch(Id, "^[0-9A-Za-z]{1,16}$") ||
                !Regex.IsMatch(Pwd, "^[0-9A-Za-z]{8,16}$"))
            {
                Message = Messages.EM12;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
