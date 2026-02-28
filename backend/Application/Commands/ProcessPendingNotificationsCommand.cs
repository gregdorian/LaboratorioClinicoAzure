namespace Lab.Api.Application.Commands
{
    public class ProcessPendingNotificationsCommand : Lab.Api.Application.CQRS.ICommand<int>
    {
        // optional max to process
        public int MaxItems { get; set; } = 50;
    }
}
