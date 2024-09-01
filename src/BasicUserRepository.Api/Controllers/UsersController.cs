using BasicUserRepository.Core.Models;
using BasicUserRepository.Core.User.v1.AddUser;
using BasicUserRepository.Core.User.v1.GetAllUsers;
using BasicUserRepository.Core.User.v1.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasicUserRepository.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Получить пользователя по ID.
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns>Пользователь</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(200, "Пользователь успешно найден", typeof(UserInfo))]
        [SwaggerResponse(404, "Пользователь не найден")]
        public async Task<ActionResult<UserInfo>> GetUserById([FromHeader] GetUserByIdRequest request, CancellationToken token)
        {
            var user = await _mediator.Send(request, token);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <returns>Список пользователей</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "Пользователи успешно найдены", typeof(IEnumerable<UserInfo>))]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetAllUsers(CancellationToken token)
        {
            var users = await _mediator.Send(new GetAllUsersRequest(), token);
            return Ok(users);
        }

        /// <summary>
        /// Добавить нового пользователя.
        /// </summary>
        /// <param name="request">Данные пользователя</param>
        /// <returns>Id созданного пользователя</returns>
        [HttpPost]
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
        /// Обновить данные пользователя.
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <param name="user">Обновленные данные пользователя</param>
        /// <returns>Результат операции</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserEntity user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userRepository.UpdateUserAsync(user);
            return NoContent();
        }

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
