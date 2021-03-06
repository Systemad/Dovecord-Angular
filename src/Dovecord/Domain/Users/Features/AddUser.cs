using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Dtos.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Dovecord.Domain.Channels;
using Dovecord.Dtos.Channel;

namespace Dovecord.Domain.Users.Features;

public static class AddUser
{
    public record AddUserCommand(UserCreationDto UserToAdd) : IRequest<UserDto>;

    public class Handler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public Handler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.UserToAdd);
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var userr = await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == user.Id, cancellationToken);
            
            return await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);
        }
    }
}