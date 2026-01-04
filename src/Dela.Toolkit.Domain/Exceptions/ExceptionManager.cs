namespace Dela.Toolkit.Domain.Exceptions;

public abstract class ExceptionManager : Exception
{
    protected ExceptionManager(string message) : base(message)
    {

    }
}