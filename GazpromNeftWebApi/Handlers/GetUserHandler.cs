using AutoMapper;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Models;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, IEnumerable<User>>
    {
        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;
        public GetUserHandler(GNContext dbContext)
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
        }
        public async Task<IEnumerable<User>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
                return await _users.ToListAsync();
            return _users.Where(u => u.Id == request.Id);
        }
    }
}
