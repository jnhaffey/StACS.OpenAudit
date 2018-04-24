namespace StACS.OpenAudit.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SanitizeWhitespaceAsNull(this string source)
        {
            return string.IsNullOrWhiteSpace(source) ? null : source;
        }
    }
}