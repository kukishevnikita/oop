using OOP_ICT.Models.Exceptions;
using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Second.Models;

public class Bank
{
    private readonly Dictionary<uint, BankAccount> _accounts = new();
    public IReadOnlyDictionary<uint, BankAccount> Accounts => _accounts;
    
    public Bank() { }
    
    private BankAccount _find(uint playerId)
    {
        if (!_accounts.ContainsKey(playerId))
        {
            throw new BankAccountNotFoundException();
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
            throw new BankNotEnoughBalanceException();
        }
    
        var account = _find(playerId);
        account.Balance -= amount;
        return account.Balance;
    }
    
    public BankAccount CreateAccount(uint playerId, decimal amount)
    {
        var account = new BankAccount(amount);
        _accounts[playerId] = account;
        return account;
    }
}