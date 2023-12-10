using Spotify.Core.Exceptions;
using Spotify.Domain.Accounts.Exceptions;
using Spotify.Domain.Transactions.Aggregates;

namespace Spotify.Domain.Accounts.Aggregates;

public class CreditCard
{
    private const int TRANSACTION_TIME_INTERVAL = -5;
    private const int TRANSACTION_DESCRIPTION_REPEAT = 1;

    public Guid Id { get; set; }
    public string Number { get; set; }
    public string Username { get; set; }
    public double AvailableLimit { get; set; }
    public DateTime ValidDate { get; set; }
    public int SecureCode { get; set; }
    public bool ActiveCard { get; set; }
    public List<Transaction> Transactions { get; set; }

    public CreditCard()
    {
        Transactions = new List<Transaction>();
    }

    public void CreateCard(string number, double availableLimit, bool activeCard, int secureCode, string username)
    {
        Id = Guid.NewGuid();
        Number = number;
        AvailableLimit = availableLimit;
        ActiveCard = activeCard;
        SecureCode = secureCode;
        Username = username;
        Transactions = new List<Transaction>();
    }

    public void CreateTransaction(string planName, double planValue)
    {
        var errors = new CardException();

        IsActive(errors);

        var transaction = new Transaction();
        transaction.Create(Username, planValue, planName);

        VerifyLimit(errors, transaction);

        ValidateTransaction(errors, transaction);

        errors.ValidateAndThrow();

        transaction.Id = Guid.NewGuid();

        AvailableLimit -= transaction.Amount;

        Transactions.Add(transaction);
    }

    private void ValidateTransaction(CardException errors, Transaction transaction)
    {
        var lastTransactions = Transactions.Where(t => t.PurchasedDate >= DateTime.Now.AddMinutes(TRANSACTION_TIME_INTERVAL));

        if (lastTransactions?.Count() >= 4)
        {
            errors.AddError(new BusinessValidation
            {
                ErrorMessage = "Too many transactions have been made, please wait a moment and try again",
                ErrorName = nameof(CardException),
            });
        }

        if (lastTransactions?.Where(t => t.Description.ToLower() == transaction.Description.ToLower()
                                    && t.Amount == transaction.Amount).Count() == TRANSACTION_DESCRIPTION_REPEAT)
        {
            errors.AddError(new BusinessValidation
            {
                ErrorMessage = "Duplicate transaction",
                ErrorName = nameof(CardException),
            });
        }
    }

    private void VerifyLimit(CardException errors, Transaction transaction)
    {
        if (transaction.Amount > AvailableLimit)
        {
            errors.AddError(new BusinessValidation
            {
                ErrorMessage = "Not limit available",
                ErrorName = nameof(CardException),
            });
        }
    }

    private void IsActive(CardException errors)
    {
        if (!ActiveCard)
        {
            errors.AddError(new BusinessValidation
            {
                ErrorMessage = "Card is not active",
                ErrorName = nameof(CardException)
            });
        }
    }
}