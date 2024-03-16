using OOP_ICT.Models;

namespace OOP_ICT.Third.Models;

public class Player
{
    public uint Id;
    public bool IsActive = false;

    private List<Card> _cards = new List<Card>();
    public IReadOnlyList<Card> Cards => _cards;

    public Player(uint id)
    {
        Id = id;
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public void ClearCards()
    {
        _cards.Clear();
    }

}