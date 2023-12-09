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
        private readonly GetUserValidator _validator;
        private readonly IMapper _mapper;
        public GetUserHandler(GNContext dbContext, IMapper mapper, GetUserValidator validator)
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<IEnumerable<User>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                return [];
            }
            try
            {
                if(request.Id == null)
                {
                    return _users.ToList();
                }
                return [await _users.FirstAsync(u => u.Id == request.Id)];
            }
            catch (DbUpdateException e)
            {
                return [];
            }
        }
    }
}
