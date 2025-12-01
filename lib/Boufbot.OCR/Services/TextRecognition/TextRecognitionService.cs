using Boufbot.OCR.Factories.Tesseract;
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

    public string GetTextFromImage(Image<Rgba32> image)
    {
        using var engine = _tesseractEngineFactory.CreateEngine();

        using var ms = new MemoryStream();
        image.SaveAsPngAsync(ms);
        ms.Position = 0;

        using var pix = Pix.LoadFromMemory(ms.ToArray());
        using var page = engine.Process(pix);

        return page.GetText();
    }
}