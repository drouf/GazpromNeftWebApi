using FluentValidation;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Requests
{
    public class PatchUserRequest : IRequest<User>
    {
        public long Id { get; set; }
    }

    public class PatchUserValidator : AbstractValidator<PatchUserRequest>
    {

        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;

        public PatchUserValidator(GNContext dbContext)
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();

            RuleFor(c => c.Id)
                .Must(c => _users.FirstOrDefault(u => u.Id == c) != null)
                .WithMessage("Пользователя не существует");
        }
    }
}
