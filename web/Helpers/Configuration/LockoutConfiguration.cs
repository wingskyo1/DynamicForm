namespace DynamicForm.Helpers.Configuration
{
    public class LockoutConfiguration
    {
        public int MaxFailedLogin { get; set; }
        public int LockoutMinutes { get; set; }
    }
}