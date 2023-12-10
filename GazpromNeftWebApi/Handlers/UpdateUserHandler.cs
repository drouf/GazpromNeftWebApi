using AutoMapper;
using GazpromNeftDomain.Entities;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.DTO;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UserDto>
    {
        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;
        private readonly IMapper _mapper;
        public UpdateUserHandler(GNContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            _users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }
}
