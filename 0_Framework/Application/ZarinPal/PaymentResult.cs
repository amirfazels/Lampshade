namespace _0_Framework.Application.ZarinPal
{
    public enum PaymentStatus
    {
        Failed,
        Succeeded,
        OfflinePayment
    }

    public class PaymentResult
    {
        public PaymentStatus Status { get; set; }
        public string Message { get; set; }
        public string IssueTrackingNo { get; set; }

        public PaymentResult Succeeded(string message, string issueTrackingNo)
        {
            Status = PaymentStatus.Succeeded;
            Message = message;
            IssueTrackingNo = issueTrackingNo;
            return this;
        }

        public PaymentResult Failed(string message)
        {
            Status = PaymentStatus.Failed;
            Message = message;
            return this;
        }

        public PaymentResult OfflinePayment(string message)
        {
            Status = PaymentStatus.OfflinePayment;
            Message = message;
            return this;
        }
    }
}