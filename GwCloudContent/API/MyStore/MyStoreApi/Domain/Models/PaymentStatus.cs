namespace MyStoreApi.Domain.Models
{
    public enum PaymentStatus
    {
        Pending = 0,
        Processing = 1,
        Paid = 2,
        Failed = 3,
        Negated = 4
    }
}