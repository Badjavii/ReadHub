<template>
  <div class="page">
    <nav>
      <div class="nav-title">ReadHub</div>
      <div class="nav-buttons">
        <button @click="goToCatalog">Catálogo</button>
        <button @click="goToProfile">Perfil</button>
      </div>
    </nav>

    <div class="container">
      <div class="form-box">
        <h2>Registrar Libro</h2>

        <form @submit.prevent="registerBook">

          <label>Título</label>
          <input type="text" v-model="title" required>

          <label>Descripción</label>
          <textarea v-model="description" required></textarea>

          <label>Autor</label>
          <input type="text" v-model="author" required>

          <label>Editorial</label>
          <input type="text" v-model="publisher" required>

          <label>Año de publicación</label>
          <input type="number" v-model="year" required>

          <label>Precio (BTC)</label>
          <input type="number" step="0.00000001" v-model="priceBTC" required>

          <label>Cantidad disponible</label>
          <input type="number" v-model="quantity" required>

          <button type="submit" class="submit-btn">Registrar Libro</button>
        </form>

      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "RegisterView",

  data() {
    return {
      title: "",
      description: "",
      author: "",
      publisher: "",
      year: "",
      priceBTC: "",
      quantity: ""
    };
  },

  methods: {
    goToCatalog() {
      this.$router.push("/books");
    },

    goToProfile() {
      this.$router.push("/profile");
    },

    async registerBook() {
      const email = localStorage.getItem("userEmail");

      if (!email) {
        alert("Debes iniciar sesión para registrar un libro");
        this.$router.push("/login");
        return;
      }

      try {
        // 1. Obtener ID del usuario
        const userResponse = await fetch(`http://localhost:5000/api/users/by-email?email=${encodeURIComponent(email)}`);
        if (!userResponse.ok) {
          alert("No se pudo obtener el usuario");
          return;
        }
        const userData = await userResponse.json();
        const userId = userData.id;

        // 2. Obtener todos los libros para calcular el nuevo ID
        const booksResponse = await fetch("http://localhost:5000/api/books");
        if (!booksResponse.ok) {
          alert("No se pudieron obtener los libros");
          return;
        }
        const books = await booksResponse.json();
        const newId = books.length + 1;

        // 3. Construir el libro EXACTO como lo espera el backend
        const newBook = {
          id: newId,
          userId: userId,
          title: this.title,
          description: this.description,
          author: this.author,
          publisher: this.publisher,
          year: parseInt(this.year),
          coverImageUrl: "", // SIEMPRE vacío
          priceBTC: parseFloat(this.priceBTC),
          quantity: parseInt(this.quantity)
        };

        // 4. Enviar al backend
        const response = await fetch("http://localhost:5000/api/books", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(newBook)
        });

        if (!response.ok) {
          alert("No se pudo registrar el libro");
          return;
        }

        alert("Libro registrado con éxito");
        this.$router.push("/books");

      } catch (error) {
        console.error("Error al registrar libro:", error);
        alert("Error al conectar con el servidor");
      }
    }
  }
};
</script>

<style>
.page {
  margin: 0;
  font-family: 'Segoe UI', sans-serif;
  background-color: #f0f4ff;
  display: flex;
  flex-direction: column;
  height: 100vh;
}

nav {
  background-color: #1976d2;
  color: white;
  padding: 12px 24px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.nav-title {
  font-size: 1.5em;
  font-weight: bold;
  letter-spacing: 1px;
}

.nav-buttons button {
  background-color: #1565c0;
  border: none;
  color: white;
  padding: 8px 16px;
  margin-left: 10px;
  border-radius: 4px;
  cursor: pointer;
}

.nav-buttons button:hover {
  background-color: #0d47a1;
}

.container {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.form-box {
  background-color: white;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 450px;
}

h2 {
  text-align: center;
  color: #1976d2;
  margin-bottom: 20px;
}

label {
  display: block;
  margin-top: 15px;
  color: #333;
  font-weight: 500;
}

input, textarea {
  width: 100%;
  padding: 10px;
  margin-top: 5px;
  border: 1px solid #ccc;
  border-radius: 6px;
}

textarea {
  height: 80px;
  resize: none;
}

.submit-btn {
  margin-top: 25px;
  width: 100%;
  background-color: #1976d2;
  color: white;
  border: none;
  padding: 12px;
  border-radius: 6px;
  font-size: 1em;
  cursor: pointer;
}

.submit-btn:hover {
  background-color: #1565c0;
}
</style>