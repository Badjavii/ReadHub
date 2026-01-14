using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.dto;
using backend.services;

namespace backend.controllers
{
    /**
     * @class UsersController
     * @brief Controlador HTTP para gestionar operaciones relacionadas con usuarios.
     *
     * Expone endpoints REST para registrar y consultar perfiles de usuario.
     */
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        /// @brief Servicio encargado de la lógica de negocio de usuarios.
        private readonly UserService _service;

        /**
         * @brief Constructor del controlador.
         * @param service Instancia de UserService inyectada por el contenedor de dependencias.
         */
        public UsersController(UserService service)
        {
            _service = service;
        }

        /**
         * @brief Registra un nuevo usuario en el sistema.
         * @param user Objeto User recibido desde el frontend.
         * @return Usuario creado con código 201 o error si ya existe.
         */
        [HttpPost]
        public IActionResult RegisterUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest(new { error = "El usuario no puede ser nulo." });

            try
            {
                var created = _service.Register(user);
                return CreatedAtAction(nameof(GetUserById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /**
         * @brief Obtiene un usuario por su ID.
         * @param id Identificador único del usuario.
         * @return Usuario encontrado o error 404 si no existe.
         */
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _service.GetUserById(id);

            if (user == null)
                return NotFound(new { error = "Usuario no encontrado." });

            return Ok(user);
        }

        /**
         * @brief Obtiene un usuario por su correo electrónico.
         * @param email Correo del usuario.
         * @return Usuario encontrado o error 404 si no existe.
         */
        [HttpGet("by-email")]
        public IActionResult GetUserByEmail([FromQuery] string email)
                        {
            var user = _service.GetUserByEmail(email);

            if (user == null)
                return NotFound(new { error = "Usuario no encontrado." });

            return Ok(user);
        }

        /**
         * @brief Elimina un usuario del sistema.
         * @param id Identificador del usuario.
         * @return Mensaje de éxito o error 404 si no existe.
         */
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            bool deleted = _service.DeleteUser(id);

            if (!deleted)
                return NotFound(new { error = "Usuario no encontrado." });

            return Ok(new { message = "Usuario eliminado correctamente." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            bool isValid = _service.ValidateCredentials(request.Email, request.Password);

            return Ok(new { success = isValid });
        }

    }
}
