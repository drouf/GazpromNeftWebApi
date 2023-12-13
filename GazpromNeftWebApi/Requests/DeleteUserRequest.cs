using FluentValidation;
using GazpromNeftDomain.Entities;
using GazpromNeftWebApi.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Requests
{
    public class DeleteUserRequest : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteUserValidator :AbstractValidator<DeleteUserRequest>
    {

        private readonly GNContext _dbContext;

        public DeleteUserValidator(GNContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(c => c.Id)
                .Must(c => _dbContext.Set<User>().AsNoTracking().FirstOrDefault(u => u.Id == c) != null)
                .WithMessage("Пользователя с id = {PropertyValue} не существует");
        }
    }
}
