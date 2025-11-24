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
    coverImageUrl VARCHAR(300),               -- URL de la portada (Google Cloud Storage)
    priceBTC DECIMAL(18,8) NOT NULL,          -- Precio en Bitcoin (con precisión)
    FOREIGN KEY (userId) REFERENCES users(id) -- Relación con usuario
);
