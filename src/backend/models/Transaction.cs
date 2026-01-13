using System.Text.Json.Serialization;
using System;

namespace backend.models
{
    /**
     * @class Transaction
     * @brief Representa una transacción dentro del sistema ReadHub.
     * 
     * Una transacción involucra a un comprador, un vendedor, un libro,
     * y un monto expresado en Bitcoin.
     */
    public class Transaction
    {
        /**
         * @brief Identificador único de la transacción.
         */
        //[JsonPropertyName("_id")]
        public int Id { get; set; }

        /**
         * @brief Identificador del usuario comprador.
         */
        //[JsonPropertyName("_buyerUserId")]
        public int BuyerUserId { get; set; }

        /**
         * @brief Identificador del usuario vendedor.
         */
        //[JsonPropertyName("_sellerUserId")]
        public int SellerUserId { get; set; }

        /**
         * @brief Identificador del libro asociado a la transacción.
         */
        //[JsonPropertyName("_bookId")]
        public int BookId { get; set; }

        /**
         * @brief Monto de la transacción en Bitcoin.
         */
        //[JsonPropertyName("_amountBTC")]
        public decimal AmountBTC { get; set; }

        /**
         * @brief Fecha en la que se realizó la transacción.
         */
        //[JsonPropertyName("_date")]
        public DateTime Date { get; set; }

        /**
         * @brief Constructor básico para inicializar una transacción.
         * @param id Identificador único de la transacción.
         * @param buyerUserId Usuario comprador.
         * @param sellerUserId Usuario vendedor.
         * @param bookId Identificador del libro.
         * @param amountBTC Monto en Bitcoin.
         * @param date Fecha de realización.
         */
        public Transaction(int id, int buyerUserId, int sellerUserId, int bookId, decimal amountBTC, DateTime date)
        {
            Id = id;
            BuyerUserId = buyerUserId;
            SellerUserId = sellerUserId;
            BookId = bookId;
            AmountBTC = amountBTC;
            Date = date;
        }
    }
}