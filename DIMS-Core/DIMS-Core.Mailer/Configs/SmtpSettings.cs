namespace DIMS_Core.Mailer.Configs
{
    internal class SmtpSettings
    {
        public const string ConfigKey = "SmtpSettings";

        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}