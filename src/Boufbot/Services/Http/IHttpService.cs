using System.Diagnostics.CodeAnalysis;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Boufbot.Services.Http;

public interface IHttpService
{
    Task<TValue> GetAndDeserializeAsync<TValue>([StringSyntax(StringSyntaxAttribute.Uri)] string url);

    Task<Stream> GetResponseStreamAsync([StringSyntax(StringSyntaxAttribute.Uri)] string url);

    Task<Image<Rgba32>> GetImageAsync([StringSyntax(StringSyntaxAttribute.Uri)] string url);
}