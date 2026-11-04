namespace GarageV3.Validation;

internal static class RegistrationNumberValidator
{
    public const string ErrorMessage = "Ange ett registreringsnummer i formatet ABC123 eller ABC12A.";

    public static bool TryParse(string? input, out string registrationNumber)
    {
        registrationNumber = string.Empty;
        string candidate = input?.Trim() ?? string.Empty;

        // kollar längden först så vi kan läsa tecknen sen
        if (candidate.Length != 6)
        {
            return false;
        }

        // tre bokstäver först
        foreach (char character in candidate[..3])
        {
            if (!char.IsLetter(character))
            {
                return false;
            }
        }

        // sen två siffror
        foreach (char character in candidate[3..5])
        {
            if (!char.IsDigit(character))
            {
                return false;
            }
        }

        // sista tecknet får vara bokstav eller siffra
        if (!char.IsLetterOrDigit(candidate[^1]))
        {
            return false;
        }

        // sparar numret med stora bokstäver även om man skrev små
        registrationNumber = candidate.ToUpperInvariant();
        return true;
    }
}
