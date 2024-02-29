using OOP_ICT.Models;
using Xunit;
using Xunit.Abstractions;

namespace OOP_ICT.FIrst.Tests;

public class TestDealerFunctions
{

    [Fact]
    public void CheckShuffle()
    {
        var deck = new CardDeck();
        var dealer = new Dealer(deck);
        var card = deck.Cards[0];
        
        dealer.Shuffle();
        Assert.NotEqual(card, deck.Cards[0]);
    }

    [Fact]
    public void CheckFirstCard()
    {
        var deck = new CardDeck();
        var dealer = new Dealer(deck);
        
        var expectedCard = deck.Cards.First();

        var card = dealer.Giveaway();
        
        Assert.Equal(expectedCard, card);
    }

    [Fact]
    public void DeleteCard()
    {
        var deck = new CardDeck();
        var dealer = new Dealer(deck);

        var lenghtBefore = deck.Cards.Count;

        dealer.Giveaway();

        var lenghtAfter = deck.Cards.Count;
        
        Assert.Equal(lenghtBefore - 1, lenghtAfter);

    }
}