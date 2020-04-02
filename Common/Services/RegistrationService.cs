using System;
using System.Collections.Generic;
using System.Linq;
using Common.Tools.Database;
using Common.Tools.LineNotify;
using Log;

namespace Common.Services

{
    public class RegistrationService : IService
    {
        private readonly IDbAccessor dbAccessor;
        private readonly ILineMessenger lineMessenger;
        private readonly ILogger logger = new Logger();

        public RegistrationService(IDbAccessor dbAccessor, ILineMessenger lineMessenger)
        {
            this.dbAccessor = dbAccessor ?? throw new ArgumentNullException(nameof(dbAccessor));
            this.lineMessenger = lineMessenger ?? throw new ArgumentNullException(nameof(lineMessenger));
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
                try 
                {
                    Account registeredAccount = dbAccessor.AddAccount(inputData[0], inputData[1], inputData[2]);
                    Random rnd = new Random();
                    string authNo = rnd.Next(10000).ToString("D4");
                    string msg = "\n認証番号：" + authNo;

                    bool MessageWasSent = lineMessenger.SendMessage(registeredAccount.access_token, msg);

                    if (MessageWasSent)
                    {
                        logger.Log("認証番号送信成功", inputData[0]);
                        output.Result["MESSAGE"] = Messages.PM03;
                        output.Result.Add("AUTHNO", authNo);
                        output.IsSuccessed = true;
                        return output;
                    }
                    else
                    {
                        logger.Log("認証番号送信失敗", inputData[0]);
                        dbAccessor.DeleteAccount(inputData[0]);
                        output.Result["MESSAGE"] = Messages.EM04;
                        return output;
                    }
                }
                catch (Exception e)
                {
                    if (!string.IsNullOrEmpty(e.InnerException.ToString())) 
                    {
                        List<string> innerException = e.InnerException.ToString().Split("\'").ToList();
                        //IDが重複している場合
                        if (innerException.Any(x => x == @"PRIMARY"))
                        {
                            output.Result["MESSAGE"] = Messages.EM03a;
                            return output;
                        }
                        //アクセストークンが重複している場合
                        if (innerException.Any(x => x == @"access_token"))
                        {
                            output.Result["MESSAGE"] = Messages.EM03b;
                            return output;
                        }
                    }
                    logger.Log("データベース未接続", inputData[0]);
                    output.Result["MESSAGE"] = Messages.EM01;
                    return output;
                }
            }
            logger.Log("登録サービスへの入力データが不正", inputData[0]);
            output.Result["MESSAGE"] = Messages.EM10;
            return output;
        }
    }
}
