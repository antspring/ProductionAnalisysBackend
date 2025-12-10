using System.Security.Cryptography;
using Services.Services.Interfaces;

namespace Services.Services.Implementations;

public class PasswordGenerateService : IPasswordGenerateService
{
    public string Generate(int length)
    {
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string special = "!@#$%^&*()-_=+[]{}|;:,.<>?";

        var password = new char[length];
        var rng = RandomNumberGenerator.Create();

        password[0] = GetRandomChar(upper, rng);
        password[1] = GetRandomChar(digits, rng);
        password[2] = GetRandomChar(special, rng);
        password[3] = GetRandomChar(lower, rng);

        const string allChars = lower + upper + digits + special;

        for (var i = 4; i < length; i++)
        {
            password[i] = GetRandomChar(allChars, rng);
        }

        return Shuffle(password, rng);
    }

    private static char GetRandomChar(string chars, RandomNumberGenerator rng)
    {
        var buffer = new byte[4];
        rng.GetBytes(buffer);
        var index = BitConverter.ToInt32(buffer, 0) % chars.Length;
        if (index < 0) index = -index;
        return chars[index];
    }

    private static string Shuffle(char[] chars, RandomNumberGenerator rng)
    {
        for (var i = chars.Length - 1; i > 0; i--)
        {
            var buffer = new byte[4];
            rng.GetBytes(buffer);
            var j = BitConverter.ToInt32(buffer, 0) % (i + 1);
            if (j < 0) j = -j;

            (chars[i], chars[j]) = (chars[j], chars[i]);
        }

        return new string(chars);
    }
}