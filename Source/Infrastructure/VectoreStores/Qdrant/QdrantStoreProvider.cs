using CommunityToolkit.VectorData.Qdrant;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.VectorData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.VectoreStores.Qdrant
{
    internal class QdrantStoreProvider : IVectoreStoreProvider
    {
        private readonly ILogger<QdrantStoreProvider> _logger;
        private readonly IVectoreStoreClient<QdrantVectorStore> _vectoreStoreClient;

        private readonly QdrantVectorStore _vectoreStore;

        public QdrantStoreProvider(
            ILogger<QdrantStoreProvider> logger,
            IVectoreStoreClient<QdrantVectorStore> vectoreStoreClient
            )
        {
            this._logger = logger;
            this._vectoreStoreClient = vectoreStoreClient;
            this._vectoreStore = vectoreStoreClient.GetVectoreStore();
        }

        public VectorStoreCollection<TKey, TRecord> GetCollection<TKey, TRecord>(string collectionName)
            where TKey : notnull
            where TRecord : class
        {
            return this._vectoreStore.GetCollection<TKey, TRecord>(collectionName);
        }


        public async Task<bool> IsCollectionExists<TKey, TRecord>(CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class
        {
            return await this._vectoreStore.CollectionExistsAsync(typeof(TRecord).Name, cancellationToken);
        }

        public async Task<bool> CreateCollection<TKey, TRecord>(CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class
        {
            try
            {
                VectorStoreCollection<TKey, TRecord> collection = this.GetCollection<TKey, TRecord>(typeof(TRecord).Name);
                await collection.EnsureCollectionExistsAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to create collection for {RecordType}", typeof(TRecord).Name);
            }
            return false;
        }

        public async Task<bool> DeleteCollection<TKey, TRecord>(string collectionName, CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class
        {
            try
            {
                VectorStoreCollection<TKey, TRecord> collection = this.GetCollection<TKey, TRecord>(typeof(TRecord).Name);
                await collection.EnsureCollectionDeletedAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to delete collection for {RecordType}", typeof(TRecord).Name);
            }
            return false;
        }
    }
}
