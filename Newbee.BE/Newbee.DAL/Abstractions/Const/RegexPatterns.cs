namespace Newbee.DAL.Abstractions.Const
{
    public static class RegexPatterns
    {
        public const string Password = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()\\-_=+{};:,<.>]).{8,}$";
    }

}
