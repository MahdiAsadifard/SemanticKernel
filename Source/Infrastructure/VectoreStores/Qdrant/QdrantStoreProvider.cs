using CommunityToolkit.VectorData.Qdrant;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.VectorData;
using System.Runtime.CompilerServices;

namespace Infrastructure.VectoreStores.Qdrant
{
    internal class QdrantStoreProvider : IVectoreStoreProvider
    {
        private readonly ILogger<QdrantStoreProvider> _logger;
        private readonly QdrantVectorStore _vectoreStore;


        public QdrantStoreProvider(
            ILogger<QdrantStoreProvider> logger,
            IVectoreStoreClient<QdrantVectorStore> vectoreStoreClient
            )
        {
            this._logger = logger;
            this._vectoreStore = vectoreStoreClient.GetVectoreStore();
        }


        public VectorStoreCollection<TKey, TRecord> GetCollection<TKey, TRecord>(string collectionName)
            where TKey : notnull
            where TRecord : class
        {
            return this._vectoreStore.GetCollection<TKey, TRecord>(collectionName);
        }

        public async IAsyncEnumerable<string> GetCollectionNamesStream([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var namesStream = this._vectoreStore.ListCollectionNamesAsync(cancellationToken);
            await foreach (var item in namesStream)
            {
                yield return item;
            }
        }

        public async Task<bool> IsCollectionExistsAsync<TKey, TRecord>(CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class
        {
            try
            {
                return await this._vectoreStore.CollectionExistsAsync(typeof(TRecord).Name, cancellationToken);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to check if collection exists for {RecordType}", typeof(TRecord).Name);
                throw;
            }
        }

        public async Task<bool> CreateCollectionAsync<TKey, TRecord>(
            VectorStoreCollection<TKey, TRecord> collection,
            CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class
        {
            try
            {
                await collection.EnsureCollectionExistsAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to create collection for {RecordType}", typeof(TRecord).Name);
                throw;
            }
        }

        public async Task<bool> DeleteCollectionAsync<TKey, TRecord>(
            VectorStoreCollection<TKey, TRecord> collection,
            CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class
        {
            try
            {
                await collection.EnsureCollectionDeletedAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to delete collection for {RecordType}", typeof(TRecord).Name);
                throw;
            }
        }

        public async Task<bool> UpsertCollectionAsync<TKey, TRecord>(
            VectorStoreCollection<TKey, TRecord> collection,
            TRecord record,
            CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class
        {
            try
            {
                await collection.UpsertAsync(record, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to upsert collection for {RecordType}", typeof(TRecord).Name);
                throw;
            }
        }
    }
}
