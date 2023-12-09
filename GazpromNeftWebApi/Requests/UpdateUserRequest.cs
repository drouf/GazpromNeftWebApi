using FluentValidation;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Requests
{
    public class UpdateUserRequest : IRequest<User>
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {

        private readonly GNContext _dbContext;
        private readonly DbSet<User> _users;

        public UpdateUserValidator(GNContext dbContext)
        {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();

            RuleFor(c => c.Id)
                .Must(c => _users.FirstOrDefault(u => u.Id == c) != null)
                .WithMessage("Пользователя не существует");

            var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";

            RuleFor(c => c.FirstName)
            .Must(c => c.All(Char.IsLetter) && c.Length != 0).WithMessage(msg);

            RuleFor(c => c.LastName)
            .Must(c => c.All(Char.IsLetter) && c.Length != 0).WithMessage(msg);

            RuleFor(c => c.Phone)
            .Must(c => c.All(Char.IsDigit)).WithMessage(msg)
            .Length(11).WithMessage("Длина должна быть от {MinLength} до {MaxLength}. Текущая длина: {TotalLength}");

            RuleFor(c => c.Email)
            .NotNull().WithMessage(msg)
            .EmailAddress();
        }
    }
}
