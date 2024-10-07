namespace PaymentGatewayApi;

public interface IPaymentGateway
{
    Task<bool> AuthorizePayment(decimal amount, string currency, UserConfig userConfig);
    bool CapturePayment(decimal amount, string currency, UserConfig userConfig);
    bool RefundPayment(string paymentId);
    bool VoidPayment(string paymentId);
}