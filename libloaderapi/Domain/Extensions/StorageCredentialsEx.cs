using Azure.Storage;
using Microsoft.Azure.Storage.Auth;

namespace libloaderapi.Domain.Extensions
{
    public static class StorageCredentialsEx
    {
        public static StorageSharedKeyCredential ToStorageSharedKeyCredential(this StorageCredentials credentials)
        {
            return new StorageSharedKeyCredential(credentials.AccountName, credentials.ExportBase64EncodedKey());
        }
    }
}
