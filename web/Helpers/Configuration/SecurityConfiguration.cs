namespace DynamicForm.Helpers.Configuration
{
    public class SecurityConfiguration
    {
        public int PasswordExpireInDays { get; set; }
        public bool EnableHttps { get; set; }
        public LockoutConfiguration  Lockout { get; set; }
        public JwtConfiguration Jwt { get; set; }
    }
}