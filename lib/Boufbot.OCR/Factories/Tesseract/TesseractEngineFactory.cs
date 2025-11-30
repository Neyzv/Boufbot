using Tesseract;

namespace Boufbot.OCR.Factories.Tesseract;

public sealed class TesseractEngineFactory
    : ITesseractEngineFactory
{
    private const string TessDataFolderName = "tessdata";
    private const string EnglishAlias = "eng";

    private const string TessVarCharListVariableName = "tessedit_char_whitelist";
    private const string Charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz’'-:%éèàùêôç0123456789";

    private static readonly string TessDataPath = Path.Combine(AppContext.BaseDirectory, TessDataFolderName);

    public TesseractEngine CreateEngine()
    {
        var engine = new TesseractEngine(TessDataPath, EnglishAlias, EngineMode.Default);
        engine.SetVariable(TessVarCharListVariableName, Charset);

        return engine;
    }
}