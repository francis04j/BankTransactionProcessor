namespace PaymentGatewayApi.Currency;

public interface ICurrencyConverter
{
    decimal Convert(decimal amount, string fromCurrency, string toCurrency); 
}