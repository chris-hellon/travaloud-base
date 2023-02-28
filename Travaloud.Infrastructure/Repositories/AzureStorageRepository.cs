using System.Globalization;
using Travaloud.Core.Interfaces.Repositories;
using Travaloud.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using MimeMapping;
using Azure.Storage.Blobs.Models;

namespace Travaloud.Infrastructure.Repositories
{
    public class AzureStorageRepository : IAzureStorageRepository
    {
        private readonly IErrorLoggerService _errorLoggerService;
        private readonly string _storageConnectionString;

        public AzureStorageRepository(IErrorLoggerService errorLoggerService, IConfiguration configuration)
        {
            _errorLoggerService = errorLoggerService;
            _storageConnectionString = configuration["AzureStorageConnectionString"];
        }

        public async Task<string> UploadImage(IFormFile blob, string containerName, string fileName = null)
        {
            try
            {
                if (fileName != null)
                {
                    var extension = Path.GetExtension(blob.FileName);
                    fileName = $"{fileName.Replace("/", "").Replace(" ", "")}{DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace("/", "").Replace("-", "").Replace(".", "").Replace(":", "").Replace(" ", "")}{extension}";
                }
                else
                    fileName = blob.FileName;
                
                var container = new BlobContainerClient(_storageConnectionString, containerName);
                var client = container.GetBlobClient(fileName);

                await using var data = blob.OpenReadStream();
                await client.UploadAsync(data, new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders()
                    {
                        ContentType = MimeUtility.GetMimeMapping(fileName),
                        CacheControl = "31536000"
                    }
                });

                return client.Uri.AbsoluteUri.Replace("https://paperstreetsoap.blob.core.windows.net", "https://paperstreetsoap.azureedge.net");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }

            return null;
        }

        public async Task<string> Upload(IFormFile blob, string containerName, string fileName = null)
        {
            try
            {
                if (fileName != null)
                {
                    var extension = Path.GetExtension(blob.FileName);
                    fileName = $"{fileName.Replace("/", "").Replace(" ", "")}{DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace("/", "").Replace("-", "").Replace(".", "").Replace(":", "").Replace(" ", "")}{extension}";
                }
                else fileName = blob.FileName;

                var container = new BlobContainerClient(_storageConnectionString, containerName);
                var client = container.GetBlobClient(fileName);

                await using var data = blob.OpenReadStream();
                await client.UploadAsync(data, new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders()
                    {
                        ContentType = MimeUtility.GetMimeMapping(fileName),
                        CacheControl = "31536000"
                    }
                });

                return client.Uri.AbsoluteUri.Replace("https://paperstreetsoap.blob.core.windows.net", "https://paperstreetsoap.azureedge.net");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }

            return null;
        }

        public async Task Delete(string imageUrl, string containerName)
        {
            try
            {
                var fileName = imageUrl.Replace($"https://paperstreetsoap.blob.core.windows.net/{containerName}/", "");

                var container = new BlobContainerClient(_storageConnectionString, containerName);
                var file = container.GetBlobClient(fileName);

                if (await file.ExistsAsync())
                    await file.DeleteAsync();
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }
        }

        public async Task UpdateAllBlobs(string containerName, string contentEncoding = null)
        {
            var container = new BlobContainerClient(_storageConnectionString, containerName);

            var blobServiceClient = new BlobServiceClient(_storageConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobs = container.GetBlobs();

            foreach (var item in blobs)
            {
                var blobClient = containerClient.GetBlobClient(item.Name);
                BlobProperties properties = await blobClient.GetPropertiesAsync();

                var headers = new BlobHttpHeaders
                {
                    ContentType = properties.ContentType,
                    ContentLanguage = properties.ContentLanguage,
                    CacheControl = properties.CacheControl,
                    ContentDisposition = properties.ContentDisposition,
                    ContentEncoding = contentEncoding ?? properties.ContentEncoding,
                    ContentHash = properties.ContentHash
                };

                // Set the blob's properties.
                await blobClient.SetHttpHeadersAsync(headers);
            }
        }
    }
}

