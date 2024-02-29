namespace OOP_ICT.Second.Models;

public class BlackJackCasinoAccount
{
    public decimal Balance { get; set; }

    public BlackJackCasinoAccount(decimal balance)
    {
        Balance = balance;
    }
}