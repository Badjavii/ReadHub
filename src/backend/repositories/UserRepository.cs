using MySql.Data.MySqlClient;
using backend.models;

namespace backend.repositories
{
    /**
     * @class UserRepository
     * @brief Repositorio encargado de gestionar usuarios en la base de datos MySQL.
     *
     * Esta clase implementa el patrón Repository, actuando como capa de acceso a datos.
     * Proporciona métodos para consultar, insertar y eliminar usuarios sin exponer
     * detalles de SQL al resto de la aplicación.
     */
    public class UserRepository
    {
        /// @brief Cadena de conexión a la base de datos MySQL.
        private readonly string _connectionString;

        /**
         * @brief Constructor del repositorio.
         * @param connectionString Cadena de conexión proporcionada por configuración.
         */
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /**
         * @brief Obtiene todos los usuarios registrados.
         *
         * Ejecuta una consulta SELECT sobre la tabla `users` y construye una lista
         * de objetos User a partir de los resultados.
         *
         * @return Lista completa de usuarios.
         */
        public List<User> GetAll()
        {
            var users = new List<User>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT id, username, email, password, walletAddress 
                             FROM users";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User(
                    reader.GetInt32("id"),
                    reader.GetString("username"),
                    reader.GetString("email"),
                    reader.GetString("password"),
                    reader.GetString("walletAddress")
                ));
            }

            return users;
        }

        /**
         * @brief Busca un usuario por su ID.
         * @param id Identificador único del usuario.
         * @return Instancia User si existe, nullptr si no se encontró.
         */
        public User? GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT id, username, email, password, walletAddress
                             FROM users WHERE id = @id";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new User(
                    reader.GetInt32("id"),
                    reader.GetString("username"),
                    reader.GetString("email"),
                    reader.GetString("password"),
                    reader.GetString("walletAddress")
                );
            }

            return null;
        }

        /**
         * @brief Busca un usuario por su correo electrónico.
         * @param email Correo del usuario.
         * @return Instancia User si existe, nullptr si no se encontró.
         */
        public User? GetByEmail(string email)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT id, username, email, password, walletAddress
                             FROM users WHERE email = @email";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@email", email);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new User(
                    reader.GetInt32("id"),
                    reader.GetString("username"),
                    reader.GetString("email"),
                    reader.GetString("password"),
                    reader.GetString("walletAddress")
                );
            }

            return null;
        }

        /**
         * @brief Inserta un nuevo usuario en la base de datos.
         *
         * @param user Objeto User con los datos a insertar.
         * @note El ID es autogenerado por MySQL.
         */
        public void Add(User user)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO users (username, email, password, walletAddress)
                             VALUES (@username, @email, @password, @walletAddress)";

            using var cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@walletAddress", user.WalletAddress);

            cmd.ExecuteNonQuery();
        }

        /**
         * @brief Elimina un usuario por su ID.
         * @param id Identificador del usuario.
         * @return true si se eliminó correctamente, false si no existía.
         */
        public bool Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM users WHERE id = @id";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}