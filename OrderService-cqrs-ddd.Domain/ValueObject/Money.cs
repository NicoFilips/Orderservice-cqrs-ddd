namespace OrderService_cqrs_ddd.Domain.ValueObject;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentException("Amount cannot be negative");
        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    public Money Add(Money other) => Currency != other.Currency
            ? throw new InvalidOperationException("Cannot add money with different currencies")
            : new Money(Amount + other.Amount, Currency);

    public Money Multiply(int factor) => new(Amount * factor, Currency);
}
