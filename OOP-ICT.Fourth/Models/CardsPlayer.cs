using OOP_ICT.Third.Models;

namespace OOP_ICT.Fourth.Models;

public class CardsPlayer : BlackJackPlayer, ICardPlayer
{
    protected CardsPlayer(int id, string username = "") : base(id, username) { }
    
    public new int GetScore()
    {
        throw new NotImplementedException();
    }
    
    public new bool IsBusted()
    {
        throw new NotImplementedException();
    }
    
    public new bool IsBlackJack()
    {
        throw new NotImplementedException();
    }
}
