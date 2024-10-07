namespace PaymentGatewayApi.Config;

public class PaymentGatewayConfig
{
    public string Name { get; set; }
    public string ApiEndpoint { get; set; }
    public string AuthToken { get; set; }
    public List<string> SupportedCurrencies { get; set; }
}