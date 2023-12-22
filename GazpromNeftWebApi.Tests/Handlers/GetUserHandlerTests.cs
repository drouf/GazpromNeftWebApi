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
            mockDbContext.Setup(c => c.Set<User>()).Returns(TestUsers.Get().AsQueryable().BuildMockDbSet().Object);
            var handler = new GetUserHandler(mockDbContext.Object, _mapper);
            
            // Act
            var result = await handler.Handle(GetTestGetUserRequest(), new CancellationToken());

            // Assert
            Assert.IsAssignableFrom<IEnumerable<UserDto>>(result);
            var expectedUsers = _mapper.Map<IEnumerable<User>,IEnumerable<UserDto>>(TestUsers.Get());
            Assert.Equal(expectedUsers.Count(), result.Count());
        }
        [Fact]
        public async Task GetSpecifiedTest()
        {
            // Arrange
            var mockDbContext = new Mock<GNContext>(new DbContextOptions<GNContext>());
            mockDbContext.Setup(c => c.Set<User>()).Returns(TestUsers.Get().AsQueryable().BuildMockDbSet().Object);
            var handler = new GetUserHandler(mockDbContext.Object, _mapper);

            // Act
            var result = await handler.Handle(GetTestGetUserRequest(1), new CancellationToken());

            // Assert
            Assert.IsAssignableFrom<IEnumerable<UserDto>>(result);
            Assert.Single(result);
            var expectedUser = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(TestUsers.Get()).First(x => x.Id == 1);
            Assert.Equivalent(expectedUser, result.First());
        }
        private GetUserRequest GetTestGetUserRequest(long? id = null)
        {
            return new GetUserRequest() { Id = id};
        }
    }
}
