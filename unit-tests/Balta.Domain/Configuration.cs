namespace Balta.Domain;

public static class Configuration
{
    public static ApiConfiguration Api { get; set; } = new();

    public class ApiConfiguration
    {
        public short MaxAccessFailedCount { get; set; } = 3;
        public short DefaultLockOutTimeInMinutes { get; set; } = 5;
        public string PasswordSalt { get; set; } = string.Empty;
    }
}