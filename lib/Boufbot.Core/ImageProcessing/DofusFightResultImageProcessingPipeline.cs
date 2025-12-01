using Boufbot.OCR.Enums;
using Boufbot.OCR.Services.ImageProcessing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Boufbot.OCR.ImageProcessing;

public sealed class DofusFightResultImageProcessingPipeline
    : IImageProcessingPipeline
{
    private const byte StrictColorTolerance = 1;
    private const byte ColorTolerance = 30;
    private const byte TextDetectionOffset = 5;
    private const double ImageScaleFactor = 1.5;

    private static readonly Rgba32[] DofusMainColors =
    [
        // Bonta | Brakmar | Gold and Steel
        new(255, 209, 148),
        // Standard
        new(240, 255, 79),
        // Tribute
        new(201, 237, 110),
        // Belladone
        new(222, 238, 110),
        // Unicorn
        new(255, 173, 237),
        // Emerald
        new(150, 248, 214),
        // Sufokia | Pandala
        new(255, 241, 148)
    ];

    private static readonly Rgba32[] DofusAllColors =
    [
        ..DofusMainColors,
        // Bonta | Brakmar | Gold and Steel
        new(205, 169, 122),
        // Standard
        new(188, 200, 78),
        // Tribute
        new(201, 237, 110),
        // Belladone
        new(189, 199, 103),
        //Unicorn
        new(216, 149, 202),
        // Emerald
        new(127, 206, 179),
        // Sufokia | Pandala
        new(215, 205, 134)
    ];

    private static readonly Rgba32 White = new(255, 255, 255);
    private static readonly Rgba32 Black = new(0, 0, 0);

    private readonly IColorDetectionService _colorDetectionService;
    private readonly IImageResizingService _imageResizingService;

    public DofusFightResultImageProcessingPipeline(IColorDetectionService colorDetectionService, IImageResizingService imageResizingService)
    {
        _colorDetectionService = colorDetectionService;
        _imageResizingService = imageResizingService;
    }

    public Image<Rgba32> ProcessImage(Image<Rgba32> image)
    {
        var minAuthorizedColorPosition = _colorDetectionService.GetMinCoordinatesForColor(
            image,
            Axis.X,
            StrictColorTolerance,
            DofusMainColors
        );

        if (minAuthorizedColorPosition.X is 0 || minAuthorizedColorPosition.X == image.Width)
            throw new InvalidOperationException("Can not find one of the targeted color on the image.");

        minAuthorizedColorPosition.X -= TextDetectionOffset;

        var result = image.Clone();

        _imageResizingService.CropImage(result, new Rectangle(minAuthorizedColorPosition.X, 0, result.Width - minAuthorizedColorPosition.X, result.Height));
        _imageResizingService.ResizeImage(result, ImageScaleFactor);

        result.ProcessPixelRows(accessor =>
        {
            for (var y = 0; y < accessor.Height; y++)
            {
                var row = accessor.GetRowSpan(y);

                for (var x = 0; x < row.Length; x++)
                {
                    row[x] = _colorDetectionService.IsNearColors(row[x], ColorTolerance, DofusAllColors) ? Black : White;
                }
            }
        });

        return result;
    }
}