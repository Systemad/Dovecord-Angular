using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Dtos.Channel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Channels.Features;

public static class AddChannel
{
    public record AddChannelCommand(ChannelManipulationDto ChannelToAdd) : IRequest<ChannelDto>;

    public class Handler : IRequestHandler<AddChannelCommand, ChannelDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public Handler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ChannelDto> Handle(AddChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = _mapper.Map<Channel>(request.ChannelToAdd);
            _context.Channels.Add(channel);
            await _context.SaveChangesAsync(cancellationToken);
            
            return await _context.Channels
                .ProjectTo<ChannelDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == channel.Id, cancellationToken);
        }
    }
}