﻿using kojira.WebApi.Core.Bus;
using kojira.WebApi.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace kojira.WebApi.Controller;

[Route("api/[controller]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediatorHandler _mediator;

    protected ApiController(
        INotificationHandler<DomainNotification> notifications,
        IMediatorHandler mediator)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediator = mediator;
    }

    protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

    protected bool IsValidOperation()
    {
        return !_notifications.HasNotifications();
    }

    protected new IActionResult Response(object? result = null)
    {
        if (IsValidOperation())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }
        return BadRequest(new
        {
            success = false,
            errors = _notifications.GetNotifications().Select(n => n.Value)
        });
    }

    protected void NotifyModelStateErrors()
    {
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors)
        {
            var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            NotifyError(string.Empty, errorMessage);
        }
    }

    protected void NotifyError(string code, string message)
    {
        _mediator.RaiseEvent(new DomainNotification(code, message));
    }

    protected void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            NotifyError(error.Code, error.Description);
        }
    }
}
