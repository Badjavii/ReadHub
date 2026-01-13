using backend.models;
using backend.repositories;

namespace backend.services
{
    /**
     * @class BookService
     * @brief Servicio que encapsula la lógica de negocio relacionada con libros.
     *
     * Esta clase actúa como intermediaria entre los controladores y el repositorio.
     * Se encarga de coordinar operaciones de negocio y delegar el acceso a datos
     * al BookRepository.
     */
    public class BookService
    {
        /// @brief Repositorio encargado de la persistencia de libros.
        private readonly BookRepository _repository;

        /**
         * @brief Constructor del servicio.
         * @param repository Instancia de BookRepository inyectada desde configuración.
         */
        public BookService(BookRepository repository)
        {
            _repository = repository;
        }

        /**
         * @brief Obtiene todos los libros registrados.
         * @return Lista completa de libros.
         */
        public List<Book> GetBooks()
        {
            return _repository.GetAll();
        }

        /**
         * @brief Busca un libro por su ID.
         * @param id Identificador único del libro.
         * @return Instancia Book si existe, nullptr si no se encontró.
         */
        public Book? GetBookById(int id)
        {
            return _repository.GetById(id);
        }

        /**
         * @brief Obtiene todos los libros publicados por un vendedor.
         * @param sellerId ID del usuario vendedor.
         * @return Lista de libros asociados al vendedor.
         */
        public List<Book> GetBooksBySeller(int sellerId)
        {
            return _repository.GetBySeller(sellerId);
        }

        /**
         * @brief Agrega un nuevo libro al sistema.
         * @param book Objeto Book con los datos a registrar.
         * @return El libro recién agregado.
         */
        public Book AddBook(Book book)
        {
            _repository.Add(book);
            return book;
        }

        /**
         * @brief Elimina un libro por su ID.
         * @param id Identificador del libro.
         * @return true si se eliminó correctamente, false si no existía.
         */
        public bool DeleteBook(int id)
        {
            return _repository.Delete(id);
        }
    }
}