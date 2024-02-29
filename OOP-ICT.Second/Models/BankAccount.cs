namespace OOP_ICT.Second.Models;

public partial class BankAccount
{
    public decimal Balance { get; set; }

    public BankAccount(decimal balance)
    {
        Balance = balance;
    }
}