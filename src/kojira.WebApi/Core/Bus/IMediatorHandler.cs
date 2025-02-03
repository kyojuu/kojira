using kojira.WebApi.Core.Commands;
using kojira.WebApi.Core.Events;

namespace kojira.WebApi.Core.Bus;

public interface IMediatorHandler
{
    Task SendCommand<T>(T command)
        where T : Command;

    Task RaiseEvent<T>(T @event)
        where T : Event;
}
