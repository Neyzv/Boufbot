using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Boufbot.OCR.Services.TextRecognition;

public interface ITextRecognitionService
{
    /// <summary>
    /// Get text from the provided image.
    /// </summary>
    /// <param name="image">The image to analyze.</param>
    /// <returns>The string content recognize in the image.</returns>
    string GetTextFromImage(Image<Rgba32> image);
}