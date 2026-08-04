namespace Infrastructure.Options
{
    public class SemanticKernelOptions
    {
        public const string SectionName = "SemanticKernel";
        public int CancellationTokenTimeoutInSeconds { get; set; }
    }
}
