using System.Text;

namespace BookStoreAPI._Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetDetailException(this Exception exception)
        {
            var stringBuilder = new StringBuilder();

            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);
                exception = exception?.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}
