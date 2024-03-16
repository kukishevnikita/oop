namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class PokerGameException : Exception
{
    public PokerGameException(string message) : base(message) { }
    public PokerGameException(string message, Exception innerException) : base(message, innerException) { }
    public PokerGameException() { }
}