using AutoMapper;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.DTO;
using GazpromNeftDomain.Entities;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, UserDto>
    {
        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;
        private readonly IMapper _mapper;
        public CreateUserHandler(GNContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            await _users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }
}
