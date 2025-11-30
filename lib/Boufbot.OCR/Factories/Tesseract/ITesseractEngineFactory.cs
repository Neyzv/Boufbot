using Tesseract;

namespace Boufbot.OCR.Factories.Tesseract;

public interface ITesseractEngineFactory
{
    /// <summary>
    /// Create and configure a new instance of a <see cref="TesseractEngine"/>.
    /// </summary>
    /// <returns></returns>
    TesseractEngine CreateEngine();
}