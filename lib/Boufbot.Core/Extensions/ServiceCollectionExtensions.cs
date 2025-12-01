using Boufbot.Core.Services.TextSanitizer;
using Boufbot.OCR.Extensions;
using Boufbot.OCR.ImageProcessing;
using Microsoft.Extensions.DependencyInjection;

namespace Boufbot.Core.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services of this project to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The app service collection instance.</param>
    /// <returns></returns>
    public static IServiceCollection AddCore(this IServiceCollection services) =>
        services
            .AddOCR()
            .AddSingleton<IDofusTextSanitizerService, DofusTextSanitizerService>()
            .AddSingleton<DofusFightResultImageProcessingPipeline>();
}