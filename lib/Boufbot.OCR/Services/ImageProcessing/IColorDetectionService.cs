using Boufbot.OCR.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Boufbot.OCR.Services.ImageProcessing;

public interface IColorDetectionService
{
    /// <summary>
    /// Determins if the provided color is near one of the provided colors and within the tolerance.
    /// </summary>
    /// <param name="color">The tested color.</param>
    /// <param name="tolerance">The tolerance to apply.</param>
    /// <param name="targetedColors">The desired colors.</param>
    /// <returns></returns>
    bool IsNearColors(Rgba32 color, byte tolerance, params Rgba32[] targetedColors);
    
    /// <summary>
    /// Get the minimum coordinate for a point who is near one of the provided colors and within the tolerance.
    /// </summary>
    /// <param name="img">The source image.</param>
    /// <param name="axis">The targeted axis.</param>
    /// <param name="tolerance">The tolerance to apply</param>
    /// <param name="targetedColors">The desired colors.</param>
    /// <returns>The coordinate of the point.</returns>
    Point GetMinCoordinatesForColor(Image<Rgba32> img, Axis axis, byte tolerance, params Rgba32[] targetedColors);
}