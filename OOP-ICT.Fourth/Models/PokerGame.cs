using OOP_ICT.Enums;
using OOP_ICT.Fourth.Enums;
using OOP_ICT.Fourth.Exceptions.PokerGameExceptions;
using OOP_ICT.Fourth.Structs;

namespace OOP_ICT.Fourth.Models;

using OOP_ICT.Models;

public class PokerGame
{
    private readonly List<PokerPlayer> _players = new();
    public IReadOnlyList<PokerPlayer> Players => _players.AsReadOnly();
    public List<PokerPlayer> PlayersInGame => _players.FindAll(player => player.InGame);
    

    private readonly Dealer _dealer;
    private readonly PokerCasino _casino;

    private int _dealerIndex = -1;

    private readonly int _smallBlind = 10;
    private readonly int _bigBlind = 20;

    public int Pot { get; private set; }

    private int _currentPlayerIndex;

    private readonly Dictionary<PokerPlayer, uint> _bets = new();
    public IReadOnlyDictionary<PokerPlayer, uint> Bets => _bets.AsReadOnly();

    private readonly List<Card> _tableCards = new();
    public IReadOnlyList<Card> TableCards => _tableCards.AsReadOnly();

    public PokerGame(Dealer dealer, PokerCasino casino)
    {
        _dealer = dealer;
        _casino = casino;
    }

    public PokerGame(Dealer dealer, PokerCasino casino, int smallBlind, int bigBlind)
    {
        _dealer = dealer;
        _casino = casino;
        _smallBlind = smallBlind;
        _bigBlind = bigBlind;
    }

    public void AddPlayer(PokerPlayer player) => _players.Add(player);
    public void RemovePlayer(PokerPlayer player) => _players.Remove(player);
    public bool CheckIfPlayerExists(PokerPlayer player) => _players.Contains(player);

    private void RotateBlinds()
    {
        _dealerIndex = (_dealerIndex + 1) % _players.Count;
    }

    private void _placeBet(PokerPlayer player, uint amount)
    {
        if (!PlayersInGame.Contains(player))
            throw new PlayerIsNotInGameException();

        if (!_casino.CheckBalance(player.Id, amount))
            throw new PlayerDoesNotHaveEnoughChipsException();

        if (!_bets.TryAdd(player, amount))
            _bets[player] += amount;

        _casino.Withdraw(player.Id, amount);
        Pot += (int)amount;
    }

    private void MakeBlinds()
    {
        var smallBlindPlayer = PlayersInGame[(_dealerIndex + 1) % PlayersInGame.Count];
        var bigBlindPlayer = PlayersInGame[(_dealerIndex + 2) % PlayersInGame.Count];

        _placeBet(smallBlindPlayer, (uint)_smallBlind);
        _placeBet(bigBlindPlayer, (uint)_bigBlind);
        
        _currentPlayerIndex = (_dealerIndex + 3) % PlayersInGame.Count;
    }

    public void StartGame()
    {
        Flush();
        
        _dealer.ShuffleDeck();

        foreach (var player in PlayersInGame)
        {
            player.InGame = true;
            _bets.Add(player, 0);
        }

        RotateBlinds();
        MakeBlinds();

        for (var i = 0; i < 2; i++)
        {
            foreach (var player in PlayersInGame)
            {
                player.AddCard((Card)_dealer.DealCard());
            }
        }
    }

    public bool CheckBetsPlacedProperly()
    {
        return PlayersInGame
            .All(player => _bets.ContainsKey(player)) &&
               PlayersInGame
                   .All(player => _bets[player] == _bets.Values.Max());
    }

    private void _dealCardToTable() =>_tableCards.Add((Card)_dealer.DealCard());

    public void DealFlop()
    {
        if (_tableCards.Count != 0)
            throw new FlopBeenDealtException("Flop has already been dealt");

        if (PlayersInGame.Count < 2)
            throw new NotEnoughPlayersInGameException("Not enough players in game");

        foreach (var player in PlayersInGame)
        {
            _placeBet(player, (uint)_smallBlind);
        }

        for (var i = 0; i < 3; i++)
        {
            _dealCardToTable();
        }
    }

    public void DealTurn()
    {
        if (_tableCards.Count != 3)
            throw new FlopHasNotBeenDealtException("Flop has not been dealt yet");

        _dealCardToTable();
    }

