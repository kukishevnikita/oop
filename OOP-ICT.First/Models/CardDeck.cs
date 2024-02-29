using OOP_ICT.Enums;

namespace OOP_ICT.Models;

public class CardDeck
{
    public List<Card> Cards { get; set; }

    public CardDeck()
    {
        Cards = new List<Card>();
        foreach (var suit in Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>())
        {
            foreach (var rank in Enum.GetValues(typeof(CardRank)).Cast<CardRank>())
            {
                Cards.Add(
                    new Card {Rank = rank, Suit = suit}
                );
            }
        }
    }
}