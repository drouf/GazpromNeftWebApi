using AutoMapper;
using GazpromNeftDomain.Entities;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest>
    {
        private readonly GNContext _dbContext;
        public DeleteUserHandler(GNContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Set<User>().FirstAsync(u => u.Id == request.Id);
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
