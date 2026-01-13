using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.services;

namespace backend.controllers
{
    /**
     * @class BooksController
     * @brief Controlador HTTP para gestionar operaciones relacionadas con libros.
     *
     * Expone endpoints REST para registrar y consultar libros dentro del sistema.
     * Se comunica con BookService, quien maneja la lógica de negocio.
     */
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        /// @brief Servicio encargado de la lógica de negocio de libros.
        private readonly BookService _service;

        /**
         * @brief Constructor del controlador.
         * @param service Instancia de BookService inyectada por el contenedor de dependencias.
         */
        public BooksController(BookService service)
        {
            _service = service;
        }

        /**
         * @brief Obtiene todos los libros registrados.
         * @return Lista de libros en formato JSON.
         */
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _service.GetBooks();
            return Ok(books);
        }

        /**
         * @brief Obtiene un libro por su ID.
         * @param id Identificador único del libro.
         * @return Libro encontrado o error 404 si no existe.
         */
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _service.GetBookById(id);
            if (book == null)
                return NotFound(new { error = "Libro no encontrado." });

            return Ok(book);
        }

        /**
         * @brief Registra un nuevo libro en el sistema.
         * @param book Objeto Book recibido desde el frontend.
         * @return Libro creado con código 201.
         */
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest(new { error = "El libro no puede ser nulo." });

            var added = _service.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = added.Id }, added);
        }

        /**
         * @brief Elimina un libro por su ID.
         * @param id Identificador del libro.
         * @return Mensaje de éxito o error 404 si no existe.
         */
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            bool deleted = _service.DeleteBook(id);

            if (!deleted)
                return NotFound(new { error = "Libro no encontrado." });

            return Ok(new { message = "Libro eliminado correctamente." });
        }
    }
}