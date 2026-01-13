using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.services;

namespace backend.controllers
{
    /**
     * @class TransactionsController
     * @brief Controlador HTTP para gestionar operaciones relacionadas con transacciones.
     *
     * Expone endpoints REST para registrar y consultar transacciones dentro del sistema.
     * Se comunica con TransactionService, quien maneja la lógica de negocio.
     */
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        /// @brief Servicio encargado de la lógica de negocio de transacciones.
        private readonly TransactionService _service;

        /**
         * @brief Constructor del controlador.
         * @param service Instancia de TransactionService inyectada por el contenedor de dependencias.
         */
        public TransactionsController(TransactionService service)
        {
            _service = service;
        }

        /**
         * @brief Obtiene todas las transacciones registradas.
         * @return Lista de transacciones en formato JSON.
         */
        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var transactions = _service.GetAllTransactions();
            return Ok(transactions);
        }

        /**
         * @brief Obtiene una transacción por su ID.
         * @param id Identificador único de la transacción.
         * @return Transacción encontrada o error 404 si no existe.
         */
        [HttpGet("{id}")]
        public IActionResult GetTransactionById(int id)
        {
            var transaction = _service.GetTransactionById(id);

            if (transaction == null)
                return NotFound(new { error = "Transacción no encontrada." });

            return Ok(transaction);
        }

        /**
         * @brief Obtiene todas las transacciones realizadas por un comprador.
         * @param buyerId Identificador del usuario comprador.
         * @return Lista de transacciones asociadas al comprador.
         */
        [HttpGet("buyer/{buyerId}")]
        public IActionResult GetTransactionsByBuyer(int buyerId)
        {
            var transactions = _service.GetTransactionsByBuyer(buyerId);
            return Ok(transactions);
        }

        /**
         * @brief Obtiene todas las transacciones realizadas hacia un vendedor.
         * @param sellerId Identificador del usuario vendedor.
         * @return Lista de transacciones asociadas al vendedor.
         */
        [HttpGet("seller/{sellerId}")]
        public IActionResult GetTransactionsBySeller(int sellerId)
        {
            var transactions = _service.GetTransactionsBySeller(sellerId);
            return Ok(transactions);
        }

        /**
         * @brief Registra una nueva transacción en el sistema.
         * @param transaction Objeto Transaction recibido desde el frontend.
         * @return Transacción creada con código 201 o error si los datos son inválidos.
         */
        [HttpPost]
        public IActionResult AddTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
                return BadRequest(new { error = "La transacción no puede ser nula." });

            try
            {
                var created = _service.AddTransaction(transaction);
                return CreatedAtAction(nameof(GetTransactionById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /**
         * @brief Elimina una transacción por su ID.
         * @param id Identificador de la transacción.
         * @return Mensaje de éxito o error 404 si no existe.
         */
        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            bool deleted = _service.DeleteTransaction(id);

            if (!deleted)
                return NotFound(new { error = "Transacción no encontrada." });

            return Ok(new { message = "Transacción eliminada correctamente." });
        }
    }
}