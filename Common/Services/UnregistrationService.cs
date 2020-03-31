using System;
using Common.Tools.Database;
using Log;

namespace Common.Services
{
    public class UnregistrationService : IService
    {
        private readonly IDbAccessor dbAccessor;
        private readonly ILogger logger = new Logger();
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
                        logger.Log("パスワード照合成功", inputData[0]);
                        dbAccessor.DeleteAccount(inputData[0]);
                        logger.Log("通知先登録解除完了", inputData[0]);
                        output.Result["MESSAGE"] = Messages.PM02;
                        output.IsSuccessed = true;
                    }
                    else 
                    {
                        logger.Log("パスワード照合失敗", inputData[0]);
                        output.Result["MESSAGE"] = Messages.EM06;
                    }
                    return output;
                }
                catch
                {
                    logger.Log("データベース未接続", inputData[0]);
                    output.Result["MESSAGE"] = Messages.EM01;
                    return output;
                }   
            }
            logger.Log("登録解除サービスへの入力データが不正", inputData[0]);
            output.Result["MESSAGE"] = Messages.EM10;
            return output;
        }
    }
}
