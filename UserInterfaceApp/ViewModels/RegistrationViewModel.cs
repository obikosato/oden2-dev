using System;
using System.Text.RegularExpressions;
using Common.Services;
using Common.Tools.Database;
using Common.Tools.LineNotify;
using Oden.Common.Factory;

namespace UserInterfaceApp.ViewModels
{
    public class RegistrationViewModel
    {
        //parameters for input 
        public string Id { get; set; }
        public string Token { get; set; }
        public string Pwd { get; set; }
        public string AuthIn { get; set; }

        //parameters for output
        public string Message { get; set; } = string.Empty;

        private string TokenAuthSend = string.Empty;

        public bool DoRegist()
        {
            string[] inputData = { Id, Pwd, Token };
            IDbAccessor db = new DbAccessor();
            ILineMessenger lm = new LineMessenger();
            ServiceFactory factory = new ServiceFactory(db, lm);

            try
            {
                using IService service = factory.Create(ServiceFactory.ServiceName.REGIST);
                var res = service.DoService(inputData);

                if (res.Result["MESSAGE"] != null)
                {
                    Message = res.Result["MESSAGE"].ToString();
                }

                if (res.IsSuccessed)
                {
                    TokenAuthSend = res.Result["AUTHNO"].ToString();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Message = Messages.GetErrorMessage(e);
                return false;
            }
        }

        public bool DoAuth()
        {
            string[] inputData = { Id, AuthIn, TokenAuthSend };
            IDbAccessor db = new DbAccessor();
            ServiceFactory factory = new ServiceFactory(db);

            try
            {
                using IService service = factory.Create(ServiceFactory.ServiceName.AUTH);
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
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(Token) || string.IsNullOrEmpty(Pwd))
            {
                Message = Messages.EM11;
                return false;
            }
            else if (!Regex.IsMatch(Id, "^[0-9A-Za-z]{1,16}$") ||
                !Regex.IsMatch(Pwd, "^[0-9A-Za-z]{8,16}$") ||
                !Regex.IsMatch(Token, "^[0-9A-Za-z]{43}$"))
            {
                Message = Messages.EM12;
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool InputNoIsValid()
        {
            if (string.IsNullOrEmpty(AuthIn))
            {
                Message = Messages.EM11;
                return false;
            }
            else if (!Regex.IsMatch(AuthIn, "^[0-9]{4}$"))
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
