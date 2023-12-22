using AutoMapper;
using GazpromNeftDomain.Entities;
using GazpromNeftWebApi.Controllers;
using GazpromNeftWebApi.Db;
using GazpromNeftWebApi.DTO;
using GazpromNeftWebApi.Handlers;
using GazpromNeftWebApi.Requests;
using GazpromNeftWebApi.Tests.StaticClasses;
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
    public class CreateUserHandlerTests
    {
        private readonly IMapper _mapper;
        public CreateUserHandlerTests() 
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AppMappingProfile));
            var serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }
        [Fact]
        public async Task CreateTest()
        {
            // Arrange
            var mockDbContext = new Mock<GNContext>(new DbContextOptions<GNContext>());
            mockDbContext.Setup(c => c.Set<User>()).Returns(TestUsers.Get().AsQueryable().BuildMockDbSet().Object);
            var handler = new CreateUserHandler(mockDbContext.Object, _mapper);
            
            // Act
            var result = await handler.Handle(GetTestCreateUserRequest(), new CancellationToken());

            // Assert
            Assert.IsAssignableFrom<UserDto>(result);
            var expectedUser = _mapper.Map<User,UserDto>(_mapper.Map<CreateUserRequest, User>(GetTestCreateUserRequest()));
            Assert.Equivalent(expectedUser, result);
        }
        private CreateUserRequest GetTestCreateUserRequest()
        {
            return new CreateUserRequest() { FirstName = "Test", LastName = "Test", Patronymic = "Test", Phone = "89111111119", Email = "test@mail.ru" };
        }
    }
}
