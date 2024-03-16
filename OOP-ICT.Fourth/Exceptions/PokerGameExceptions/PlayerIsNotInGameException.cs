namespace OOP_ICT.Fourth.Exceptions.PokerGameExceptions;

public class PlayerIsNotInGameException : PokerGameException
{
    public PlayerIsNotInGameException(string message) : base(message) { }
    public PlayerIsNotInGameException(string message, Exception innerException) : base(message, innerException) { }
    public PlayerIsNotInGameException() { }
}