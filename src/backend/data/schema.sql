-- Crear la base de datos
CREATE DATABASE IF NOT EXISTS readhub;
USE readhub;

-- Tabla de usuarios
CREATE TABLE IF NOT EXISTS users (
    id INT AUTO_INCREMENT PRIMARY KEY,        -- Identificador único
    username VARCHAR(100) NOT NULL,           -- Nombre de usuario
    email VARCHAR(150) NOT NULL UNIQUE,       -- Correo electrónico único
    password VARCHAR(150) NOT NULL,           -- Contraseña (texto plano o hash)
    walletAddress VARCHAR(200) NOT NULL       -- Dirección de wallet Bitcoin
);

-- Tabla de libros
CREATE TABLE IF NOT EXISTS books (
    id INT AUTO_INCREMENT PRIMARY KEY,        -- Identificador único del libro
    userId INT NOT NULL,                      -- Usuario vendedor
    title VARCHAR(200) NOT NULL,              -- Título del libro
    description TEXT,                         -- Descripción breve
    author VARCHAR(150),                      -- Autor
    publisher VARCHAR(150),                   -- Editorial
    year INT,                                 -- Año de publicación
    coverImageUrl VARCHAR(300),               -- URL de la portada (GitLab Raw)
    priceBTC DECIMAL(18,8) NOT NULL,          -- Precio en Bitcoin (con precisión)
    quantity INT NOT NULL DEFAULT 0,          -- Cantidad disponible en inventario
    FOREIGN KEY (userId) REFERENCES users(id) -- Relación con usuario vendedor
);

-- Tabla de transacciones
CREATE TABLE IF NOT EXISTS transactions (
    id INT AUTO_INCREMENT PRIMARY KEY,        -- Identificador único de la transacción
    buyerUserId INT NOT NULL,                 -- Usuario comprador
    sellerUserId INT NOT NULL,                -- Usuario vendedor
    bookId INT NOT NULL,                      -- Libro asociado
    amountBTC DECIMAL(18,8) NOT NULL,         -- Monto de la transacción en Bitcoin
    date DATETIME NOT NULL,                   -- Fecha y hora de la transacción
    FOREIGN KEY (buyerUserId) REFERENCES users(id),   -- Relación con comprador
    FOREIGN KEY (sellerUserId) REFERENCES users(id),  -- Relación con vendedor
    FOREIGN KEY (bookId) REFERENCES books(id)         -- Relación con libro
);
