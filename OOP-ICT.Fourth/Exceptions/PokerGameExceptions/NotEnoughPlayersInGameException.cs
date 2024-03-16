namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class NotEnoughPlayersInGameException : PokerGameException
{
    public NotEnoughPlayersInGameException(string message) : base(message) { }
    public NotEnoughPlayersInGameException(string message, Exception inner) : base(message, inner) { }
    public NotEnoughPlayersInGameException() { }
}