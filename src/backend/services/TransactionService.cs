using backend.models;
using backend.repositories;

namespace backend.services
{
    /**
     * @class TransactionService
     * @brief Servicio que encapsula la lógica de negocio relacionada con transacciones.
     *
     * Esta clase actúa como intermediaria entre los controladores y el repositorio.
     * Valida reglas de negocio básicas y delega el acceso a datos al TransactionRepository.
     */
    public class TransactionService
    {
        /// @brief Repositorio encargado de la persistencia de transacciones.
        private readonly TransactionRepository _repository;

        /**
         * @brief Constructor del servicio.
         * @param repository Instancia de TransactionRepository inyectada desde configuración.
         */
        public TransactionService(TransactionRepository repository)
        {
            _repository = repository;
        }

        /**
         * @brief Obtiene todas las transacciones registradas.
         * @return Lista completa de transacciones.
         */
        public List<Transaction> GetAllTransactions()
        {
            return _repository.GetAll();
        }

        /**
         * @brief Obtiene una transacción por su ID.
         * @param id Identificador único de la transacción.
         * @return Instancia Transaction si existe, nullptr si no se encontró.
         */
        public Transaction? GetTransactionById(int id)
        {
            return _repository.GetById(id);
        }

        /**
         * @brief Obtiene todas las transacciones realizadas por un comprador.
         * @param buyerId Identificador del usuario comprador.
         * @return Lista de transacciones asociadas al comprador.
         */
        public List<Transaction> GetTransactionsByBuyer(int buyerId)
        {
            return _repository.GetByBuyer(buyerId);
        }

        /**
         * @brief Obtiene todas las transacciones realizadas hacia un vendedor.
         * @param sellerId Identificador del usuario vendedor.
         * @return Lista de transacciones asociadas al vendedor.
         */
        public List<Transaction> GetTransactionsBySeller(int sellerId)
        {
            return _repository.GetBySeller(sellerId);
        }

        /**
         * @brief Registra una nueva transacción en el sistema.
         *
         * Valida que los IDs sean válidos y que el monto sea positivo.
         *
         * @param transaction Objeto Transaction con los datos a registrar.
         * @return La transacción recién registrada.
         * @exception Exception Si los datos son inválidos.
         */
        public Transaction AddTransaction(Transaction transaction)
        {
            ValidateTransaction(transaction);
            _repository.Add(transaction);
            return transaction;
        }

        /**
         * @brief Elimina una transacción por su ID.
         * @param id Identificador de la transacción.
         * @return true si se eliminó correctamente, false si no existía.
         */
        public bool DeleteTransaction(int id)
        {
            return _repository.Delete(id);
        }

        /**
         * @brief Valida que los datos de la transacción sean correctos.
         * @param transaction Transacción a validar.
         * @exception Exception Si algún dato es inválido.
         */
        private void ValidateTransaction(Transaction transaction)
        {
            if (transaction.BuyerUserId <= 0)
                throw new Exception("El ID del comprador es inválido.");

            if (transaction.SellerUserId <= 0)
                throw new Exception("El ID del vendedor es inválido.");

            if (transaction.BookId <= 0)
                throw new Exception("El ID del libro es inválido.");

            if (transaction.AmountBTC <= 0)
                throw new Exception("El monto debe ser mayor a cero.");
        }
    }
}