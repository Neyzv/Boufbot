using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Boufbot.OCR.ImageProcessing;

public interface IImageProcessingPipeline
{
    /// <summary>
    /// Apply image transformation on the provided image.
    /// </summary>
    /// <param name="image">The image to treat.</param>
    /// <returns>The newly created treated image.</returns>
    Image<Rgba32> ProcessImage(Image<Rgba32> image);
}