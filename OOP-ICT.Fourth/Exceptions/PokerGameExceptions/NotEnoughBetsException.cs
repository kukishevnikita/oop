namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class NotEnoughBetsException : PokerGameException
{
    public NotEnoughBetsException(string message) : base(message) { }
    public NotEnoughBetsException(string message, Exception inner) : base(message, inner) { }
    public NotEnoughBetsException() { }
}