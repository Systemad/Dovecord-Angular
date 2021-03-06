using AutoBogus;
using Dovecord.Databases;
using Dovecord.Domain.Channels;

namespace Dovecord.Seeders;

public static class ChannelSeeder
{
    public static void SeedSampleChannels(DoveDbContext context)
    {
        if (!context.Channels.Any())
        {
            context.Channels.Add(new AutoFaker<Channel>().Ignore(x => x.ChannelMessages));
            context.Channels.Add(new AutoFaker<Channel>().Ignore(x => x.ChannelMessages));
            context.Channels.Add(new AutoFaker<Channel>().Ignore(x => x.ChannelMessages));
            context.SaveChanges();
        }
    }
}