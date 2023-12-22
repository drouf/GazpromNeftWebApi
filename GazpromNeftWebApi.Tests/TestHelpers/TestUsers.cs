using GazpromNeftDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GazpromNeftWebApi.Tests.StaticClasses
{
    internal static class TestUsers
    {
        public static IEnumerable<User> Get()
        {
            return new User[]
            {
                new() {Id = 1, FirstName="First", LastName = "First", Patronymic = "First", Phone = "89111111111", Email = "first@mail.ru"},
                new() {Id = 2, FirstName="Second", LastName = "Second", Patronymic = "Second", Phone = "89222222222", Email = "second@mail.ru"},
                new() {Id = 3, FirstName="Third", LastName = "Third", Patronymic = "Third", Phone = "89333333333", Email = "third@mail.ru"},
            };
        }
    }
}
