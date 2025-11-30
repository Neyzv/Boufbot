using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Boufbot.OCR.Services.ImageProcessing;

public sealed class ImageResizingService
    : IImageResizingService
{
    public void ResizeImage(Image<Rgba32> img, double scale) =>
        img.Mutate(ctx => ctx
            .Resize((int)(img.Width * scale), (int)(img.Height * scale), KnownResamplers.Bicubic)
        );

    public void CropImage(Image<Rgba32> img, Rectangle cropArea) =>
        img.Mutate(ctx => ctx.Crop(cropArea));
}