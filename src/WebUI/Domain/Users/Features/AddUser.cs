using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Dtos.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;
using WebUI.Domain.Channels;
using WebUI.Dtos.Channel;
using WebUI.Dtos.User;

namespace WebUI.Domain.Users.Features;

public static class AddUser
{
    public record AddUserCommand(UserManipulationDto ChannelToAdd) : IRequest<UserDto>;

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
            var user = _mapper.Map<User>(request.ChannelToAdd);
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            // TODO: fix
            return await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == user.Id, cancellationToken);
        }
    }
}