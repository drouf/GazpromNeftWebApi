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
        private readonly IMapper _mapper;
        public GetUserHandler(GNContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            IQueryable<User> query = _dbContext.Set<User>().AsNoTracking();

            if (request.Id != null)
                query.Where(u => u.Id == request.Id);

            var result = await query.ToListAsync();

            return _mapper.Map<IEnumerable<User>,IEnumerable<UserDto>>(result);
        }
    }
}
