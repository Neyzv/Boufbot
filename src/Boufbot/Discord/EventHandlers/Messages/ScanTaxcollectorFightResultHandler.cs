using Boufbot.Core.Services.TextSanitizer;
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
    private readonly IDofusTextSanitizerService _textSanitizerService;

    public ScanTaxcollectorFightResultHandler(IHttpService httpService,
        ITextRecognitionService textRecognitionService,
        DofusFightResultImageProcessingPipeline imageProcessingPipeline,
        IDofusTextSanitizerService textSanitizerService)
    {
        _httpService = httpService;
        _textRecognitionService = textRecognitionService;
        _imageProcessingPipeline = imageProcessingPipeline;
        _textSanitizerService = textSanitizerService;
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

        await message.ReplyAsync(
            string.Join(
                ",",
                _textSanitizerService.SanitizeFightResultNames(
                    _textRecognitionService
                        .GetTextFromImage(_imageProcessingPipeline.ProcessImage(image))
                        .Split(LineBreak)
                )
            )
        ).ConfigureAwait(false);
    }
}