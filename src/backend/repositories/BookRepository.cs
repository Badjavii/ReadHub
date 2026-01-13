using MySql.Data.MySqlClient;
using backend.models;

namespace backend.repositories
{
    /**
     * @class BookRepository
     * @brief Repositorio encargado de gestionar la persistencia de libros en MySQL.
     *
     * Esta clase actúa como capa de acceso a datos (DAO/Repository Pattern).
     * Se conecta a la base de datos mediante MySqlConnection y ejecuta consultas SQL
     * para obtener, insertar o eliminar registros de la tabla `books`.
     *
     * No mantiene estado interno ni listas en memoria. Cada operación abre su propia
     * conexión y devuelve objetos Book completamente construidos.
     */
    public class BookRepository
    {
        /// @brief Cadena de conexión a la base de datos MySQL.
        private readonly string _connectionString;

        /**
         * @brief Constructor del repositorio.
         * @param connectionString Cadena de conexión proporcionada por configuración.
         */
        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /**
         * @brief Obtiene todos los libros almacenados en la base de datos.
         *
         * Ejecuta una consulta SELECT sobre la tabla `books` y construye una lista
         * de objetos Book a partir de los resultados.
         *
         * @return Lista de libros existentes en la base de datos.
         */
        public List<Book> GetAll()
        {
            var books = new List<Book>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT id, userId, title, description, author, publisher, year, 
                                    coverImageUrl, priceBTC, quantity 
                             FROM books";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                books.Add(new Book(
                    reader.GetInt32("id"),
                    reader.GetInt32("userId"),
                    reader.GetString("title"),
                    reader.GetString("description"),
                    reader.GetString("author"),
                    reader.GetString("publisher"),
                    reader.GetInt32("year"),
                    reader.GetString("coverImageUrl"),
                    reader.GetDecimal("priceBTC"),
                    reader.GetInt32("quantity")
                ));
            }

            return books;
        }

        /**
         * @brief Busca un libro por su identificador único.
         *
         * @param id Identificador del libro.
         * @return Instancia Book si existe, o nullptr si no se encontró.
         */
        public Book? GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT id, userId, title, description, author, publisher, year, 
                                    coverImageUrl, priceBTC, quantity 
                             FROM books WHERE id = @id";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Book(
                    reader.GetInt32("id"),
                    reader.GetInt32("userId"),
                    reader.GetString("title"),
                    reader.GetString("description"),
                    reader.GetString("author"),
                    reader.GetString("publisher"),
                    reader.GetInt32("year"),
                    reader.GetString("coverImageUrl"),
                    reader.GetDecimal("priceBTC"),
                    reader.GetInt32("quantity")
                );
            }

            return null;
        }

        /**
         * @brief Obtiene todos los libros publicados por un vendedor específico.
         *
         * @param sellerId ID del usuario vendedor.
         * @return Lista de libros asociados al vendedor.
         */
        public List<Book> GetBySeller(int sellerId)
        {
            var books = new List<Book>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT id, userId, title, description, author, publisher, year, 
                                    coverImageUrl, priceBTC, quantity 
                             FROM books WHERE userId = @sellerId";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@sellerId", sellerId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                books.Add(new Book(
                    reader.GetInt32("id"),
                    reader.GetInt32("userId"),
                    reader.GetString("title"),
                    reader.GetString("description"),
                    reader.GetString("author"),
                    reader.GetString("publisher"),
                    reader.GetInt32("year"),
                    reader.GetString("coverImageUrl"),
                    reader.GetDecimal("priceBTC"),
                    reader.GetInt32("quantity")
                ));
            }

            return books;
        }

        /**
         * @brief Inserta un nuevo libro en la base de datos.
         *
         * @param book Objeto Book con los datos a insertar.
         *
         * @note El ID es autogenerado por MySQL.
         */
        public void Add(Book book)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO books 
                            (userId, title, description, author, publisher, year, coverImageUrl, priceBTC, quantity)
                             VALUES (@userId, @title, @description, @author,
                                     @publisher, @year, @coverImageUrl, @priceBTC, @quantity)";

            using var cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@userId", book.UserId);
            cmd.Parameters.AddWithValue("@title", book.Title);
            cmd.Parameters.AddWithValue("@description", book.Description);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@publisher", book.Publisher);
            cmd.Parameters.AddWithValue("@year", book.Year);
            cmd.Parameters.AddWithValue("@coverImageUrl", book.CoverImageUrl);
            cmd.Parameters.AddWithValue("@priceBTC", book.PriceBTC);
            cmd.Parameters.AddWithValue("@quantity", book.Quantity);

            cmd.ExecuteNonQuery();
        }

        /**
         * @brief Elimina un libro de la base de datos.
         *
         * @param id Identificador del libro a eliminar.
         * @return true si se eliminó correctamente, false si no existía.
         */
        public bool Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM books WHERE id = @id";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}