using System.Threading.Channels;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Channel = Domain.Entities.Channel;

namespace Application.Services;

public class ChannelService : IChannelService
{
    private DoveDbContext _context;
        
    public ChannelService(DoveDbContext context)
    {
        _context = context;
    }

    public async Task<List<Message>> GetMessagesByChannelIdAsync(Guid id)
    {
        return await _context.ChannelMessages.Where(a => a.ChannelId == id).ToListAsync();
    }

    public async Task<List<Channel>> GetChannels()
    {
        return await _context.TextChannels.ToListAsync();
    }

    public async Task<bool> DeleteChannelAsync(Guid id)
    {
        var channel = await GetChannelByIdAsync(id);
        _context.TextChannels.Remove(channel);
        var deleted = await _context.SaveChangesAsync();
        return deleted > 0;
    }

    public async Task<bool> CreateChannelAsync(Channel channel)
    {
        var count = await _context.TextChannels.CountAsync();
        if (count > 10)
            return false;
        
        // Add error code
        await _context.TextChannels.AddAsync(channel);
        var created = await _context.SaveChangesAsync();
        return created > 0;
    }

    public async Task<Channel> GetChannelByIdAsync(Guid id)
    {
        return await _context.TextChannels.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateChannelAsync(Channel channel)
    {
        var channelToUpdate = await _context.TextChannels.Where(x => x.Id == channel.Id)
            .AsTracking().SingleOrDefaultAsync();
        channelToUpdate.Name = channel.Name;
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }
}