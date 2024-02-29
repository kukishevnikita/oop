using System.Security.Principal;
using OOP_ICT.Models;
using OOP_ICT.Models.Exceptions;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;
using Xunit;
using Xunit.Abstractions;

namespace OOP_ICT.Second.Tests;

public class TestBlackJackCasino
{
    [Fact]
    public void CheckBalance()
    {
        var casino = new BlackJackCasino();
        var account = casino.CreateAccount(1, 1000);
        
        Assert.True(casino.Accounts.ContainsKey(1));
        Assert.Equal(1000, account.Balance);
        
    }

    [Fact]
    public void CheckDeposit()
    {
        var casino = new BlackJackCasino();
        var account = casino.CreateAccount(1, 1000);
        var balance = account.Balance;

        casino.Deposit(200, 1);
        
        Assert.Equal(balance + 200, account.Balance);
        
    }

    [Fact]
    public void CheckWithdrawException()
    {
        var casino = new BlackJackCasino();
        var account = casino.CreateAccount(1, 1000);

        Assert.Throws<BlackJackCasinoNotEnoughBalanceException>(() => casino.Withdraw(1, 10000));
    }

    [Fact]
    public void CheckSuccessfullyWithdraw()
    {
        var casino = new BlackJackCasino();
        var account = casino.CreateAccount(1, 1000);
        var balance = account.Balance;

        casino.Withdraw(1, 200);
        
        Assert.Equal(balance - 200, account.Balance);
    }

    [Fact]
    public void CheckDepositAccountException()
    {
        var casino = new BlackJackCasino();
        var account = casino.CreateAccount(1, 1000);

        Assert.Throws<BlackJackCasinoAccountNotFoundException>((() => casino.Deposit(200, 2)));
    }

    [Fact]
    public void CheckWithdrawAccountException()
    {
        var casino = new BlackJackCasino();
        var account = casino.CreateAccount(1, 1000);

        Assert.Throws<BlackJackCasinoAccountNotFoundException>((() => casino.Withdraw(2, 200)));
    }
}
