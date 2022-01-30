using System;

namespace JwtAuthentication.API
{
    public static class SecuritySettings
    {
        public static string AccessTokenSecret = "zD&$Fy9KCSqcfHp3$3=pYvZzwjv#LrtTzwup";
        public static TimeSpan AccessTokenExpiration = TimeSpan.FromMinutes(10);
        public static string Audience = "clients.equilibriorh";
        public static string Issuer = "equilibriorh";
    }
}
