namespace Booking.Shared.Exceptions;

[Serializable]
public class InternalServerException : Exception
{
    public InternalServerException() { }

    public InternalServerException(string message)
        : base(message) { }

    public InternalServerException(string message, Exception inner)
        : base(message, inner) { }
}
