using AutoMapper;
using GazpromNeftDomain.Entities;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.DTO;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, IEnumerable<UserDto>>
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
        public async Task<IEnumerable<UserDto>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            IQueryable<User> query = _dbContext.Set<User>();

            if (request.Id != null)
                query.Where(u => u.Id == request.Id);

            var result = await query.ToListAsync();

            return _mapper.Map<IEnumerable<UserDto>>(result);
        }
    }
}
