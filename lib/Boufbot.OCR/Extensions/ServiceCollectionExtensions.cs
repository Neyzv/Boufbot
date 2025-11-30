using Boufbot.OCR.Factories.Tesseract;
using Boufbot.OCR.ImageProcessing;
using Boufbot.OCR.Services.ImageProcessing;
using Boufbot.OCR.Services.TextRecognition;
using Microsoft.Extensions.DependencyInjection;

namespace Boufbot.OCR.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services of this project to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The app service collection instance.</param>
    /// <returns></returns>
    public static IServiceCollection AddOCR(this IServiceCollection services) =>
        services
            .AddSingleton<IColorDetectionService, ColorDetectionService>()
            .AddSingleton<IImageResizingService, ImageResizingService>()
            .AddSingleton<ITextRecognitionService, TextRecognitionService>()
            .AddSingleton<ITesseractEngineFactory, TesseractEngineFactory>()
            .AddSingleton<DofusFightResultImageProcessingPipeline>();
}