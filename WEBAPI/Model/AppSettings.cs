namespace WEBAPI.Model
{
    public class AppSettings
    {
        public JWT JWT { get; set; }
    }
    public class JWT
    {
        public string Secretkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string DurationInMinutes { get; set; }
        public string RefreshTokenExpiry { get; set; }
    }
}
