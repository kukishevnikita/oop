using OOP_ICT.Enums;
using OOP_ICT.Models;
using OOP_ICT.Fourth.Enums;
using OOP_ICT.Fourth.Models;

namespace OOP_ICT.Fourth.Tests;

using Xunit;

public class TestEvaluatorFunctions
{
    [Fact]
    public void TestEvaluatorFunctions_EvaluateRoyalFlush1_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Clubs, CardRank.King),
            new(CardSuit.Clubs, CardRank.Queen),
            new(CardSuit.Clubs, CardRank.Jack),
            new(CardSuit.Clubs, CardRank.Ten),
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Clubs, CardRank.Eight)
        };

        var royalFlush = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Clubs, CardRank.King),
            new(CardSuit.Clubs, CardRank.Queen),
            new(CardSuit.Clubs, CardRank.Jack),
            new(CardSuit.Clubs, CardRank.Ten),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.RoyalFlush, result.Combination);
        Assert.True(royalFlush.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateRoyalFlush2_False()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Diamonds, CardRank.King),
            new(CardSuit.Diamonds, CardRank.Queen),
            new(CardSuit.Diamonds, CardRank.Jack),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Eight),
            new(CardSuit.Diamonds, CardRank.Seven)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.NotEqual(PokerCombinations.RoyalFlush, result.Combination);
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateStraightFlush1_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Clubs, CardRank.Eight),
            new(CardSuit.Clubs, CardRank.Seven),
            new(CardSuit.Clubs, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Clubs, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Three)
        };

        var straightFlush = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Clubs, CardRank.Eight),
            new(CardSuit.Clubs, CardRank.Seven),
            new(CardSuit.Clubs, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Five),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.StraightFlush, result.Combination);
        Assert.True(straightFlush.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateStraightFlush2_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Diamonds, CardRank.Five),
            new(CardSuit.Diamonds, CardRank.Four),
            new(CardSuit.Diamonds, CardRank.Three),
            new(CardSuit.Diamonds, CardRank.Two),
            new(CardSuit.Diamonds, CardRank.Ace),
            new(CardSuit.Clubs, CardRank.Four),
            new(CardSuit.Spades, CardRank.Three)
        };

        var straightFlush = new List<Card>
        {
            new(CardSuit.Diamonds, CardRank.Five),
            new(CardSuit.Diamonds, CardRank.Four),
            new(CardSuit.Diamonds, CardRank.Three),
            new(CardSuit.Diamonds, CardRank.Two),
            new(CardSuit.Diamonds, CardRank.Ace),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.StraightFlush, result.Combination);
        Assert.True(straightFlush.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateStraightFlush3_False()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Eight),
            new(CardSuit.Diamonds, CardRank.Seven),
            new(CardSuit.Clubs, CardRank.Six),
            new(CardSuit.Diamonds, CardRank.Four),
            new(CardSuit.Diamonds, CardRank.Three),
            new(CardSuit.Diamonds, CardRank.Two)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.NotEqual(PokerCombinations.StraightFlush, result.Combination);
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateFourOfAKind1_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Nine),
            new(CardSuit.Spades, CardRank.Nine),
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Clubs, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Three)
        };

        var fourOfAKind = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Nine),
            new(CardSuit.Spades, CardRank.Nine),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.FourOfAKind, result.Combination);
        Assert.True(fourOfAKind.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateFourOfAKind3_False()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Nine),
            new(CardSuit.Spades, CardRank.Eight),
            new(CardSuit.Clubs, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Three),
            new(CardSuit.Clubs, CardRank.Two)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.NotEqual(PokerCombinations.FourOfAKind, result.Combination);
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateFullHouse1_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Nine),
            new(CardSuit.Spades, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Three),
            new(CardSuit.Clubs, CardRank.Two)
        };

        var fullHouse = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Nine),
            new(CardSuit.Spades, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Four),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.FullHouse, result.Combination);
        Assert.Equal(fullHouse, result.Cards);
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateFullHouse2_False()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Eight),
            new(CardSuit.Hearts, CardRank.Four),
            new(CardSuit.Spades, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Three),
            new(CardSuit.Clubs, CardRank.Two)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.NotEqual(PokerCombinations.FullHouse, result.Combination);
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateFullHouse3_False()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Four),
            new(CardSuit.Spades, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Three),
            new(CardSuit.Clubs, CardRank.Two),
            new(CardSuit.Clubs, CardRank.Ace)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.NotEqual(PokerCombinations.FullHouse, result.Combination);
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateFlush1_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Two),
            new(CardSuit.Diamonds, CardRank.Six),
            new(CardSuit.Diamonds, CardRank.Seven),
            new(CardSuit.Clubs, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Eight),
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Clubs, CardRank.Ten)
        };

        var flush = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Two),
            new(CardSuit.Clubs, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Eight),
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Clubs, CardRank.Ten)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.Flush, result.Combination);
        Assert.True(flush.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateFlush3_False()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Eight),
            new(CardSuit.Diamonds, CardRank.Seven),
            new(CardSuit.Diamonds, CardRank.Six),
            new(CardSuit.Diamonds, CardRank.Five),
            new(CardSuit.Diamonds, CardRank.Four),
            new(CardSuit.Diamonds, CardRank.Three)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.NotEqual(PokerCombinations.Flush, result.Combination);
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateStraight1_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Eight),
            new(CardSuit.Hearts, CardRank.Seven),
            new(CardSuit.Spades, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Clubs, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Three)
        };

        var straight = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Eight),
            new(CardSuit.Hearts, CardRank.Seven),
            new(CardSuit.Spades, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Five),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.Straight, result.Combination);
        Assert.True(straight.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateStraight2_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Diamonds, CardRank.Four),
            new(CardSuit.Hearts, CardRank.Three),
            new(CardSuit.Spades, CardRank.Two),
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Clubs, CardRank.King),
            new(CardSuit.Clubs, CardRank.Queen)
        };

        var straight = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Diamonds, CardRank.Four),
            new(CardSuit.Hearts, CardRank.Three),
            new(CardSuit.Spades, CardRank.Two),
            new(CardSuit.Clubs, CardRank.Ace),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.Straight, result.Combination);
        Assert.True(straight.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateStraight3_False()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Eight),
            new(CardSuit.Hearts, CardRank.Seven),
            new(CardSuit.Spades, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Three),
            new(CardSuit.Clubs, CardRank.Two),
            new(CardSuit.Clubs, CardRank.Ace)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.NotEqual(PokerCombinations.Straight, result.Combination);
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateThreeOfAKind1_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Nine),
            new(CardSuit.Spades, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Clubs, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Three)
        };

        var threeOfAKind = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Nine),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.ThreeOfAKind, result.Combination);
        Assert.True(threeOfAKind.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateThreeOfAKind2_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Nine),
            new(CardSuit.Spades, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Clubs, CardRank.Two),
            new(CardSuit.Clubs, CardRank.Three)
        };

        var threeOfAKind = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Nine),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.ThreeOfAKind, result.Combination);
        Assert.True(threeOfAKind.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateThreeOfAKind3_False()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Four),
            new(CardSuit.Spades, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Three),
            new(CardSuit.Clubs, CardRank.Two)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.NotEqual(PokerCombinations.ThreeOfAKind, result.Combination);
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateTwoPairs1_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Four),
            new(CardSuit.Spades, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Clubs, CardRank.Three),
            new(CardSuit.Clubs, CardRank.Two)
        };

        var twoPairs = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Four),
            new(CardSuit.Spades, CardRank.Four),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.TwoPairs, result.Combination);
        Assert.True(twoPairs.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateTwoPairs2_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Four),
            new(CardSuit.Spades, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Clubs, CardRank.Three),
            new(CardSuit.Clubs, CardRank.Two)
        };

        var twoPairs = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Four),
            new(CardSuit.Spades, CardRank.Four),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.TwoPairs, result.Combination);
        Assert.True(twoPairs.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateTwoPairs3_False()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Four),
            new(CardSuit.Spades, CardRank.Five),
            new(CardSuit.Clubs, CardRank.Three),
            new(CardSuit.Clubs, CardRank.Two),
            new(CardSuit.Clubs, CardRank.Ace)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.NotEqual(PokerCombinations.TwoPairs, result.Combination);
    }


    [Fact]
    public void TestEvaluatorFunctions_EvaluatePair1_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
            new(CardSuit.Hearts, CardRank.Four),
            new(CardSuit.Spades, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Diamonds, CardRank.Ace),
            new(CardSuit.Spades, CardRank.Three)
        };

        var pair = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Nine),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.Pair, result.Combination);
        Assert.True(pair.All(result.Cards.Contains));
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluatePair3_False()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Eight),
            new(CardSuit.Hearts, CardRank.Four),
            new(CardSuit.Spades, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Clubs, CardRank.Three),
            new(CardSuit.Clubs, CardRank.Two)
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.NotEqual(PokerCombinations.Pair, result.Combination);
    }

    [Fact]
    public void TestEvaluatorFunctions_EvaluateHighCard1_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Hearts, CardRank.Ace),
            new(CardSuit.Spades, CardRank.King),
            new(CardSuit.Clubs, CardRank.Jack),
            new(CardSuit.Diamonds, CardRank.Eight),
            new(CardSuit.Clubs, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Four),
            new(CardSuit.Clubs, CardRank.Two)
        };

        var highCard = new Card(CardSuit.Hearts, CardRank.Ace);

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.HighCard, result.Combination);
        Assert.Equal(highCard, result.HighestCard);
    }
    
    [Fact]
    public void TestEvaluatorFunctions_TestTwoPairCustom_True()
    {
        var hand = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Queen),
            new(CardSuit.Clubs, CardRank.King),
            new(CardSuit.Hearts, CardRank.Three),
            new(CardSuit.Diamonds, CardRank.Three),
            new(CardSuit.Hearts, CardRank.Ten),
            new(CardSuit.Diamonds, CardRank.Queen),
            new(CardSuit.Hearts, CardRank.King)
        };

        var twoPairs = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Queen),
            new(CardSuit.Diamonds, CardRank.Queen),
            new(CardSuit.Clubs, CardRank.King),
            new(CardSuit.Hearts, CardRank.King),
        };

        var result = PokerEvaluator.Evaluate(hand);

        Assert.Equal(PokerCombinations.TwoPairs, result.Combination);
        Assert.True(twoPairs.All(result.Cards.Contains));
    }
}