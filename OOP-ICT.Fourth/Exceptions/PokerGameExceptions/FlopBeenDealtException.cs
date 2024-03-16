namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class FlopBeenDealtException : PokerGameException
{
    public FlopBeenDealtException() : base("Flop has already been dealt.") { }
    public FlopBeenDealtException(string message) : base(message) { }
    public FlopBeenDealtException(string message, Exception inner) : base(message, inner) { }
}