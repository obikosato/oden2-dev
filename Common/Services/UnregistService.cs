using System;
using Common.Tools.Database;

namespace Common.Services
{
    public class UnregistService : IService
    {
        private readonly IDbAccessor dbAccessor;
        public UnregistService(IDbAccessor da)
        {
            dbAccessor = da;
        }
        public void Dispose()
        {
            dbAccessor.Dispose();
        }

        public ServiceResults DoService(string[] inputData)
        {
            var output = new ServiceResults();
            output.Result.Add("MESSAGE", string.Empty);

            if (inputData.Length == 2)
            {
                try
                {
                    if (dbAccessor.IsVerified(inputData[0], inputData[1])) 
                    {
                        dbAccessor.DeleteAccount(inputData[0]);
                        output.Result["MESSAGE"] = Messages.PM02;
                        output.IsSuccessed = true;
                    }
                    else 
                    {
                        output.Result["MESSAGE"] = Messages.EM06;
                    }
                    return output;
                }
                catch (Exception e)
                {
                    output.Result["MESSAGE"] = Messages.GetErrorMessage(e);
                    return output;
                }   
            }
            output.Result["MESSAGE"] = Messages.EM10;
            return output;
        }
    }
}
