using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Commands.Notifications;

public class MemberCreatedNotification(Member member) : INotification
{
    public Member Member { get; init; } = member;
}