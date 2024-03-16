namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class BetsAreUnequalException : PokerGameException
{
    public BetsAreUnequalException(string message) : base(message) { }
    public BetsAreUnequalException(string message, Exception inner) : base(message, inner) { }
    public BetsAreUnequalException() { }
}