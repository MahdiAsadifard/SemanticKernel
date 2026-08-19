using CommunityToolkit.VectorData.Qdrant;
using Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qdrant.Client;

namespace Infrastructure.VectoreStores.Qdrant
{
    public class QdrantStoreClient : IVectoreStoreClient<QdrantVectorStore>
    {
        private readonly ILogger<QdrantStoreClient> _logger;
        private readonly IOptions<QdrantVectorStoreOption> _qdrantOptions;

        public QdrantStoreClient(
            ILogger<QdrantStoreClient> logger,
            IOptions<QdrantVectorStoreOption> qdrantOptions
            )
        {
            this._logger = logger;
            this._qdrantOptions = qdrantOptions;
        }

        private QdrantClient GetVectoreClient()
        {
            return new QdrantClient(
                host: this._qdrantOptions.Value.Host,
                port: this._qdrantOptions.Value.Port,
                https: this._qdrantOptions.Value.Https,
                apiKey: this._qdrantOptions.Value.ApiKey);
        }

        public QdrantVectorStore GetVectoreStore()
        {

            var client = this.GetVectoreClient();
            var vectoreStore = new QdrantVectorStore(client, ownsClient: true);
            this._logger.Log(LogLevel.Information, "GetVectoreStore called with options: {Options}", this._qdrantOptions.Value);
            return vectoreStore;
        }
    }
}
