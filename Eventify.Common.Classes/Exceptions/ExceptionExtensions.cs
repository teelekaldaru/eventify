using System;
using System.Text;

namespace Eventify.Common.Classes.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string LogException(this Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder(ex.Message);
            if (ex.InnerException != null)
            {
                stringBuilder.Append(". INNER:" + ex.InnerException.LogException());
            }

            return stringBuilder.ToString();
        }

        public static string LogInnerExceptions(this Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (ex.InnerException != null)
            {
                stringBuilder.Append(". INNER:" + ex.InnerException.LogException());
            }

            return stringBuilder.ToString();
        }
    }
}
