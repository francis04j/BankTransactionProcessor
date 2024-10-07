namespace PaymentGatewayApi;

public class VisaPaymentGateway : IPaymentGateway
{
    private readonly string apiEndpoint;
    private readonly string authToken;

    public VisaPaymentGateway(string apiEndpoint, string authToken)
    {
        this.apiEndpoint = apiEndpoint;
        this.authToken = authToken;
    }

    public async Task<bool> AuthorizePayment(decimal amount, string currency, UserConfig userConfig)
    {
        Console.WriteLine($"Stripe: Authorizing {amount} {currency} for user {userConfig.Id}");
        // Simulate API call
        string tokenizedCardNumber = await TokenizeCard(userConfig.CardNumber);
        return true;
    }

    public bool CapturePayment(decimal amount, string currency, UserConfig userConfig)
    {
        Console.WriteLine($"Stripe: Capturing payment for {amount} {currency} for user {userConfig.Id}");
        return true;
    }

    public bool RefundPayment(string paymentId)
    {
        Console.WriteLine($"Stripe: Refunding  transaction {paymentId}");
        return true;
    }

    public bool VoidPayment(string transactionId)
    {
        Console.WriteLine($"Stripe: Voiding payment for transaction {transactionId}");
        return true;
    }
}
