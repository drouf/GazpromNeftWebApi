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
        private readonly IMapper _mapper;
        public GetUserHandler(GNContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
            _mapper = mapper;
        }
        public async Task<IEnumerable<User>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
                return _users.ToList();
            return _users.Where(u => u.Id == request.Id);
        }
    }
}
