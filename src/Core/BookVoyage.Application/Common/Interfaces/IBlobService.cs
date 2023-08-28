using Microsoft.AspNetCore.Http;

namespace BookVoyage.Application.Common.Interfaces;

public interface IBlobService
{
    Task<string> GetBlob(string blobName, string containerName);
    Task<string> UploadBlob(string blobName, string containerName, IFormFile file);
}