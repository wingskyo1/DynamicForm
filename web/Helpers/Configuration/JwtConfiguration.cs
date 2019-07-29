namespace DynamicForm.Helpers.Configuration
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public int MinutesToLive { get; set; }
    }
}