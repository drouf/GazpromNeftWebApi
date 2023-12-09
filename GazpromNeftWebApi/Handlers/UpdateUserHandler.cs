using AutoMapper;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Models;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, User?>
    {
        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;
        private readonly UpdateUserValidator _validator;
        private readonly IMapper _mapper;
        public UpdateUserHandler(GNContext dbContext, IMapper mapper, UpdateUserValidator validator) 
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<User?> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                return null;
            }
            var user = _mapper.Map<User>(request);
            try
            {
                _users.Update(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            catch(DbUpdateException e)
            {
                return null;
            }
        }
    }
}
