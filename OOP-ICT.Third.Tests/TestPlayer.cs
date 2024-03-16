using System.Security.AccessControl;
using OOP_ICT.Enums;
using OOP_ICT.Models;
using OOP_ICT.Third.Models;
using Xunit;

namespace OOP_ICT.Third.Tests;

public class TestPlayer
{
    [Fact]
    public void CheckPlayerHasEmptyHand()
    {
        var player = new Player(1u);
        
        Assert.Empty(player.Cards);
    }

    [Fact]
    public void CheckPlayerAddCard()
    {
        var player = new Player(1u);
        var card = new Card{Rank = CardRank.Ace, Suit = CardSuit.Clubs};
        player.AddCard(card);
        
        Assert.True(player.Cards.Count == 1);
        Assert.Contains(card, player.Cards);
    }

    [Fact]
    public void CheckPlayerClearCard()
    {
        var player = new Player(1u); 
        
        player.AddCard(new Card {Rank = CardRank.Ace, Suit = CardSuit.Clubs});
        player.AddCard(new Card {Rank = CardRank.King, Suit = CardSuit.Diamonds});
        player.ClearCards();
        
        Assert.Empty(player.Cards);
    }
}