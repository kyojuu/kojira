using FluentValidation.Results;
using kojira.WebApi.Core.Events;

namespace kojira.WebApi.Core.Commands;

public abstract class Command : Message
{
    public DateTime Timestamp { get; private set; }

    public ValidationResult? ValidationResult { get; set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }

    public abstract bool IsValid();
}
