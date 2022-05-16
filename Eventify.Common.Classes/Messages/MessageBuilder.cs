namespace Eventify.Common.Classes.Messages
{
    public static class MessageBuilder
    {
        public static string BuildMessage(string header, string description)
        {
            string obj = string.IsNullOrWhiteSpace(header) ? "Error" : header;
            string? text = string.IsNullOrWhiteSpace(description) ? null : ": " + description;
            return obj + text;
        }
    }
}
