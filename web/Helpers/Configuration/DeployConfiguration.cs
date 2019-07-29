namespace DynamicForm.Helpers.Configuration
{
    public class DeployConfiguration
    {
        public string Mode { get; set; }
        public string Name { get; set; }
        public string MasterEndpoint { get; set; }
        public string[] EdgeEndpoints { get; set; }
    }
}