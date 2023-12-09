using FluentValidation;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Requests
{
    public class CreateUserRequest : IRequest<User>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {   
        public CreateUserValidator() 
        {
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
