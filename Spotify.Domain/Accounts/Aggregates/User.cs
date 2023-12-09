using Spotify.Domain.Accounts.ValueObjects;
using Spotify.Domain.Streaming.Aggregates;

namespace Spotify.Domain.Accounts.Aggregates;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public CPF Cpf { get; set; }
    public List<CreditCard> CreditCards { get; set; }
    public List<Playlist> Playlists { get; set; }
    public List<Subscription> Subscriptions { get; set; }

    public User()
    {
        CreditCards = new List<CreditCard>();
        Playlists = new List<Playlist>();
        Subscriptions = new List<Subscription>();
    }

    public void Create(string nome, string email, string cpf, CreditCard card, Plan plan)
    {
        Name = nome;
        Email = email;
        Cpf = new CPF(cpf);

        AddSubscription(plan, card);

        AddCreditCard(card);
    }

    private void AddSubscription(Plan plan, CreditCard card)
    {
        card.CreateTransaction(plan.Name, plan.Value);

        if(Subscriptions.Count > 0 && Subscriptions.Any(s => s.Active)) 
        { 
            var activePlan = Subscriptions.FirstOrDefault(s => s.Active);
            activePlan.Active = false;
        }

        Subscriptions.Add(new Subscription
        {
            Active = true,
            Id = Guid.NewGuid(),
            Plan = plan,
            ValidDateAt = DateTime.Now,
        });
    }

    private void AddCreditCard(CreditCard card)
    {
        CreditCards.Add(card);
    }
}