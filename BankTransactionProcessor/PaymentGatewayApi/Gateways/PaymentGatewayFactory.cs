using PaymentGatewayApi.Config;

namespace PaymentGatewayApi;

public class PaymentGatewayFactory
{
    private readonly IConfiguration _configuration;

    public PaymentGatewayFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IPaymentGateway GetPaymentGateway(string gatewayName)
    {
        var gateways = _configuration.GetSection("PaymentGateways:Gateways").Get<List<PaymentGatewayConfig>>();
        var gatewayConfig = gateways.FirstOrDefault(g => g.Name == gatewayName);

        if (gatewayConfig == null)
            throw new Exception($"Payment gateway {gatewayName} is not configured.");

        // Decrypt sensitive data such as AuthToken before passing it to the gateway
        string decryptedAuthToken = EncryptionUtility.Decrypt(gatewayConfig.EncryptedAuthToken);
        
        return gatewayConfig.Name switch
        {
            "Stripe" => new StripePaymentGateway(gatewayConfig.ApiEndpoint, gatewayConfig.AuthToken),
            "PayPal" => new VisaPaymentGateway(gatewayConfig.ApiEndpoint, gatewayConfig.AuthToken),
            _ => throw new Exception($"Payment gateway {gatewayName} is not supported.")
        };
    }
}