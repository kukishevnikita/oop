namespace OOP_ICT.Fourth.Models;

using Structs;
using Enums;
using OOP_ICT.Models;
using OOP_ICT.Enums;

public class PokerEvaluator
{
    private static List<Card> GetCardsInSequence(IReadOnlyCollection<Card> cards, IReadOnlyCollection<CardRank> sequence)
    {
        return cards.Where(card => sequence.Contains(card.Rank)).ToList();
    }
    
    private static EvaluationHandlerResult HasHighCard(IReadOnlyCollection<Card> cards)
    {
        var highCard = cards.Max(card => card.Rank);
        return new EvaluationHandlerResult
        {
            IsSuccessful = true,
            Cards = cards.Where(card => card.Rank == highCard).ToList()
        };
    }

    private static EvaluationHandlerResult HasPair(IReadOnlyCollection<Card> cards)
    {
        cards = cards.OrderByDescending(card => card.Rank).ToList();
        var pair =
            cards.GroupBy(card => card.Rank).FirstOrDefault(group => group.Count() == 2);
        return pair != null
            ? new EvaluationHandlerResult
            {
                IsSuccessful = true,
                Cards = pair.ToList()
            }
            : new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            };
    }

    private static EvaluationHandlerResult HasTwoPairs(IReadOnlyCollection<Card> cards)
    {
        var pairs = cards.GroupBy(card => card.Rank)
            .Where(group => group.Count() == 2)
            .OrderByDescending(group => group.Key)
            .Take(2).ToList();
        if (pairs.Count < 2) return new EvaluationHandlerResult
        {
            IsSuccessful = false,
            Cards = new List<Card>()
        };
        var twoPairCards = pairs.SelectMany(pair => pair).ToList();
        return new EvaluationHandlerResult
        {
            IsSuccessful = true,
            Cards = twoPairCards
        };
    }

    private static EvaluationHandlerResult HasThreeOfAKind(IReadOnlyCollection<Card> cards)
    {
        cards = cards.OrderByDescending(card => card.Rank).ToList();
        var threeOfAKind =
            cards.GroupBy(card => card.Rank).FirstOrDefault(group => group.Count() == 3);
        return threeOfAKind != null
            ? new EvaluationHandlerResult
            {
                IsSuccessful = true,
                Cards = threeOfAKind.ToList()
            }
            : new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            };
    }

    private static EvaluationHandlerResult HasStraight(IReadOnlyCollection<Card> cards)
    {
        var orderedRanks = cards.OrderBy(card => card.Rank)
            .Select(card => card.Rank)
            .Distinct()
            .ToList();

        // Group sequences of cards where the difference between the ranks between two cards is 1
        var straightCardsRanks = Enumerable.Range(0, orderedRanks.Count)
            .Select(index => new { Rank = orderedRanks[index], Index = index })
            .GroupBy(rank => rank.Rank - rank.Index)
            .FirstOrDefault(group => group.Count() >= 5)?
            .Select(rank => rank.Rank)
            .ToList();
        
        if (straightCardsRanks?.Count >= 5)
        {
            var fiveStraightCards = GetCardsInSequence(cards, straightCardsRanks.TakeLast(5).ToList());
            return new EvaluationHandlerResult
            {
                IsSuccessful = true,
                Cards = fiveStraightCards
            };
        }
        
        var lowAceStraight = new List<CardRank>
            { CardRank.Ace, CardRank.Two, CardRank.Three, CardRank.Four, CardRank.Five };
        var hasLowAceStraight = lowAceStraight.All(rank => orderedRanks.Contains(rank));
        
        if (!hasLowAceStraight)
        {
            return new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            };
        }

        var lowAceStraightCards = GetCardsInSequence(cards, lowAceStraight);
        return new EvaluationHandlerResult
        {
            IsSuccessful = true,
            Cards = lowAceStraightCards
        };
    }

    private static EvaluationHandlerResult HasFlush(IReadOnlyCollection<Card> cards)
    {
        var flushSuit = cards.GroupBy(card => card.Suit)
            .FirstOrDefault(group => group.Count() >= 5);
        var flushCards = flushSuit?.OrderByDescending(card => card.Rank).Take(5).ToList();
        return flushCards != null
            ? new EvaluationHandlerResult
            {
                IsSuccessful = true,
                Cards = flushCards
            }
            : new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            };
    }

    private static EvaluationHandlerResult HasFullHouse(IReadOnlyCollection<Card> cards)
    {
        var hasThreeOfAKindResult = HasThreeOfAKind(cards);
        
        if (!hasThreeOfAKindResult.IsSuccessful)
        {
            return new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            };
        }
        
        var cardsWithoutThreeOfAKind = cards.Except(hasThreeOfAKindResult.Cards).ToList();
        
        var hasPairResult = HasPair(cardsWithoutThreeOfAKind);
        
        if (!hasThreeOfAKindResult.IsSuccessful || !hasPairResult.IsSuccessful)
        {
            return new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            };
        }
        
        var fullHouseCards = hasThreeOfAKindResult.Cards.Concat(hasPairResult.Cards).ToList();
        
        return fullHouseCards.Count < 5 
            ? new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            }
            : new EvaluationHandlerResult
            {
                IsSuccessful = true,
                Cards = fullHouseCards
            };
    }

    private static EvaluationHandlerResult HasFourOfAKind(IReadOnlyCollection<Card> cards)
    {
        var fourOfAKind =
            cards.GroupBy(card => card.Rank).FirstOrDefault(group => group.Count() == 4);
        return fourOfAKind != null
            ? new EvaluationHandlerResult
            {
                IsSuccessful = true,
                Cards = fourOfAKind.ToList()
            }
            : new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            };
    }

    private static EvaluationHandlerResult HasStraightFlush(IReadOnlyCollection<Card> cards)
    {
        var hasFlushResult = HasFlush(cards);
        
        if (!hasFlushResult.IsSuccessful)
        {
            return new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            };
        }
        
        var flushSuitCards = cards.Where(card => card.Suit == hasFlushResult.Cards.First().Suit).ToList();
        
        var hasStraightResult = HasStraight(flushSuitCards);
        
        if (!hasStraightResult.IsSuccessful || !hasFlushResult.IsSuccessful)
        {
            return new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            };
        }
        
        var straightFlushCards = hasStraightResult.Cards.Intersect(hasFlushResult.Cards).ToList();

        return new EvaluationHandlerResult
        {
            IsSuccessful = true,
            Cards = straightFlushCards
        };
    }

    private static EvaluationHandlerResult HasRoyalFlush(IReadOnlyCollection<Card> cards)
    {
        var hasStraightFlushResult = HasStraightFlush(cards);
        
        if (!hasStraightFlushResult.IsSuccessful || 
            hasStraightFlushResult.Cards.Max(card => card.Rank) != CardRank.Ace ||
            hasStraightFlushResult.Cards.Min(card => card.Rank) != CardRank.Ten)
        {
            return new EvaluationHandlerResult
            {
                IsSuccessful = false,
                Cards = new List<Card>()
            };
        }

        return new EvaluationHandlerResult
        {
            IsSuccessful = true,
            Cards = hasStraightFlushResult.Cards
        };
    }

    private static readonly Dictionary<PokerCombinations, Func<IReadOnlyCollection<Card>, EvaluationHandlerResult>>
        Combinations = new()
    {
        { PokerCombinations.RoyalFlush, HasRoyalFlush },
        { PokerCombinations.StraightFlush, HasStraightFlush },
        { PokerCombinations.FourOfAKind, HasFourOfAKind },
        { PokerCombinations.FullHouse, HasFullHouse },
        { PokerCombinations.Flush, HasFlush },
        { PokerCombinations.Straight, HasStraight },
        { PokerCombinations.ThreeOfAKind, HasThreeOfAKind },
        { PokerCombinations.TwoPairs, HasTwoPairs },
        { PokerCombinations.Pair, HasPair },
        { PokerCombinations.HighCard, HasHighCard }
    };

    public static EvaluationResult Evaluate(IReadOnlyCollection<Card> cards)
    {
        // Returns the highest combination found in the cards and the cards in the combination ordered by rank
        foreach (var combination in Combinations)
        {
            var result = combination.Value(cards);
            
            if (!result.IsSuccessful) continue;

            return new EvaluationResult
            {
                Combination = combination.Key,
                Cards = result.Cards,
                HighestCard = result.Cards.MaxBy(card => card.Rank) ?? null
            };
        }

        throw new ArgumentException("Invalid combination");
    }

    // HasTree result must not contain a HasPair result in full house
    // Exclude HasThreeOfAKind result from HasPair input in full house
    // HasStraight result must contain a HasFlush result in straight flush
    // First get HasFlush result, then filter input cards by HasFlush result suit
}
