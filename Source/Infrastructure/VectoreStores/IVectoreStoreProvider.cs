using Microsoft.Extensions.VectorData;
using System.Runtime.CompilerServices;

namespace Infrastructure.VectoreStores
{
    public interface IVectoreStoreProvider
    {
        VectorStoreCollection<TKey, TRecord> GetCollection<TKey, TRecord>(string collectionName)
            where TKey : notnull
            where TRecord : class;

        IAsyncEnumerable<string> GetCollectionNamesStream(CancellationToken cancellationToken = default);

        Task<bool> CreateCollectionAsync<TKey, TRecord>(
            VectorStoreCollection<TKey, TRecord> collection,
            CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class;


        Task<bool> IsCollectionExistsAsync<TKey, TRecord>(
            CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class;

        Task<bool> DeleteCollectionAsync<TKey, TRecord>(
            VectorStoreCollection<TKey, TRecord> collection,
            CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class;

        Task<bool> UpsertCollectionAsync<TKey, TRecord>(
            VectorStoreCollection<TKey, TRecord> collection,
            TRecord record,
            CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class;

    }
}
