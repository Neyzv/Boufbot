namespace Boufbot.Services.TextSanitizer;

public sealed class DofusTextSanitizer
    : IDofusTextSanitizer
{
    private const byte MinCharacterName = 3;

    public IEnumerable<string> SanitizeFightResultNames(string[] names)
    {
        foreach (var line in names
                     .Select(x => x.Trim())
                     .Where(x => x.Length >= MinCharacterName))
        {
            var indexOfFirstUppercaseChar = -1;

            for (var i = 0; i < line.Length; i++)
            {
                if (!char.IsUpper(line[i]))
                    continue;

                indexOfFirstUppercaseChar = i;
                break;
            }

            if (indexOfFirstUppercaseChar < 0
                || line.Length - indexOfFirstUppercaseChar < MinCharacterName)
                continue;

            yield return line.AsSpan(indexOfFirstUppercaseChar).ToString();
        }
    }
}