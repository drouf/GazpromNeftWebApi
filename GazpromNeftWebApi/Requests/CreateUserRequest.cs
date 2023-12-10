using FluentValidation;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Requests
{
    public class CreateUserRequest : IRequest<UserDto>
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
            var msgEmpty = "Поле {PropertyName} не заполнено";
            var msgOnlyLetters = "Поле {PropertyName} должно состоять только из букв";
            var msgOnlyDigits = "Поле {PropertyName} должно состоять только из цифр";
            var msgFixedLength = "Длина должна быть {MaxLength}. Текущая длина: {TotalLength}";
            var msgIncorrectEmailFormat = "Некорректный формат email-адреса";

            RuleFor(c => c.FirstName)
                .Must(c => c.Length != 0).WithMessage(msgEmpty)
                .Must(c => c.All(Char.IsLetter)).WithMessage(msgOnlyLetters);

            RuleFor(c => c.LastName)
                .Must(c => c.Length != 0).WithMessage(msgEmpty)
                .Must(c => c.All(Char.IsLetter)).WithMessage(msgOnlyLetters);

            RuleFor(c => c.Phone)
                .Must(c => c.All(Char.IsDigit)).WithMessage(msgOnlyDigits)
                .Length(11).WithMessage(msgFixedLength);

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage(msgIncorrectEmailFormat);
        }
    }
}
