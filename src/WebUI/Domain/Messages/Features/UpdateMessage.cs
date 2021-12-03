using AutoMapper;
using Infrastructure.Dtos.Channel;
using Infrastructure.Dtos.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;
using WebUI.Exceptions;

namespace WebUI.Domain.Messages.Features;

public static class UpdateMessage
{
    public record UpdateMessageCommand(Guid Id, MessageManipulationDto NewMessageData) : IRequest<bool>;
    
    public class Query : IRequestHandler<UpdateMessageCommand, bool>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        
        public Query(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var messageToUpdate = await _context.ChannelMessages
                .Where(x => x.Id == request.Id)
                .AsTracking()
                .SingleOrDefaultAsync(cancellationToken);
            
            if (messageToUpdate is null)
                throw new NotFoundException("Message", request.Id);
            
            _mapper.Map(request.NewMessageData, messageToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}