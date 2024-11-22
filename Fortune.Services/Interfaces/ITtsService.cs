using System.Reflection.Metadata;

namespace Fortune.Services.Interfaces
{
    public interface ITtsService
    {
        byte[] GetTTSBlob(string text);
    }
}
