using MySql.Data.MySqlClient;
using backend.models;

namespace backend.repositories
{
    /**
     * @class TransactionRepository
     * @brief Repositorio encargado de gestionar transacciones en la base de datos MySQL.
     *
     * Esta clase implementa el patrón Repository, proporcionando métodos para consultar
     * e insertar transacciones sin exponer SQL al resto de la aplicación.
     */
    public class TransactionRepository
    {
        /// @brief Cadena de conexión a la base de datos MySQL.
        private readonly string _connectionString;

        /**
         * @brief Constructor del repositorio.
         * @param connectionString Cadena de conexión proporcionada por configuración.
         */
        public TransactionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /**
         * @brief Obtiene todas las transacciones registradas.
         *
         * Ejecuta una consulta SELECT sobre la tabla `transactions` y construye una lista
         * de objetos Transaction a partir de los resultados.
         *
         * @return Lista completa de transacciones.
         */
        public List<Transaction> GetAll()
        {
            var transactions = new List<Transaction>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT id, buyerUserId, sellerUserId, bookId, amountBTC, date
                             FROM transactions";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                transactions.Add(new Transaction(
                    reader.GetInt32("id"),
                    reader.GetInt32("buyerUserId"),
                    reader.GetInt32("sellerUserId"),
                    reader.GetInt32("bookId"),
                    reader.GetDecimal("amountBTC"),
                    reader.GetDateTime("date")
                ));
            }

            return transactions;
        }

        /**
         * @brief Obtiene una transacción por su ID.
         * @param id Identificador único de la transacción.
         * @return Instancia Transaction si existe, nullptr si no se encontró.
         */
        public Transaction? GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT id, buyerUserId, sellerUserId, bookId, amountBTC, date
                             FROM transactions WHERE id = @id";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Transaction(
                    reader.GetInt32("id"),
                    reader.GetInt32("buyerUserId"),
                    reader.GetInt32("sellerUserId"),
                    reader.GetInt32("bookId"),
                    reader.GetDecimal("amountBTC"),
                    reader.GetDateTime("date")
                );
            }

            return null;
        }

        /**
         * @brief Obtiene todas las transacciones realizadas por un comprador.
         * @param buyerId Identificador del usuario comprador.
         * @return Lista de transacciones asociadas al comprador.
         */
        public List<Transaction> GetByBuyer(int buyerId)
        {
            var transactions = new List<Transaction>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT id, buyerUserId, sellerUserId, bookId, amountBTC, date
                             FROM transactions WHERE buyerUserId = @buyerId";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@buyerId", buyerId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                transactions.Add(new Transaction(
                    reader.GetInt32("id"),
                    reader.GetInt32("buyerUserId"),
                    reader.GetInt32("sellerUserId"),
                    reader.GetInt32("bookId"),
                    reader.GetDecimal("amountBTC"),
                    reader.GetDateTime("date")
                ));
            }

            return transactions;
        }

        /**
         * @brief Obtiene todas las transacciones realizadas hacia un vendedor.
         * @param sellerId Identificador del usuario vendedor.
         * @return Lista de transacciones asociadas al vendedor.
         */
        public List<Transaction> GetBySeller(int sellerId)
        {
            var transactions = new List<Transaction>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT id, buyerUserId, sellerUserId, bookId, amountBTC, date
                             FROM transactions WHERE sellerUserId = @sellerId";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@sellerId", sellerId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                transactions.Add(new Transaction(
                    reader.GetInt32("id"),
                    reader.GetInt32("buyerUserId"),
                    reader.GetInt32("sellerUserId"),
                    reader.GetInt32("bookId"),
                    reader.GetDecimal("amountBTC"),
                    reader.GetDateTime("date")
                ));
            }

            return transactions;
        }

        /**
         * @brief Inserta una nueva transacción en la base de datos.
         *
         * @param transaction Objeto Transaction con los datos a insertar.
         * @note El ID es autogenerado por MySQL.
         */
        public void Add(Transaction transaction)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO transactions 
                            (buyerUserId, sellerUserId, bookId, amountBTC, date)
                             VALUES (@buyerUserId, @sellerUserId, @bookId, @amountBTC, @date)";

            using var cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@buyerUserId", transaction.BuyerUserId);
            cmd.Parameters.AddWithValue("@sellerUserId", transaction.SellerUserId);
            cmd.Parameters.AddWithValue("@bookId", transaction.BookId);
            cmd.Parameters.AddWithValue("@amountBTC", transaction.AmountBTC);
            cmd.Parameters.AddWithValue("@date", transaction.Date);

            cmd.ExecuteNonQuery();
        }

        /**
         * @brief Elimina una transacción por su ID.
         * @param id Identificador de la transacción.
         * @return true si se eliminó correctamente, false si no existía.
         */
        public bool Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM transactions WHERE id = @id";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}