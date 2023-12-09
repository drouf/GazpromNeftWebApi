using AutoMapper;
using FluentValidation;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Models;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, User?>
    {
        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;
        private readonly CreateUserValidator _validator;
        private readonly IMapper _mapper;
        public CreateUserHandler(GNContext dbContext, IMapper mapper, CreateUserValidator validator) 
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<User?> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var user = _mapper.Map<User>(request);
            await _users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
