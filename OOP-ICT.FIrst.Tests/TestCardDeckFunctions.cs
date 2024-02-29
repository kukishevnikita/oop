using OOP_ICT.Models;
using Xunit;
using Xunit.Abstractions;

namespace OOP_ICT.FIrst.Tests;

public class TestCardDeckFunctions
{
    [Fact]
    public void CheckLenght()
    {
        var deck = new CardDeck();
        var lenght = deck.Cards.Count;
        
        Assert.Equal(52, lenght);
    }
}