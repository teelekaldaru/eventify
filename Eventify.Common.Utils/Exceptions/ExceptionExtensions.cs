using System;
using System.Text;

namespace Eventify.Common.Utils.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string LogException(this Exception ex)
        {
            var stringBuilder = new StringBuilder(ex.Message);
            if (ex.InnerException != null)
            {
                stringBuilder.Append(". INNER:" + ex.InnerException.LogException());
            }

            return stringBuilder.ToString();
        }

        public static string LogInnerExceptions(this Exception ex)
        {
            var stringBuilder = new StringBuilder();
            if (ex.InnerException != null)
            {
                stringBuilder.Append(". INNER:" + ex.InnerException.LogException());
            }

            return stringBuilder.ToString();
        }
    }
}
