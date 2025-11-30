using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Boufbot.Services.Http;

public sealed class HttpService
    : IHttpService
{
    private readonly HttpClient _httpClient = new HttpClient
    {
        Timeout = TimeSpan.FromSeconds(10)
    };

    public async Task<Stream> GetResponseStreamAsync([StringSyntax(StringSyntaxAttribute.Uri)] string url)
    {
        var response = await _httpClient
            .GetAsync(url, HttpCompletionOption.ResponseHeadersRead)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    public async Task<TValue> GetAndDeserializeAsync<TValue>(string subUrl) =>
        (await JsonSerializer
            .DeserializeAsync<TValue>(await GetResponseStreamAsync(subUrl).ConfigureAwait(false))
            .ConfigureAwait(false))!;

    public async Task<Image<Rgba32>> GetImageAsync(string subUrl)
    {
        await using var response = await GetResponseStreamAsync(subUrl).ConfigureAwait(false);

        return await Image.LoadAsync<Rgba32>(response).ConfigureAwait(false);
    }
}