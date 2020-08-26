
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using libloaderapi.Domain.Extensions;
using Microsoft.Azure.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace libloaderapi.Domain.Services
{
    public interface IBlobService
    {
        string GetBlobDownloadURI(string name);
    }

    public class BlobService : IBlobService
    {
        private readonly BlobContainerClient _container;
        private readonly BlobServiceClient _client;
        private readonly StorageSharedKeyCredential _key;

        public BlobService(IConfiguration configuration)
        {
            var connectionString = configuration["AZURE_STORAGE_CONNECTION_STRING"];

            _client = new BlobServiceClient(connectionString);
            _container = _client.GetBlobContainerClient("serve");
            _key = CloudStorageAccount.Parse(connectionString).Credentials.ToStorageSharedKeyCredential();
        }

        public string GetBlobDownloadURI(string name)
        {

            var blobClient = _container.GetBlobClient(name);
            if(!blobClient.Exists())
                return string.Empty;

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _container.Name,
                BlobName = name,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1),
            };
            sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);

            return blobClient.Uri + "?" + sasBuilder.ToSasQueryParameters(_key).ToString();
        }
    }
}
