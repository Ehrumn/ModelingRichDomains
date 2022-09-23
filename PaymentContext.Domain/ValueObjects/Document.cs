using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, eDocumentType type)
        {
            Number = number;
            Type = type;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsTrue(Validate(), "Document.Number", "Documento inválido"));
        }

        public string Number{get; private set; }

        public eDocumentType Type { get; private set; }

        private bool Validate()
        {
            if (Type == eDocumentType.CNPJ && Number.Length == 14)
                return true;

            if (Type == eDocumentType.CNPJ && Number.Length == 11)
                return true;

            return false;
        }
    }
}