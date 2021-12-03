using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Dtos.Channel;
using Infrastructure.Dtos.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;
using WebUI.Exceptions;

namespace WebUI.Domain.Messages.Features;

public static class GetMessage
{
    public record MessageQuery(Guid Id) : IRequest<ChannelMessageDto>;

    public class QueryHandler : IRequestHandler<MessageQuery, ChannelMessageDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ChannelMessageDto> Handle(MessageQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.ChannelMessages
                .ProjectTo<ChannelMessageDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result is null)
                throw new NotFoundException("Message", request.Id);

            return result;
        }
    }
}