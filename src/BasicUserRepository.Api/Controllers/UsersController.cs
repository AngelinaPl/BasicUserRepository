using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
using Swashbuckle.AspNetCore.Annotations;

namespace BasicUserRepository.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Получить пользователя по ID.
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns>Пользователь</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(200, "Пользователь успешно найден", typeof(UserInfo))]
        [SwaggerResponse(404, "Пользователь не найден")]
        [SwaggerResponse(400, "Id пользователя некорректно")]
        public async Task<ActionResult<UserInfo>> GetUserById([FromRoute] int id,
            CancellationToken token)
        {
            if (id > 0)
            {
                var user = await _mediator.Send(new GetUserByIdRequest { Id = id }, token);
                if (user == null) return NotFound();
                return Ok(user);
            }

            return StatusCode(400, "User ID must be greater than zero.");
        }

        /// <summary>
        ///     Получить всех пользователей.
        /// </summary>
        /// <param name="request">Фильтр по полям пользователя</param>
        /// <returns>Список пользователей</returns>
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "Пользователи успешно найдены", typeof(IEnumerable<UserInfo>))]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetAllUsers([FromQuery] GetAllUsersRequest request,
            CancellationToken token)
        {
            var users = await _mediator.Send(request, token);
            return Ok(users);
        }

        /// <summary>
        ///     Добавить нового пользователя.
        /// </summary>
        /// <param name="request">Данные пользователя</param>
        /// <returns>Id созданного пользователя</returns>
        [HttpPost("AddUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponse(201, "Пользователь успешно создан", typeof(int))]
        [SwaggerResponse(400, "Некорректные данные пользователя")]
        public async Task<int> AddUser([FromBody] AddUserRequest request, CancellationToken token)
        {
            var userId = await _mediator.Send(request, token);
            return userId;
        }

        /// <summary>
        ///     Обновить данные пользователя.
        /// </summary>
        /// <param name="request">Обновленные данные пользователя</param>
        /// <returns>Результат операции</returns>
        [HttpPut("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);
            return result switch
            {
                UpdateUserResult.NotFound => NotFound(),
                UpdateUserResult.Error => StatusCode(500, "Internal Server Error"),
                UpdateUserResult.Updated => NoContent(),
                _ => NoContent()
            };
        }

        /// <summary>
        ///     Удалить пользователя.
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("DeleteUser")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteUser([FromBody] DeleteUserRequest request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);
            return result switch
            {
                DeleteUserResult.NotFound => NotFound(),
                DeleteUserResult.Error => StatusCode(500, "Internal Server Error"),
                DeleteUserResult.Deleted => NoContent(),
                _ => NoContent()
            };
        }
    }
}