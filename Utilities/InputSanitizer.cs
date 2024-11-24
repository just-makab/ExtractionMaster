using System.Text.RegularExpressions;

namespace EM_WebApp.Utilities
{
    public class InputSanitizer : IInputSanitizer
    {
        public string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Remove potentially dangerous characters
            return Regex.Replace(input, @"[^\w\s@.-]", string.Empty);
        }
    }
}
