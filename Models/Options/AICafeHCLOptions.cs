namespace AISample.Models.Options
{
    public class AICafeHCLOptions
    {
        public const string SectionName = "SemanticKernel:AICafeHCL";
        public string Model { get; set; }
        public string Endpoint { get; set; }
        public string ApiKey { get; set; }
    }
}
