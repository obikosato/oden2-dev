using System;
using Common.Tools.Database;

namespace Common.Services
{
    public class UnregistrationService : IService
    {
        private readonly IDbAccessor dbAccessor;
        public UnregistrationService(IDbAccessor dbAccessor)
        {
            this.dbAccessor = dbAccessor ?? throw new ArgumentNullException(nameof(dbAccessor));
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
                catch
                {
                    output.Result["MESSAGE"] = Messages.EM01;
                    return output;
                }   
            }
            output.Result["MESSAGE"] = Messages.EM10;
            return output;
        }
    }
}
