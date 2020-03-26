using System;

namespace Common.Services
{
    public class Messages
    {
        public static readonly string PM01 = "通知先を登録しました。";
        public static readonly string PM02 = "通知先の登録を解除しました。";
        public static readonly string PM03 = "認証番号を送信しました。LINEを開いて確認してください。" +
            "\n受信した認証番号を下の欄に入力してください。" +
            "\n認証番号が届かない場合は、お手数ですが、前のページに戻ってもう一度やり直してください。";

        public static readonly string EM01 = "データベースエラーです。\n" +
            "申し訳ございませんが、しばらくたってから、もう一度お試しください。";
        public static readonly string EM02 = "ネットワークエラーです。\n" +
            "申し訳ございませんが、しばらくたってから、もう一度お試しください。";
        public static readonly string EM03a = "入力された通知先IDはすでに使用されています。\n" +
            "別の通知先IDをご使用ください。";
        public static readonly string EM03b = "入力されたアクセストークンはすでに使用されています。\n" +
            "別のアクセストークンをご使用ください。";
        public static readonly string EM04 = "認証番号の送信に失敗しました。\n" +
            "アクセストークンを確認して、もう一度やり直してください。";
        public static readonly string EM05 = "認証番号の照合に失敗しました。\n" +
            "もう一度やり直してください。";
        public static readonly string EM06 = "IDまたはパスワードが間違っています。";

        public static readonly string EM10 = "不明なエラーが発生しました...";
        public static readonly string EM11 = "未入力の欄があります！！";
        public static readonly string EM12 = "入力が不正です！！";

        public static string AM01(string eventName, string artist)
        {
            bool noEventName = string.IsNullOrEmpty(eventName);
            bool noArtist = string.IsNullOrEmpty(artist);

            string msg = "\n\n■新横浜混雑注意報■\n\n" +
                "本日、新横浜駅周辺で混雑が予想されます。";

            if (noEventName && noArtist)
            {
                msg += "\n横浜アリーナでイベントが開催されます。";
            }
            else
            {
                msg += "\n横浜アリーナで次のイベントが開催されます。" +
                    "\n\n--本日開催！！--\n";

                if (!noEventName)
                {
                    msg += "\nイベント名：" + eventName;
                }

                if (!noArtist)
                {
                    msg += "\nアーティスト名：" + artist;
                }
            }
            return msg;
        }
        public static string AM02 = "\n\n\n本日、新横浜周辺のイベントはありません。";
        public static string AM03 = "\n\n\nシステムのエラーにより、本日のイベント情報が取得できませんでした。";

        public static string URL = "\n\n\n横浜アリーナHP：" + "https://www.yokohama-arena.co.jp/";

        public static string GetErrorMessage(Exception e)
        {
            return EM01 + e.Message + "\n" + e.InnerException + "\n" + e.GetHashCode().ToString();
        }
    }
}
