namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class FlopHasNotBeenDealtException : PokerGameException
{
    public FlopHasNotBeenDealtException() : base("Flop has not been dealt yet.") { }
    public FlopHasNotBeenDealtException(string message) : base(message) { }
    public FlopHasNotBeenDealtException(string message, Exception inner) : base(message, inner) { }
}