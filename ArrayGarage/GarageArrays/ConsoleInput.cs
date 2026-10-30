using System.Globalization;

namespace GarageArrays;

internal static class ConsoleInput
{
    private static string ReadText(string prompt)
    {
        Console.Write(prompt);
        // ReadLine ger null när inmatningen tar slut. ?? anger vad som då ska hända.
        string? input = Console.ReadLine() ?? throw new EndOfStreamException();
        return input.Trim();
    }

    // Loopen frågar igen vid tom inmatning. return avslutar metoden när ett värde har angetts.
    public static string ReadRequiredText(string prompt)
    {
        while (true)
        {
            string input = ReadText(prompt);

            if (input.Length > 0)
            {
                return input;
            }

            Console.WriteLine("Ange ett värde. Fältet får inte vara tomt.");
        }
    }

    public static string? ReadOptionalText(string prompt)
    {
        string input = ReadText(prompt);
        // Villkor ? värde1 : värde2 väljer mellan två värden.
        // Tom text ger null, vilket betyder att sökningen inte ska begränsas av fältet.
        return input.Length == 0 ? null : input;
    }

    public static string ReadRegistrationNumber(string prompt)
    {
        while (true)
        {
            string input = ReadText(prompt);

            if (RegistrationNumberValidator.TryParse(input, out string registration))
            {
                return registration;
            }

            Console.WriteLine(RegistrationNumberValidator.ErrorMessage);
        }
    }

    public static string? ReadOptionalRegistrationNumber(string prompt)
    {
        while (true)
        {
            string? input = ReadOptionalText(prompt);

            if (input is null)
            {
                return null;
            }

            if (RegistrationNumberValidator.TryParse(input, out string registration))
            {
                return registration;
            }

            Console.WriteLine(RegistrationNumberValidator.ErrorMessage);
        }
    }

    public static int ReadInt(string prompt, int minimum, int maximum = int.MaxValue)
    {
        while (true)
        {
            string input = ReadText(prompt);
            if (int.TryParse(input, out int value) &&
                value >= minimum && value <= maximum)
            {
                return value;
            }

            if (maximum == int.MaxValue)
            {
                Console.WriteLine($"Ange ett giltigt heltal som är minst {minimum}.");
            }
            else
            {
                Console.WriteLine($"Ange ett heltal mellan {minimum} och {maximum}.");
            }
        }
    }

    public static int? ReadOptionalWheelCount()
    {
        while (true)
        {
            string input = ReadText("Antal hjul (lämna tomt för alla): ");

            if (input.Length == 0)
            {
                return null;
            }

            if (int.TryParse(input, out int value) && value >= 0)
            {
                return value;
            }

            Console.WriteLine("Ange ett heltal som är 0 eller större, eller lämna fältet tomt.");
        }
    }

    public static decimal ReadPositiveDecimal(string prompt)
    {
        while (true)
        {
            string input = ReadText(prompt);

            if (decimal.TryParse(input, NumberStyles.AllowDecimalPoint,
                CultureInfo.CurrentCulture, out decimal value) && value > 0)
            {
                return value;
            }

            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Console.WriteLine($"Ange ett positivt tal. Använd '{separator}' som decimaltecken.");
        }
    }
}
