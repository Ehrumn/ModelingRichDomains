using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable<Notification>, 
                                   IHandler<CreateBoletoSubscriptionCommand>,
                                   IHandler<CreatePayPalSubscriptionCommand>,
                                   IHandler<CreateCreditCardSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
    {
        //Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {

            return new CommandResult(false, "Não foi possível realizar a inscrição");
        }

        //verificar se documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está em uso");


        //verificar se email já está cadastrado
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já está em uso");

        //gerar os VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, eDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, 
                                  command.Number, 
                                  command.Complement, 
                                  command.Neighborhood, 
                                  command.City, 
                                  command.State, 
                                  command.Country, 
                                  command.ZipCode);

        
        //Gerar as entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));

        var payment = new BoletoPayment(command.BarCode,
                                        command.BoletoNumber,
                                        command.PaidDate,
                                        command.ExpireDate,
                                        command.Total,
                                        command.TotalPaid,
                                        new Name(command.Payer, ""),
                                        new Document(command.PayerDocument, command.PayerDocumentType),
                                        address,
                                        email);

        //Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        //Agrupar as Validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        //Salvar as informações
        _repository.CreateSubscription(student);

        //Enviar E-mail de boas vindas
        _emailService.SendEmail(student.Name.ToString(), student.Email.Address, "Bem vindo ao Rafael.IO", "Assinatura realizada com sucessl");

        //retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso");
    }

    public ICommandResult Handler(CreatePayPalSubscriptionCommand command)
    {
        //verificar se documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está em uso");


        //verificar se email já está cadastrado
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já está em uso");

        //gerar os VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, eDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, 
                                  command.Number, 
                                  command.Complement, 
                                  command.Neighborhood, 
                                  command.City, 
                                  command.State, 
                                  command.Country, 
                                  command.ZipCode);

        
        //Gerar as entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));

        var payment = new PayPalPayment(command.TransactonCode,
                                        command.PaidDate,
                                        command.ExpireDate,
                                        command.Total,
                                        command.TotalPaid,
                                        new Name(command.Payer, ""),
                                        new Document(command.PayerDocument, command.PayerDocumentType),
                                        address,
                                        email);

        //Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        //Agrupar as Validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        //Salvar as informações
        _repository.CreateSubscription(student);

        //Enviar E-mail de boas vindas
        _emailService.SendEmail(student.Name.ToString(), student.Email.Address, "Bem vindo ao Rafael.IO", "Assinatura realizada com sucessl");

        //retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso");
    }

    public ICommandResult Handler(CreateCreditCardSubscriptionCommand command)
    {
        //verificar se documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está em uso");


        //verificar se email já está cadastrado
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já está em uso");

        //gerar os VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, eDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, 
                                  command.Number, 
                                  command.Complement, 
                                  command.Neighborhood, 
                                  command.City, 
                                  command.State, 
                                  command.Country, 
                                  command.ZipCode);

        
        //Gerar as entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));

        var payment = new CreditCardPayment(command.CardHolderName,
                                            command.CardNumber,
                                            command.LastTransactionNumber,
                                            command.PaidDate,
                                            command.ExpireDate,
                                            command.Total,
                                            command.TotalPaid,
                                            new Name(command.Payer, ""),
                                            new Document(command.PayerDocument, command.PayerDocumentType),
                                            address,
                                            email);

        //Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        //Agrupar as Validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        //Salvar as informações
        _repository.CreateSubscription(student);

        //Enviar E-mail de boas vindas
        _emailService.SendEmail(student.Name.ToString(), student.Email.Address, "Bem vindo ao Rafael.IO", "Assinatura realizada com sucessl");

        //retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso");
    }
}
