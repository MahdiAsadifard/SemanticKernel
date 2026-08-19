using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.VectoreStores
{
    public interface IVectoreStoreClient<T> where T : class
    {
        T GetVectoreStore();
    }
}
