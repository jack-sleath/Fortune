using System.Reflection.Metadata;

namespace Fortune.Services.Interfaces
{
    public interface ITtsService
    {
        Task<byte[]> GetTTSBlob(string text);
    }
}
