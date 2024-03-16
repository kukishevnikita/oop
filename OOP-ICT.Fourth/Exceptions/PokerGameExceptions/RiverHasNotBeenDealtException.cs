namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class RiverHasNotBeenDealtException : PokerGameException
{
    public RiverHasNotBeenDealtException() : base("River has not been dealt yet.") { }
    public RiverHasNotBeenDealtException(string message) : base(message) { }
    public RiverHasNotBeenDealtException(string message, Exception inner) : base(message, inner) { }
}