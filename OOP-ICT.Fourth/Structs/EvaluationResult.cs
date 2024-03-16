using OOP_ICT.Enums;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Structs;
using OOP_ICT.Fourth.Enums;

public struct EvaluationResult
{
    public PokerCombinations Combination;
    public Card? HighestCard;
    public IReadOnlyCollection<Card> Cards;
}