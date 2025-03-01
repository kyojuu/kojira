﻿using MediatR;

namespace kojira.WebApi.Notifications;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private List<DomainNotification> _notifications;

    public DomainNotificationHandler()
    {
        _notifications = new List<DomainNotification>();
    }

    public Task Handle(DomainNotification message, CancellationToken cancellationToken)
    {
        _notifications.Add(message);

        return Task.CompletedTask;
    }

    public virtual List<DomainNotification> GetNotifications()
    {
        return _notifications;
    }

    public virtual bool HasNotifications()
    {
        return GetNotifications().Count > 0;
    }

    public void Disposed()
    {
        _notifications = new List<DomainNotification>();
    }
}

