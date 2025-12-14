using AutoFixture;
using Core.Abstractions;
using Core.Entities;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Core.UnitTests
{
    public class UserServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUserStorage> _storageMock;
        private readonly Mock<IPasswordHashService> _passwordHashMock;
        private readonly UserService _service;


        public UserServiceTests()
        {
            _fixture = new Fixture();
            _storageMock = new Mock<IUserStorage>();
            _passwordHashMock = new Mock<IPasswordHashService>();

            // Настраиваем DI-контейнер
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddSingleton<IUserStorage>(_storageMock.Object);
            services.AddSingleton<IPasswordHashService>(_passwordHashMock.Object);
            services.AddSingleton<UserService>();

            // Создаем сервис-локатор
            var sp = services.BuildServiceProvider();

            // Получаем собранный объект UserService
            _service = sp.GetRequiredService<UserService>();
        }

        [Fact]
        public async Task GetAllUsersTest()
        {
            var expectedUsers = _fixture.CreateMany<User>().ToList();
            _storageMock.Setup(x => x.GetAll()).ReturnsAsync(expectedUsers);

            var actualUsers = await _service.GetAllUsers();

            Assert.True(actualUsers.IsSuccess);
            Assert.NotNull(actualUsers.Result);
            Assert.Equal(expectedUsers, actualUsers.Result);
        }
    }
}
