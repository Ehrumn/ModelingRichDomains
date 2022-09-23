using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(string transactonCode, 
                             DateTime paidDate, 
                             DateTime expireDate, 
                             decimal total, 
                             decimal totalPaid, 
                             Name payer, 
                             Document document, 
                             Address address, 
                             Email email) 
                             : base(paidDate, 
                                    expireDate,
                                    total,
                                    totalPaid,
                                    payer,
                                    document,
                                    address,
                                    email)

        {
            TransactonCode = transactonCode;
        }

        public string TransactonCode { get; private set; }
    }
}