using System.Threading;
using System.Threading.Tasks;
using BasicUserRepository.Api.Controllers;
using BasicUserRepository.Core.Enums;
using BasicUserRepository.Core.Models;
using BasicUserRepository.Core.User.v1.AddUser;
using BasicUserRepository.Core.User.v1.DeleteUser;
using BasicUserRepository.Core.User.v1.GetAllUsers;
using BasicUserRepository.Core.User.v1.GetUserById;
using BasicUserRepository.Core.User.v1.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class UsersControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UsersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetUserById_ReturnsOk_WhenUserExists()
    {
        // Arrange
        var userId = 1;
        var user = new UserInfo { Id = userId, FirstName = "John", LastName = "Doe" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(user);

        // Act
        var result = await _controller.GetUserById(userId, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<UserInfo>(okResult.Value);
        Assert.Equal(userId, returnedUser.Id);
    }

    [Fact]
    public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = 1;
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync((UserInfo)null);

        // Act
        var result = await _controller.GetUserById(userId, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetUserById_ReturnsBadRequest_WhenIdIsInvalid()
    {
        // Arrange
        var invalidUserId = -1;

        // Act
        var result = await _controller.GetUserById(invalidUserId, CancellationToken.None);

        // Assert
        var badRequestResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task GetAllUsers_ReturnsOk_WithListOfUsers()
    {
        // Arrange
        var users = new UserInfo[]
        {
            new UserInfo { Id = 1, FirstName = "John", LastName = "Doe" },
            new UserInfo { Id = 2, FirstName = "Jane", LastName = "Smith" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllUsersRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(users);

        // Act
        var result = await _controller.GetAllUsers(new GetAllUsersRequest(), CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsType<UserInfo[]>(okResult.Value);
        Assert.Equal(2, returnedUsers.Length);
    }

    [Fact]
    public async Task AddUser_ReturnsCreated_WhenUserIsAdded()
    {
        // Arrange
        var newUserId = 1;
        var addUserRequest = new AddUserRequest { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<AddUserRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(newUserId);

        // Act
        var result = await _controller.AddUser(addUserRequest, CancellationToken.None);

        // Assert
        Assert.Equal(newUserId, result);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNoContent_WhenUserIsUpdated()
    {
        // Arrange
        var updateUserRequest = new UpdateUserRequest { Id = 1, FirstName = "John", LastName = "Doe" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(UpdateUserResult.Updated);

        // Act
        var result = await _controller.UpdateUser(updateUserRequest, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNoContent_WhenUserIsDeleted()
    {
        // Arrange
        var deleteUserRequest = new DeleteUserRequest { Id = 1 };
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(DeleteUserResult.Deleted);

        // Act
        var result = await _controller.DeleteUser(deleteUserRequest, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
