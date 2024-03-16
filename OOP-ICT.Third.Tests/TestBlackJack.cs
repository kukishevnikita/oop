using OOP_ICT.Enums;
using OOP_ICT.Models;
using OOP_ICT.Third.Models;
using Xunit;

namespace OOP_ICT.Third.Tests;

public class TestBlackJack
{
    [Fact]
    public void CheckAddPlayer()
    {
        var player = new Player(1u);
        var blackJack = new BlackJack();
        
        blackJack.AddPlayer(player);

        Assert.Contains(player, blackJack.Players);
        Assert.True(blackJack.Players.Count == 1);
    }

    [Fact]
    public void CheckDeletePlayer()
    {
        var player = new Player(1u);
        var blackJack = new BlackJack();
        
        blackJack.AddPlayer(player);
        blackJack.MakeBet(player, 100);
        blackJack.DealCardToPlayer(player);
        
        blackJack.DeletePlayer(player);

        Assert.Empty(blackJack.Players);
        Assert.DoesNotContain(player, blackJack.Bets);
        Assert.Empty(player.Cards);
    }

    [Fact]
    public void CheckPrepareGame()
    {
        var player = new Player(1u);
        var blackJack = new BlackJack();
        
        blackJack.AddPlayer(player);
        blackJack.DealCardToPlayer(player);
        blackJack.DealCardToDealer();
        blackJack.MakeBet(player, 100);
        blackJack.PrepareGame();
        
        Assert.Empty(player.Cards);
        Assert.Empty(blackJack.DealerCards);
        Assert.Empty(blackJack.Bets);
        Assert.True(player.IsActive);
        

    }

    [Fact]
    public void CheckStartGame()
    {
        var player = new Player(1u);
        var blackJack = new BlackJack();
        
        blackJack.AddPlayer(player);
        
        blackJack.StartGame();
        
        Assert.Equal(2, player.Cards.Count);
        Assert.Single(blackJack.DealerCards);
    }

    [Fact]
    public void CheckMakeBet()
    {
        var player = new Player(1u);
        var blackJack = new BlackJack();
        
        blackJack.MakeBet(player, 100);

        Assert.Contains(player, blackJack.Bets);
        Assert.Equal(100u, blackJack.Bets[player]);
    }

    [Fact]
    public void CheckEvaluate1()
    {
        var cards = new List<Card>
        {
            new Card {Rank = CardRank.Ten, Suit = CardSuit.Diamonds},
            new Card {Rank = CardRank.Ace, Suit = CardSuit.Clubs}
        };

        var cardSum = BlackJack.CalculateScore(cards);
        
        Assert.Equal(21u, cardSum);
    }

    [Fact]
    public void CheckEvaluate2()
    {
        var cards = new List<Card>
        {
            new Card {Rank = CardRank.Ten, Suit = CardSuit.Diamonds},
            new Card {Rank = CardRank.Seven, Suit = CardSuit.Hearts},
            new Card {Rank = CardRank.Ace, Suit = CardSuit.Clubs}
        };

        var cardSum = BlackJack.CalculateScore(cards);
        
        Assert.Equal(18u, cardSum);
    }
}