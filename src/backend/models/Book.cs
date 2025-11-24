using System.Text.Json.Serialization;
using System;

namespace backend.models
{
    /**
     * @class Book
     * @brief Representa un libro dentro del sistema ReadHub.
     * 
     * Cada libro está asociado a un usuario vendedor mediante su UserId.
     * Se almacenan los datos básicos de identificación, descripción y precio del libro.
     */
    public class Book
    {
        /**
         * @brief Identificador único del libro en el sistema.
         */
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        /**
         * @brief Identificador del usuario que está vendiendo el libro.
         */
        [JsonPropertyName("_userId")]
        public int UserId { get; set; }

        /** 
         * @brief Título del libro.
         */
        [JsonPropertyName("_title")]
        public string Title { get; set; }

        /** 
         * @brief Descripción breve del libro.
         */
        [JsonPropertyName("_description")]
        public string Description { get; set; }

        /** 
         * @brief Nombre del autor.
         */
        [JsonPropertyName("_author")]
        public string Author { get; set; }

        /** 
         * @brief Editorial del libro.
         */
        [JsonPropertyName("_publisher")]
        public string Publisher { get; set; }

        /** 
         * @brief Año de publicación.
         */
        [JsonPropertyName("_year")]
        public int Year { get; set; }

        /** 
         * @brief URL de la imagen de portada almacenada en Google Cloud Storage.
         */
        [JsonPropertyName("_coverImageUrl")]
        public string CoverImageUrl { get; set; }

        /**
         * @brief Precio del libro expresado en Bitcoin.
         */
        [JsonPropertyName("_priceBTC")]
        public decimal PriceBTC { get; set; }

        /**
         * @brief Cantidad de unidades disponibles de este libro.
         */
        [JsonPropertyName("_quantity")]
        public int Quantity { get; set; }

        /**
         * @brief Constructor básico para inicializar un libro.
         */
        public Book(int id, int userId, string title, string description, string author, string publisher, int year, string coverImageUrl, decimal priceBTC, int quantity)
        {
            Id = id;
            UserId = userId;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            Year = year;
            CoverImageUrl = coverImageUrl ?? throw new ArgumentNullException(nameof(coverImageUrl));
            PriceBTC = priceBTC;
            Quantity = quantity;
        }
    }
}