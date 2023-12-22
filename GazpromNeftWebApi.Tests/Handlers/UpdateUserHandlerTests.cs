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
    public class UpdateUserHandlerTests
    {
        private readonly IMapper _mapper;
        public UpdateUserHandlerTests() 
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AppMappingProfile));
            var serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }
        [Fact]
        public async Task UpdateTest()
        {
            // Arrange
            var mockDbContext = new Mock<GNContext>(new DbContextOptions<GNContext>());
            mockDbContext.Setup(c => c.Set<User>()).Returns(TestUsers.Get().AsQueryable().BuildMockDbSet().Object);
            var handler = new UpdateUserHandler(mockDbContext.Object, _mapper);
            
            // Act
            var result = await handler.Handle(GetTestUpdateUserRequest(), new CancellationToken());

            // Assert
            Assert.IsAssignableFrom<UserDto>(result);
            var expectedUser = _mapper.Map<User,UserDto>(_mapper.Map<UpdateUserRequest, User>(GetTestUpdateUserRequest()));
            Assert.Equivalent(expectedUser, result);
        }
        private UpdateUserRequest GetTestUpdateUserRequest()
        {
            return new UpdateUserRequest() { Id = 2, FirstName = "Test", LastName = "Test", Patronymic = "Test", Phone = "89111111119", Email = "test@mail.ru" };
        }
    }
}
