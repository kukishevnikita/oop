using System.Net.Mail;

namespace OOP_ICT.Fourth.Models;

public class PokerPlayer : CardsPlayer
{
    public PokerPlayer(int id, string username = "") : base(id, username) { }
    public bool InGame { get; set; } = true;
}