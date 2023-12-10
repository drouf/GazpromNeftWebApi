using FluentValidation;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Requests
{
    public class GetUserRequest : IRequest<IEnumerable<User>>
    {
        public long? Id { get; set; }
    }
    public class GetUserValidator : AbstractValidator<GetUserRequest>
    {

        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;

        public GetUserValidator(GNContext dbContext)
        {
            _dbContext = dbContext;
            _users = _dbContext.Set<User>();

            RuleFor(c => c.Id)
                .Must(c => _users.FirstOrDefault(u => u.Id == c) != null || c == null)
                .WithMessage("Пользователя с id = {PropertyValue} не существует");
        }
    }
}
