using Microsoft.AspNetCore.Http;

namespace Travaloud.Core.Interfaces.Repositories
{
    public interface IAzureStorageRepository
    {
        Task<string> Upload(IFormFile blob, string containerName, string fileName = null);

        Task<string> UploadImage(IFormFile blob, string containerName, string fileName = null);

        Task Delete(string imageUrl, string containerName);

        Task UpdateAllBlobs(string containerName, string contentEncoding = null);
    }
}

