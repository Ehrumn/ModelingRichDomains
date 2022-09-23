using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands;

public class CreateCreditCardSubscriptionCommand: Notifiable<Notification>, ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }

    public string CardHolderName { get; set; }
    public string CardNumber { get; set; }
    public string LastTransactionNumber { get; set; }
    public string PaymentNumber { get; set; }
    public DateTime PaidDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public Decimal Total { get; set; }
    public Decimal TotalPaid { get; set; }
    public string Payer { get; set; }
    public string PayerDocument { get; set; }
    public eDocumentType PayerDocumentType { get; set; }
    public string PayerEmail { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<CreateBoletoSubscriptionCommand>().Requires()
            .IsGreaterThan(FirstName, 3, "FirstName", "O nome deve ter no mínimo 3 caracteres")
            .IsGreaterThan(LastName, 3, "LastName", "O sobrenome deve ter no mínimo 3 caracteres")
            .IsLowerThan(FirstName, 40, "FirstName", "Nomo deve conter no máximo 40 caracteres"));
    }
}
