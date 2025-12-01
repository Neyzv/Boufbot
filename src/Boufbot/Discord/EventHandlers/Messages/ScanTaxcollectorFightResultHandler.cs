using Boufbot.OCR.ImageProcessing;
using Boufbot.OCR.Services.TextRecognition;
using Boufbot.Services.Http;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;

namespace Boufbot.Discord.EventHandlers.Messages;

public sealed class ScanTaxcollectorFightResultHandler
    : IMessageCreateShardedGatewayHandler
{
    private const string WebpImageContentType = "image/webp";
    private const string PngImageContentType = "image/png";
    private const char LineBreak = '\n';

    private readonly IHttpService _httpService;
    private readonly ITextRecognitionService _textRecognitionService;
    private readonly DofusFightResultImageProcessingPipeline _imageProcessingPipeline;

    public ScanTaxcollectorFightResultHandler(IHttpService httpService,
        ITextRecognitionService textRecognitionService,
        DofusFightResultImageProcessingPipeline imageProcessingPipeline)
    {
        _httpService = httpService;
        _textRecognitionService = textRecognitionService;
        _imageProcessingPipeline = imageProcessingPipeline;
    }

    public async ValueTask HandleAsync(GatewayClient client, Message message)
    {
        if (message.Author.IsBot)
            return;

        if (message.Attachments.Count is not 1)
            return;

        var attachment = message.Attachments[0];

        if (attachment.ContentType is not WebpImageContentType and not PngImageContentType)
            return;

        var image = await _httpService.GetImageAsync(attachment.Url).ConfigureAwait(false);
        
        var text = _textRecognitionService.GetTextFromImage(image, _imageProcessingPipeline).Split(LineBreak).Select(x => x.Trim()).Where(x => x.Length > 0).ToArray();

        await message.ReplyAsync(string.Join(",", text)).ConfigureAwait(false);
    }
}