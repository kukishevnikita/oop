using System.Security.Authentication.ExtendedProtection;
using OOP_ICT.Fourth.Models;
using OOP_ICT.Fourth.Enums;
using OOP_ICT.Models;
using OOP_ICT.Enums;

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
