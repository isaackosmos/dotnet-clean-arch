using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArch.Application.Members.Commands.Notifications;

public class MemberCreatedEmailHandler(ILogger<MemberCreatedEmailHandler> logger)
    : NotificationHandler<MemberCreatedNotification>
{
    protected override void Handle(MemberCreatedNotification notification)
    {
        logger.LogInformation($"Member created email notification sent for: {notification.Member.FirstName}");
    }
}