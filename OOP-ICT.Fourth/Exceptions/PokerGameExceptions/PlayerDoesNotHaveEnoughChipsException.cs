namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class PlayerDoesNotHaveEnoughChipsException : PokerGameException
{
    public PlayerDoesNotHaveEnoughChipsException(string message) : base(message) { }
    public PlayerDoesNotHaveEnoughChipsException(string message, Exception inner) : base(message, inner) { }
    public PlayerDoesNotHaveEnoughChipsException() {}
}