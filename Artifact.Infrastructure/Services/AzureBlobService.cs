using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Artifact.Infrastructure.Services
{
    public class AzureBlobService : IStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public AzureBlobService(BlobContainerClient containerClient)
        {
            _containerClient = containerClient;
        }

        public async Task UploadAsync(string name, Stream content)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(name);
            await blobClient.UploadAsync(content, true);
        }

        public async Task<Stream> DownloadAsync(string name)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(name);
            BlobDownloadInfo download = await blobClient.DownloadAsync();
            return download.Content;
        }

        public async Task<List<string>> ListAsync()
        {
            var items = new List<string>();

            await foreach (BlobItem item in _containerClient.GetBlobsAsync())
            {
                items.Add(item.Name);
            }

            return items;
        }
    }
}
