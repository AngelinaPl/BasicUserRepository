using BasicUserRepository.Core.Interfaces;
using BasicUserRepository.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicUserRepository.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Получить пользователя по ID.
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns>Пользователь</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserEntity), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserEntity>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
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
        [ProducesResponseType(typeof(IEnumerable<UserEntity>), 200)]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Добавить нового пользователя.
        /// </summary>
        /// <param name="user">Данные пользователя</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserEntity), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddUser([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userRepository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
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
