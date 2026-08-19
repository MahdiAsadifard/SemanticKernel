using Microsoft.Extensions.VectorData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.VectoreStores
{
    public interface IVectoreStoreProvider
    {
        VectorStoreCollection<TKey, TRecord> GetCollection<TKey, TRecord>(string collectionName)
            where TKey : notnull
            where TRecord : class;

        Task<bool> CreateCollection<TKey, TRecord>(CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class;


        Task<bool> IsCollectionExists<TKey, TRecord>(CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class;

        Task<bool> DeleteCollection<TKey, TRecord>(string collectionName, CancellationToken cancellationToken = default)
            where TKey : notnull
            where TRecord : class;

    }
}
