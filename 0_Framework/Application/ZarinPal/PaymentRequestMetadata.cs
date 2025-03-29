namespace _0_Framework.Application.ZarinPal;

public class PaymentRequestMetadata
{
    public PaymentRequestMetadata()
    {
        mobile = "";
        email = "";
        order_id = "";
    }

    public string? mobile { get; private set; }
    public string? email { get; private set; }
    public string? order_id { get; private set; }

    public void SetMobile(string mobile)
    {
        this.mobile = mobile;
    }
    public void SetEmail(string email)
    {
        this.email = email;
    }
    public void SetOrderId(long order_id)
    {
        this.order_id = order_id.ToString();
    }
}