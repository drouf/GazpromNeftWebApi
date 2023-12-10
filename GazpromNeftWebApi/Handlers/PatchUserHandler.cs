using AutoMapper;
using GazpromNeftDomain.Entities;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.DTO;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Handlers
{
    public class PatchUserHandler : IRequestHandler<PatchUserRequest, UserDto>
    {
        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;
        private readonly IMapper _mapper;
        public PatchUserHandler(GNContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(PatchUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _users.FirstAsync(c => c.Id == request.Id, cancellationToken);
            var requestUser = _mapper.Map<User>(request);
            var properties = requestUser.GetType().GetProperties();
            foreach(var property in properties)
            {
                var propertyValue = property.GetValue(requestUser);
                if (propertyValue != null)
                {
                    property.SetValue(user, propertyValue);
                }
            }
            _users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }
}
