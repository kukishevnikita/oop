using OOP_ICT.Models;

namespace OOP_ICT.Fourth;

public interface ICardPlayer
{
    string Username { get; }
    int Id { get; }
    
    IReadOnlyList<Card> Cards { get; }
    
    void AddCard(Card card);
    void FlushCards();
}