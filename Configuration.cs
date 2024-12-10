namespace BlogAspNet;

public static class Configuration
{
    public static string JwtKey = "jasdDKShfgADAZJ23AKjvnbSDJ23A4KDSADnvhgDSDfwksamdk=";
    public static string ApiKeyname = "api_key";
    public static string ApiKey = "curso_api_ilTevUM/z0ey3NwcV/unWg==";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string Username { get; set; }
        public string Password { get; set; }
    }
}