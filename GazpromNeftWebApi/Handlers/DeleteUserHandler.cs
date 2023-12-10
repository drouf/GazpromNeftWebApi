using AutoMapper;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Models;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, User>
    {
        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;
        public DeleteUserHandler(GNContext dbContext) 
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
        }
        public async Task<User> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _users.FirstAsync(u => u.Id == request.Id);
            _users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
