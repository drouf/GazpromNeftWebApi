﻿using FluentValidation;
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
                .WithMessage("Пользователя с id = {PropertyValue} не существует");

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
