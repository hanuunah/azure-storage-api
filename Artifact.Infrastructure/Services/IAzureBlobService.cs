namespace Artifact.Infrastructure.Services
{
    public interface IStorageService
    {
        Task UploadAsync(string name, Stream content);
        Task<Stream> DownloadAsync(string name);
        Task<List<string>> ListAsync();
    }
}