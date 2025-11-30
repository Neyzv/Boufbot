using Boufbot.Services.Http;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;

namespace Boufbot.Discord.EventHandlers.Messages;

public sealed class ScanTaxcollectorFightResultHandler
    : IMessageCreateShardedGatewayHandler
{
    private const string WebpImageContentType = "image/webp";

    private readonly IHttpService _httpService;

    public ScanTaxcollectorFightResultHandler(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async ValueTask HandleAsync(GatewayClient client, Message message)
    {
        if (message.Author.IsBot)
            return;

        if (message.Attachments.Count is not 1)
            return;

        var attachment = message.Attachments[0];

        if (attachment.ContentType is not WebpImageContentType)
            return;

        var image = await _httpService.GetImageAsync(attachment.Url).ConfigureAwait(false);

        await message.ReplyAsync("Correct").ConfigureAwait(false);
    }
}