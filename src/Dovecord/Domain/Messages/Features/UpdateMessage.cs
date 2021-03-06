using AutoMapper;
using Dovecord.Databases;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Dovecord.Dtos.Message;
using Dovecord.Extensions.Services;

namespace Dovecord.Domain.Messages.Features;

public static class UpdateMessage
{
    public record UpdateMessageCommand(Guid Id, string NewMessageData) : IRequest<bool>;
    
    public class Query : IRequestHandler<UpdateMessageCommand, bool>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        
        public Query(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var messageToUpdate = await _context.ChannelMessages
                .Where(x => x.Id == request.Id)
                .AsTracking()
                .SingleOrDefaultAsync(cancellationToken);
            
            if (messageToUpdate is null)
                throw new NotFoundException("Message", request.Id);
            
            if (Guid.Parse(_currentUserService.UserId) != messageToUpdate.UserId)
                return false;
            
            _mapper.Map(request.NewMessageData, messageToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}