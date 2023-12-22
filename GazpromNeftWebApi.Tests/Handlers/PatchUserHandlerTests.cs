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
    public class PatchUserHandlerTests
    {
        private readonly IMapper _mapper;
        public PatchUserHandlerTests() 
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AppMappingProfile));
            var serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }
        [Fact]
        public async Task PatchTest()
        {
            // Arrange
            var mockDbContext = new Mock<GNContext>(new DbContextOptions<GNContext>());
            mockDbContext.Setup(c => c.Set<User>()).Returns(TestUsers.Get().AsQueryable().BuildMockDbSet().Object);
            var handler = new PatchUserHandler(mockDbContext.Object, _mapper);
            
            // Act
            var result = await handler.Handle(GetTestPatchUserRequest(), new CancellationToken());

            // Assert
            Assert.IsAssignableFrom<UserDto>(result);
            var patchRequest = GetTestPatchUserRequest();
            var expectedUser = _mapper.Map<User, UserDto>(TestUsers.Get().First(u => u.Id == patchRequest.Id));
            expectedUser.FirstName = patchRequest.FirstName;
            Assert.Equivalent(expectedUser, result);
        }
        private PatchUserRequest GetTestPatchUserRequest()
        {
            return new PatchUserRequest() { Id = 2, FirstName = "TestPatch" };
        }
    }
}
