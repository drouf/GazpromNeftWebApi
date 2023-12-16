using AutoMapper;
using GazpromNeftDomain.Entities;
using GazpromNeftWebApi.Controllers;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.DTO;
using GazpromNeftWebApi.Handlers;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazpromNeftWebApi.Tests.Handlers
{
    public class GetUserHandlerTests
    {
        private readonly IMapper _mapper;
        public GetUserHandlerTests() 
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AppMappingProfile));
            var serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }
        [Fact]
        public async Task GetTest()
        {
            // Arrange
            var mockDbContext = new Mock<GNContext>(new DbContextOptions<GNContext>());
            mockDbContext.Setup(c => c.Set<User>()).Returns(GetTestUsers().AsQueryable().BuildMockDbSet().Object);
            var handler = new GetUserHandler(mockDbContext.Object, _mapper);
            
            // Act
            var result = await handler.Handle(GetTestGetUserRequest(), new CancellationToken());

            // Assert
            Assert.IsAssignableFrom<IEnumerable<UserDto>>(result);
            var expectedUsers = _mapper.Map<IEnumerable<User>,IEnumerable<UserDto>>(GetTestUsers());
            Assert.Equal(expectedUsers.Count(), result.Count());
        }
        private IEnumerable<User> GetTestUsers()
        {
            return new User[]
            {
                new() {Id = 1, FirstName="First", LastName = "First", Patronymic = "First", Phone = "89111111111", Email = "first@mail.ru"},
                new() {Id = 2, FirstName="Second", LastName = "Second", Patronymic = "Second", Phone = "89222222222", Email = "second@mail.ru"},
                new() {Id = 3, FirstName="Third", LastName = "Third", Patronymic = "Third", Phone = "89333333333", Email = "third@mail.ru"},
            };
        }
        private GetUserRequest GetTestGetUserRequest()
        {
            return new GetUserRequest() { Id = null };
        }
    }
}
