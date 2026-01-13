using System.Text.Json.Serialization;
using System;

namespace backend.models
{
    /**
     * @class User
     * @brief Representa un perfil único de usuario en ReadHub.
     * 
     * Los usuarios son anónimos: pueden comprar y vender libros simultáneamente sin distinción de roles.
     * Solo se almacenan los datos mínimos necesarios para la autenticación y las transacciones.
     */
    public class User
    {
        /**
         * @brief Identificador único del usuario en el sistema.
         */
        //[JsonPropertyName("_id")]
        public int Id { get; set; }

        /** 
         * @brief Nombre de usuario único dentro del sistema.
         */
        //[JsonPropertyName("_username")]
        public string Username { get; set; }

        /** 
         * @brief Correo electrónico del usuario.
         */
        //[JsonPropertyName("_email")]
        public string Email { get; set; }

        /**
         * @brief Dirección de la wallet de Bitcoin asociada al usuario.
         */
        //[JsonPropertyName("_walletAddress")]
        public string WalletAddress { get; set; }

        /**
         * @brief Contraseña privada del usuario (no accesible directamente).
         */
        public string Password { get; set; }

        /**
         * @brief Constructor básico para inicializar un usuario.
         * @param id Identificador único.
         * @param username Nombre de usuario.
         * @param email Correo electrónico.
         * @param password Contraseña.
         * @param walletAddress Dirección de la wallet de Bitcoin.
         */
        public User(int id, string username, string email, string password, string walletAddress)
        {
            Id = id;
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            WalletAddress = walletAddress ?? throw new ArgumentNullException(nameof(walletAddress));
        }

        /**
         * @brief Verifica si la contraseña ingresada coincide con la almacenada.
         * @param password Contraseña a verificar.
         * @return true si coincide, false en caso contrario.
         */
        public bool VerifyPassword(string password)
        {
            return Password == password;
        }

        /**
         * @brief Cambia la contraseña del usuario.
         * @param actualPassword Contraseña actual.
         * @param newPassword Nueva contraseña.
         * @throws UnauthorizedAccessException si la contraseña actual no coincide.
         */
        public void ChangePassword(string actualPassword, string newPassword)
        {
            if (!VerifyPassword(actualPassword))
                throw new UnauthorizedAccessException("La contraseña actual no coincide.");

            Password = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
        }
    }
}
