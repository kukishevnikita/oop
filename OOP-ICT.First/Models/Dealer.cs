namespace OOP_ICT.Models;

public class Dealer
{
    public CardDeck Deck;
    public Dealer(CardDeck deck)
    {
        Deck = deck;
    }
    private readonly Random _random = new Random();
    public void Shuffle()
    {
        var random = _random.Next(1, 52);
        var half = Deck.Cards.Count / 2;
        for (var j = 0; j <= random; j++)
        {
            var tmp1 = Deck.Cards.GetRange(0, half);
            var tmp2 = Deck.Cards.GetRange(half, half);
            var tmp3 = new List<Card>();
            for (var i = 0; i < half; i++)
            {
                tmp3.Add(tmp2[i]);
                tmp3.Add(tmp1[i]);
            }
            Deck.Cards = tmp3;
        }
    }

    public Card Giveaway()
    {
        var card = Deck.Cards.First();
        Deck.Cards.Remove(card);
        return card;
    }

    public void SetDeck(CardDeck cardDeck)
    {
        Deck = cardDeck;
    }
}