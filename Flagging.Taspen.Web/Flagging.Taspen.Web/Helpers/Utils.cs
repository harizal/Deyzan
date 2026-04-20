namespace Flagging.Taspen.Web.Helpers
{
    public static class Utils
    {
        public static string MaskName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // remove spaces (since your expected output ignores them)
            var cleaned = input.Replace(" ", "");

            if (cleaned.Length <= 3)
                return cleaned;

            return cleaned.Substring(0, 3) + new string('*', cleaned.Length - 3);
        }
    }
}