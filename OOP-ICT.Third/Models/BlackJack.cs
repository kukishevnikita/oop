using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks.Sources;
using OOP_ICT.Enums;
using OOP_ICT.Models;

namespace OOP_ICT.Third.Models;

public class BlackJack
{
    private readonly List<Player> _players = new List<Player>();
    public IReadOnlyList<Player> Players => _players;
    public IReadOnlyList<Player> PlayersInGame => _players.FindAll(player => player.IsActive);
    
    private readonly Dictionary<Player, uint> _bets = new();
    public IReadOnlyDictionary<Player, uint> Bets => _bets;

    private Dealer _dealer = new Dealer(new CardDeck());
    private Player _dealerPlayer = new Player(0u);
    public IReadOnlyList<Card> DealerCards => _dealerPlayer.Cards;

    public void AddPlayer(Player player) 
    {
        _players.Add(player);
    }

    public void DeletePlayer(Player player)
    {
        player.ClearCards();
        _bets.Remove(player);
        _players.Remove(player);
    }

    public void StartGame()
    {
        DealCardToDealer();
        foreach (var player in _players)
        {
            DealCardToPlayer(player);
            DealCardToPlayer(player);
        }
    }

    public void PrepareGame()
    {
        foreach (var player in _players)
        {
            player.ClearCards();
            player.IsActive = true;
        }
        
        _dealer.SetDeck(new CardDeck());
        _dealerPlayer.ClearCards();
        
        _bets.Clear();
    }

    public void MakeBet(Player player, uint amount)
    {
        _bets[player] = amount;
    }

    public void DealCardToDealer() => DealCardToPlayer(_dealerPlayer);

    public void DealRemainingCardsToDealer()
    {
        while (CalculateScore(_dealerPlayer.Cards) < 17)
        {
            DealCardToDealer();
        }
    }

    public void DealCardToPlayer(Player player)
    {
        var card = _dealer.Giveaway();
        player.AddCard(card);
    }

    public IEnumerable<BlackJackGamePlayerResult> CalculateWinners()
    {
        var dealerScore = CalculateScore(_dealerPlayer.Cards);
        var winnersWithScore =  _players
            .Select(player => new {Player = player, Score = CalculateScore(player.Cards), Bet = _bets[player]})
            .Where(item => item.Score <= 21)
            .Where(item => item.Score > dealerScore)
            .Select(item => new
            BlackJackGamePlayerResult{
                Player = item.Player, 
                Score = item.Score, 
                Winnings = (uint) (item.Bet * item.Score == 21 && item.Player.Cards.Count == 2 ? 
                    2 : 1.5
                )
            });
        return winnersWithScore;
    }
    

    public static uint CalculateScore(IEnumerable<Card> cards)
    {
        var cardToValue = new Dictionary<CardRank, uint>
        {
            {CardRank.Ace, 11},
            {CardRank.Two, 2},
            {CardRank.Three, 3},
            {CardRank.Four, 4},
            {CardRank.Five, 5},
            {CardRank.Six, 6},
            {CardRank.Seven, 7},
            {CardRank.Eight,8},
            {CardRank.Nine, 9},
            {CardRank.Ten, 10},
            {CardRank.Jack, 10},
            {CardRank.Queen, 10},
            {CardRank.King, 10}
        };

        var cardSum = 0u;
        foreach (var card in cards)
        {
            var cardValue = cardToValue[card.Rank];
            if (card.Rank == CardRank.Ace && cardSum + cardValue > 21)
            {
                cardValue = 1u;
            }
            cardSum += cardValue;
            
        }

        return cardSum;
    }
    
}