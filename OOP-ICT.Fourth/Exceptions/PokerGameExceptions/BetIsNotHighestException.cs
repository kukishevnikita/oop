namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class BetIsNotHighestException : Exception
{
    public BetIsNotHighestException() : base() { }
    public BetIsNotHighestException(string message) : base(message) { }
    public BetIsNotHighestException(string message, Exception innerException) : base(message, innerException) { }
}