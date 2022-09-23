namespace PaymentContext.Shared.Commands;

public interface ICommandResult
{
    ICommandResult Handle(Task command);
}
