namespace OrderService.Application.Exceptions;

public class DatabaseUnavailableException : Exception
{
    public DatabaseUnavailableException(string message, Exception innerException)
        : base(message, innerException) { }
}
