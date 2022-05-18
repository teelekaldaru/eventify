namespace Eventify.Common.Utils.Messages
{
    public static class MessageBuilder
    {
        public static string BuildMessage(string header, string description)
        {
            var obj = string.IsNullOrWhiteSpace(header) ? "Error" : header;
            var text = string.IsNullOrWhiteSpace(description) ? null : ": " + description;
            return obj + text;
        }
    }
}
