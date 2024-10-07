namespace PaymentGatewayApi.Currency;

public class CurrencyConverter : ICurrencyConverter
{
    public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
    {
        // Example mock conversion rates
        var rates = new Dictionary<(string, string), decimal>
        {
            { ("USD", "EUR"), 0.85m },
            { ("EUR", "USD"), 1.18m }
        };

        return rates.ContainsKey((fromCurrency, toCurrency))
            ? amount * rates[(fromCurrency, toCurrency)]
            : amount;
    }
}