    public void DealRiver()
    {
        if (_tableCards.Count != 4)
            throw new TurnHasNotBeenDealtException("Turn has not been dealt yet");

        _dealCardToTable();
    }

    public void DealLeftoverCards()
    {
        if (_tableCards.Count == 0)
            throw new FlopHasNotBeenDealtException("Flop has not been dealt yet");
            
        if (_tableCards.Count == 3)
            DealRiver();
        
        if (_tableCards.Count == 4)
            DealTurn();
    }

    private Tuple<IReadOnlyCollection<PokerPlayer>, PokerCombinations> _getWinners()
    {
        var players = PlayersInGame;
        var playerResults = new Dictionary<PokerPlayer, EvaluationResult>();

        foreach (var player in players)
        {
            var cards = new List<Card>();
            cards.AddRange(player.Cards);
            cards.AddRange(_tableCards);

            var result = PokerEvaluator.Evaluate(cards);
            playerResults.Add(player, result);
        }

        var winners = new List<PokerPlayer>();
        var highestCombination = PokerCombinations.HighCard;
        var highestCard = new Card(CardSuit.Clubs, CardRank.Two);

        foreach (var player in players)
        {
            var result = playerResults[player];

            if (result.Combination > highestCombination)
            {
                highestCombination = result.Combination;
                highestCard = result.HighestCard;
                winners.Clear();
                winners.Add(player);
            }
            else if (result.Combination == highestCombination)
            {
                if (highestCard != null && result.HighestCard != null && result.HighestCard.Rank > highestCard.Rank)
                {
                    highestCard = result.HighestCard;
                    winners.Clear();
                    winners.Add(player);
                }
                else if (Equals(highestCard, result.HighestCard))
                {
                    winners.Add(player);
                }
            }
        }

        return new Tuple<IReadOnlyCollection<PokerPlayer>, PokerCombinations>(winners, highestCombination);
    }

    public Tuple<IReadOnlyCollection<PokerPlayer>, PokerCombinations> EndGame()
    {
        if (_tableCards.Count != 5)
            throw new RiverHasNotBeenDealtException("River has not been dealt yet");

        var winners = _getWinners();

        var reward = Pot / winners.Item1.Count;

        foreach (var winner in winners.Item1)
        {
            _casino.Deposit(winner.Id, (uint)reward);
        }

        foreach (var player in PlayersInGame)
        {
            player.InGame = false;
        }
        
        return winners;
    }

    public void Fold(PokerPlayer player)
    {
        if (!PlayersInGame.Contains(player))
            throw new PlayerIsNotInGameException("Player is not in game");

        player.InGame = false;
    }

    public void Check(PokerPlayer player)
    {
        if (!PlayersInGame.Contains(player))
            throw new PlayerIsNotInGameException("Player is not in game");
    }
    
    public void Call(PokerPlayer player)
    {
        if (!PlayersInGame.Contains(player))
            throw new PlayerIsNotInGameException("Player is not in game");

        var highestBet = _bets.Values.Max();
        
        _casino.Withdraw(player.Id, highestBet - _bets.GetValueOrDefault(player, (uint)0));
        _bets[player] = highestBet;
        Pot += (int)(highestBet - _bets[player]);
    }
    
    public void Raise(PokerPlayer player, uint amount)
    {
        if (!PlayersInGame.Contains(player))
            throw new PlayerIsNotInGameException("Player is not in game");

        var highestBet = _bets.Values.Max();

        if (_bets[player] + amount < highestBet)
            throw new BetIsNotHighestException("Player's bet is not the highest");

        _casino.Withdraw(player.Id, amount);
        _bets[player] += amount;
        Pot += (int)amount;
    }

    private void Flush()
    {
        foreach (var player in _players)
        {
            player.FlushCards();
        }
        
        _bets.Clear();
        _tableCards.Clear();
        _dealer.SetDeck(new CardDeck());
        Pot = 0;
    }

    public void NextPlayer()
    {
        if (PlayersInGame.Count < 2)
            throw new NotEnoughPlayersInGameException("Not enough players in game");
        
        do
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % Players.Count;
        } while (!Players[_currentPlayerIndex].InGame);
    }
    
    public PokerPlayer GetCurrentPlayer()
    {
        return Players[_currentPlayerIndex];
    }
}