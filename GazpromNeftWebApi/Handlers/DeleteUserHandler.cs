using AutoMapper;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Models;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, User?>
    {
        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;
        private readonly IMapper _mapper;
        public DeleteUserHandler(GNContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
            _mapper = mapper;
        }
        public async Task<User?> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _users.FirstAsync(u => u.Id == request.Id);
            _users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
