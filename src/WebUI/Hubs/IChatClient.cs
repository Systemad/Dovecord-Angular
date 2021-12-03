﻿using Domain.Entities;
using WebUI.Domain.Messages;
using WebUI.Domain.Users;

namespace WebUI.Hubs;

public interface IChatClient
{
    Task SendUserList(List<User> users);

    Task UserTyping(ActorAction action);

    Task MessageReceived(ChannelMessage message);

    Task DeleteMessageReceived(string id);

    Task AddToChannel(Guid channelId, Guid userId);
}