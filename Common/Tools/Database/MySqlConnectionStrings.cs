namespace Common.Tools.Database
{
    public class MySqlConnectionStrings
    {
        private const string serverName = "lacalhost";
        private const string databaseName = "mydb";
        private const string userId = "root";
        private const string pwd = "password";
        private const string sslMode = "none";

        public static string ConnectionStrings
        {
            get => @"server=" + serverName +
                @";database=" + databaseName +
                @";userid=" + userId +
                @";pwd=" + pwd + 
                @";sslmode=" + sslMode +
                @";";
        }
    }
}
