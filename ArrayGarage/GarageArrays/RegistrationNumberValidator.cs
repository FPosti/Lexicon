namespace GarageArrays;

internal static class RegistrationNumberValidator
{
    public const string ErrorMessage = "Ange ett registreringsnummer i formatet ABC123 eller ABC12A.";

    // true betyder att numret är giltigt. out lämnar då tillbaka numret med stora bokstäver.
    public static bool TryParse(string? input, out string registrationNumber)
    {
        registrationNumber = string.Empty;
        string candidate = input?.Trim() ?? string.Empty;

        if (candidate.Length != 6)
        {
            return false;
        }

        // [..3] väljer de tre första tecknen. foreach kontrollerar ett tecken i taget.
        foreach (char character in candidate[..3])
        {
            if (!char.IsLetter(character))
            {
                return false;
            }
        }

        // [3..5] väljer tecknen på index 3 och 4,
        foreach (char character in candidate[3..5])
        {
            if (!char.IsDigit(character))
            {
                return false;
            }
        }

        // ^1 betyder det sista tecknet.
        if (!char.IsLetterOrDigit(candidate[^1]))
        {
            return false;
        }

        // Spara registreringsnumret med stora bokstäver.
        registrationNumber = candidate.ToUpperInvariant();
        return true;
    }
}
