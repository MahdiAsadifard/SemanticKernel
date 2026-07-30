namespace Infrastructure.Options
{
    public class OllamaOption
    {
        public const string SectionName = "SemanticKernel:Ollama";
        public string Model { get; set; }
        public string Endpoint { get; set; }
    }
}
