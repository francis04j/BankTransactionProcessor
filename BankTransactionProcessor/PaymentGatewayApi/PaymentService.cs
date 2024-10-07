using PaymentGatewayApi.Currency;

namespace PaymentGatewayApi;

public class PaymentService
{
    private readonly PaymentGatewayFactory _gatewayFactory;
    private readonly ICurrencyConverter _currencyConverter;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(PaymentGatewayFactory gatewayFactory, ICurrencyConverter currencyConverter, ILogger<PaymentService> logger)
    {
        _gatewayFactory = gatewayFactory;
        _currencyConverter = currencyConverter;
        _logger = logger;
    }

    public async Task<bool> ProcessPayment(string gatewayName, decimal amount, string currency)
    {
        try
        {
            var gateway = _gatewayFactory.GetPaymentGateway(gatewayName);
            
            // Convert currency if necessary
            if (currency != "USD")
            {
                amount = _currencyConverter.Convert(amount, currency, "USD");
                _logger.LogInformation($"Converted amount to USD: {amount}");
            }

            // Authorize payment
            return await gateway.AuthorizePayment(amount, "USD", new UserConfig());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Payment processing failed: {ex.Message}");
            return false;
        }
    }
}
