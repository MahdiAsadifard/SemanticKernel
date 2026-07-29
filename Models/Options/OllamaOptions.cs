namespace AISample.Models.Options
{
    public class OllamaOptions
    {
        public const string SectionName = "SemanticKernel:Ollama";
        public string Model { get; set; }
        public string Endpoint { get; set; }
    }
}
