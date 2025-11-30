using Boufbot.OCR.ImageProcessing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Boufbot.OCR.Services.TextRecognition;

public interface ITextRecognitionService
{
    /// <summary>
    /// Get text from the provided image.
    /// </summary>
    /// <param name="image">The image to analyze.</param>
    /// <param name="imageProcessingPipeline">The pipeline to process the image.</param>
    /// <returns></returns>
    string GetTextFromImage(Image<Rgba32> image, IImageProcessingPipeline? imageProcessingPipeline = null);
}