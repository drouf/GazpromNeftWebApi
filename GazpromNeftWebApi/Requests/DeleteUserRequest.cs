using FluentValidation;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Requests
{
    public class DeleteUserRequest : IRequest<User>
    {
        public long Id { get; set; }
    }

    public class DeleteUserValidator :AbstractValidator<DeleteUserRequest>
    {

        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;

        public DeleteUserValidator(GNContext dbContext)
        {
            _dbContext = dbContext;
            _users = _dbContext.Set<User>();

            RuleFor(c => c.Id)
                .Must(c => _users.FirstOrDefault(u => u.Id == c) != null)
                .WithMessage("Пользователя с id = {PropertyValue} не существует");
        }
    }
}
