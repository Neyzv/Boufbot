using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Boufbot.OCR.Services.ImageProcessing;

public interface IImageResizingService
{
    /// <summary>
    /// Resize the provided image by the provided <paramref name="scale"/>.
    /// </summary>
    /// <param name="img">The source image</param>
    /// <param name="scale">The scale to apply</param>
    void ResizeImage(Image<Rgba32> img, double scale);

    /// <summary>
    /// Crop the provided image with the specified <paramref name="cropArea"/>.
    /// </summary>
    /// <param name="img">The image to crop.</param>
    /// <param name="cropArea">The crop area.</param>
    void CropImage(Image<Rgba32> img, Rectangle cropArea);
}