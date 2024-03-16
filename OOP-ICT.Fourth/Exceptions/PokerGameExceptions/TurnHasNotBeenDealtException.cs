namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class TurnHasNotBeenDealtException : PokerGameException
{
    public TurnHasNotBeenDealtException() : base("Turn has not been dealt yet.") { }
    public TurnHasNotBeenDealtException(string message) : base(message) { }
    public TurnHasNotBeenDealtException(string message, Exception inner) : base(message, inner) { }
}