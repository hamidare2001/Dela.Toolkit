namespace Dela.Toolkit.Extensions;

public static class ExceptionExtension
{
    public static string AllMessages(this Exception exception)
    {
        var message = exception?.Message + "\n";

        var ex = exception;
        while (ex != null)
        { 
            if (ex.InnerException != null)
            {
                message += ex.InnerException.Message + "\n";
                ex = ex.InnerException;
            } else
                return message;
        }

        return message;
    }
}