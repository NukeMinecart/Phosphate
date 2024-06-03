using System.IO;

namespace Phosphate.Validation;

public static class Validators
{
    public static bool ValidateDirectory(string? value)
    {
        return value != null && Directory.Exists(value);
    }
}