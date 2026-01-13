using backend.models;
using backend.repositories;

namespace backend.services
{
    /**
     * @class UserService
     * @brief Servicio que encapsula la lógica de negocio relacionada con usuarios.
     *
     * Esta clase actúa como intermediaria entre los controladores y el repositorio.
     * Valida reglas de negocio como evitar duplicados y asegurar correos válidos.
     */
    public class UserService
    {
        /// @brief Repositorio encargado de la persistencia de usuarios.
        private readonly UserRepository _repository;

        /**
         * @brief Constructor del servicio.
         * @param repository Instancia de UserRepository inyectada desde configuración.
         */
        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        /**
         * @brief Registra un nuevo usuario en el sistema.
         *
         * Valida que el correo no esté registrado previamente y que cumpla
         * con las reglas de negocio definidas.
         *
         * @param user Instancia User con los datos del usuario.
         * @return El usuario recién registrado.
         * @exception Exception Si el correo ya está registrado o es inválido.
         */
        public User Register(User user)
        {
            ValidateEmail(user.Email);

            if (_repository.GetByEmail(user.Email) != null)
                throw new Exception("Ya existe un usuario con ese correo.");

            // MySQL generará el ID automáticamente
            _repository.Add(user);
            return user;
        }

        /**
         * @brief Obtiene un usuario por su correo electrónico.
         * @param email Correo del usuario.
         * @return Instancia User si existe, nullptr si no se encontró.
         */
        public User? GetUserByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        /**
         * @brief Obtiene un usuario por su ID.
         * @param id Identificador único del usuario.
         * @return Instancia User si existe, nullptr si no se encontró.
         */
        public User? GetUserById(int id)
        {
            return _repository.GetById(id);
        }

        /**
         * @brief Elimina un usuario del sistema.
         * @param id Identificador del usuario.
         * @return true si se eliminó correctamente, false si no existía.
         */
        public bool DeleteUser(int id)
        {
            return _repository.Delete(id);
        }

        /**
         * @brief Valida que el correo tenga un formato correcto.
         * @param email Correo a validar.
         * @exception Exception Si el correo no es válido.
         */
        private void ValidateEmail(string email)
        {
            if (!email.Contains("@") || !email.Contains("."))
                throw new Exception("Correo inválido.");
        }

        /**
         * @brief Valida las credenciales de un usuario.
         * @param email Correo del usuario.
         * @param password Contraseña del usuario.
         * @return true si las credenciales son válidas, false en caso contrario.
         */
        public bool ValidateCredentials(string email, string password)
        {
            var user = _repository.GetByEmail(email);

            if (user == null)
                return false;

            return user.VerifyPassword(password);
        }

    }
}