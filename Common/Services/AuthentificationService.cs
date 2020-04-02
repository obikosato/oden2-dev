using Common.Tools.Database;
using Log;
using System;

namespace Common.Services
{
    public class AuthentificationService : IService
    {
        private readonly IDbAccessor dbAccessor;
        private readonly ILogger logger = new Logger();
        public AuthentificationService(IDbAccessor dbAccessor)
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

            if (inputData.Length == 3)
            {
                if (!Equals(inputData[1], inputData[2]))
                {
                    logger.Log("認証番号照合失敗", inputData[0]);
                    output.Result["MESSAGE"] = Messages.EM05;
                    return output;
                }
                else
                {
                    try
                    {
                        logger.Log("認証番号照合成功", inputData[0]);
                        dbAccessor.ActivateAccount(inputData[0]);
                        logger.Log("新規通知先登録完了", inputData[0]);
                        output.Result["MESSAGE"] = Messages.PM01;
                        output.IsSuccessed = true;
                        return output;
                    }
                    catch
                    {
                        logger.Log("データベース未接続", inputData[0]);
                        output.Result["MESSAGE"] = Messages.EM01;
                        return output;
                    }
                }
            }
            logger.Log("認証サービスへの入力データが不正", inputData[0]);
            output.Result["MESSAGE"] = Messages.EM10;
            return output;
        }
    }
}
