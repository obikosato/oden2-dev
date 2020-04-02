using System;
using System.Globalization;
using Common.Tools.Database;
using Common.Tools.LineNotify;
using Common.Tools.WebSite;
using Log;
using TimeZoneConverter;

namespace Common.Services
{
    public class NotificationService : IService
    {
        private readonly IDbAccessor dbAccessor;
        private readonly ILineMessenger lineMessenger;
        private readonly IEventInfoConverter eventInfoConverter;
        private readonly Logger logger = new Logger();

        public NotificationService(IDbAccessor dbAccessor, IEventInfoConverter eventInfoConverter, ILineMessenger lineMessenger) 
        {
            this.dbAccessor = dbAccessor ?? throw new ArgumentNullException(nameof(dbAccessor));
            this.eventInfoConverter = eventInfoConverter ?? throw new ArgumentNullException(nameof(eventInfoConverter));
            this.lineMessenger = lineMessenger ?? throw new ArgumentNullException(nameof(lineMessenger));
        }

        public ServiceResults DoService(string[] inputData) {

            var output = new ServiceResults();
            output.Result.Add("MESSAGE", string.Empty);
            string alertMessage;
            EventInfo convertedEventInfo = eventInfoConverter.ConvertEventInfo();
            
            if (DateTime.TryParseExact(convertedEventInfo.date, "yyyy.MM.dd", null, DateTimeStyles.AssumeLocal, out DateTime eventDateTime))
            {
                //ローカル時刻とローカルのタイムゾーンから東京(UTC+9:00)の現在時刻を取得
                TimeZoneInfo tokyoTimeInfo = TZConvert.GetTimeZoneInfo("Tokyo Standard Time");
                DateTime localTimeInTokyo = TimeZoneInfo.ConvertTime(DateTime.Now, tokyoTimeInfo);
                
                //東京の日付とイベントの日付を比較
                if (eventDateTime == localTimeInTokyo.Date)
                {
                    //本日のイベントがある場合
                    alertMessage = Messages.AM01(convertedEventInfo.title, convertedEventInfo.artist);
                }
                else
                {
                    //直近のイベントが本日でない場合
                    alertMessage = Messages.AM02;
                }
            }
            else {
                //イベント情報を取得できなかった場合
                alertMessage = Messages.AM03;
            }

            logger.Log(alertMessage, "アラートメッセージ");
            alertMessage += Messages.URL;

            try
            {
                //有効な通知先情報を取得
                var tokens = dbAccessor.GetActiveAccounts();
                foreach (var x in tokens)
                {
                    string logMessage = lineMessenger.SendMessage(x.access_token, alertMessage) ? "アラート送信成功" : "アラート送信失敗";
                    logger.Log(logMessage, x.id);
                }
                return output;
            }
            catch
            {
                logger.Log(message:"データベース未接続");
                return output;
            }
        }
        
        public void Dispose()
        {
            dbAccessor.Dispose();
        }
    }
}
