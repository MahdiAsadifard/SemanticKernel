using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Options
{
    public class QdrantVectorStoreOption
    {
        public const string SectionName = "VectorStores:QdrantLocal";

        // write props for Host, Port, Https, Endpoint, ApiKey
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool Https { get; set; } = false;
        public string Endpoint { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
