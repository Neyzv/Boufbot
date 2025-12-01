namespace Boufbot.Services.TextSanitizer;

public interface IDofusTextSanitizer
{
    /// <summary>
    /// Sanitize the provided actor names coming from the fight result OCR.
    /// </summary>
    /// <param name="names">The detected names of the actors.</param>
    /// <returns>Sanitized names.</returns>
    IEnumerable<string> SanitizeFightResultNames(string[] names);
}