using Boufbot.OCR.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Boufbot.OCR.Services.ImageProcessing;

public sealed class ColorDetectionService
    : IColorDetectionService
{
    public bool IsNearColors(Rgba32 color, byte tolerance, params Rgba32[] targetedColors) =>
        targetedColors.Any(targetedColor =>
            Math.Abs(color.R - targetedColor.R) <= tolerance && Math.Abs(color.G - targetedColor.G) <= tolerance && Math.Abs(color.B - targetedColor.B) <= tolerance);

    public Point GetMinCoordinatesForColor(Image<Rgba32> img, Axis axis, byte tolerance, params Rgba32[] targetedColors)
    {
        var minX = img.Width;
        var minY = img.Height;

        img.ProcessPixelRows(accessor =>
        {
            for (var y = 0; y < accessor.Height; y++)
            {
                var row = accessor.GetRowSpan(y);

                for (var x = 0; x < row.Length; x++)
                {
                    ref var c = ref row[x];

                    if (!IsNearColors(c, tolerance, targetedColors)
                        || ((axis is not Axis.X || x >= minX)
                            && (axis is not Axis.Y || y >= minY)
                            && (axis is not Axis.Any || x + y >= minX + minY)))
                        continue;

                    minX = x;
                    minY = y;

                    break;
                }
            }
        });

        return new Point(minX, minY);
    }
}