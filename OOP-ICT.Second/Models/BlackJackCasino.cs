using OOP_ICT.Models.Exceptions;
using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Second.Models;

public class BlackJackCasino
{
    private readonly Dictionary<uint, BlackJackCasinoAccount> _accounts = new();
    public IReadOnlyDictionary<uint, BlackJackCasinoAccount> Accounts => _accounts;

    public BlackJackCasino() { }

    private BlackJackCasinoAccount _find(uint playerId)
    {
        if (!_accounts.ContainsKey(playerId))
        {
            throw new BlackJackCasinoAccountNotFoundException();
        }

        return _accounts[playerId];
    }

    public decimal Deposit(decimal amount, uint playerId)
    {
        var account = _find(playerId);
        account.Balance += amount;
        return account.Balance;
    }

    private bool _checkBalance(uint playerId, decimal amount)
    {
        return _find(playerId).Balance >= amount;
    }

    public decimal Withdraw(uint playerId, decimal amount)
    {
        if (!_checkBalance(playerId, amount))
        {
            throw new BlackJackCasinoNotEnoughBalanceException();
        }

        var account = _find(playerId);
        account.Balance -= amount;
        return account.Balance;
    }

    public BlackJackCasinoAccount CreateAccount(uint playerId, decimal amount)
    {
        var account = new BlackJackCasinoAccount(amount);
        _accounts[playerId] = account;
        return account;
    }
}