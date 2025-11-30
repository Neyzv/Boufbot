using Boufbot.OCR.Factories.Tesseract;
using Boufbot.OCR.ImageProcessing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Tesseract;

namespace Boufbot.OCR.Services.TextRecognition;

public sealed class TextRecognitionService
    : ITextRecognitionService
{
    private readonly ITesseractEngineFactory _tesseractEngineFactory;

    public TextRecognitionService(ITesseractEngineFactory tesseractEngineFactory)
    {
        _tesseractEngineFactory = tesseractEngineFactory;
    }

    public string GetTextFromImage(Image<Rgba32> image, IImageProcessingPipeline? imageProcessingPipeline = null)
    {
        using var engine = _tesseractEngineFactory.CreateEngine();

        using var ms = new MemoryStream();
        (imageProcessingPipeline is null ? image : imageProcessingPipeline.ProcessImage(image)).SaveAsPngAsync(ms);
        ms.Position = 0;

        using var pix = Pix.LoadFromMemory(ms.ToArray());
        using var page = engine.Process(pix);

        return page.GetText();
    }
}