<template>
  <div class="page">
    <nav>
      <div class="nav-title">ReadHub</div>
      <div class="nav-buttons">
        <button @click="goToCatalog">Catálogo</button>
        <button @click="goToRegisterBook" class="register-nav-btn">Registrar Libro</button>
        <button @click="goToProfile">Perfil</button>
      </div>
    </nav>

    <div class="content">
      <div class="search-row">
        <input
          type="text"
          v-model="searchQuery"
          placeholder="Buscar libro por nombre..."
          class="search-input"
        >
      </div>

      <div class="books-grid">
        <div
          class="book-card"
          v-for="book in filteredBooks"
          :key="book.id"
        >
          <h3>{{ book.title }}</h3>
          <p>{{ book.author }}</p>
          <p class="price">Precio: {{ book.priceBTC }} BTC</p>

          <button class="view-btn" @click="goToBook(book.id)">
            Ver más
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "CatalogView",

  data() {
    return {
      books: [],
      searchQuery: ""
    };
  },

  created() {
    this.fetchBooks();
  },

  methods: {
    goToCatalog() {
      this.$router.push("/books");
    },

    goToProfile() {
      this.$router.push("/profile");
    },

    goToRegisterBook() {
      this.$router.push("/books/register");
    },

    goToBook(id) {
      this.$router.push(`/books/${id}`);
    },

    async fetchBooks() {
      try {
        const response = await fetch("http://localhost:5000/api/books");

        if (!response.ok) {
          alert("No se pudieron obtener los libros");
          return;
        }

        const data = await response.json();
        this.books = data;

      } catch (error) {
        console.error("Error al obtener libros:", error);
        alert("Error al conectar con el servidor");
      }
    }
  },

  computed: {
    filteredBooks() {
      const query = this.searchQuery.toLowerCase();

      return this.books.filter(function(book) {
        return book.title.toLowerCase().includes(query);
      });
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

.content {
  padding: 20px;
  flex: 1;
  overflow-y: auto;
}

.search-row {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 15px;
  margin-bottom: 20px;
}

.search-input {
  width: 60%;
  max-width: 500px;
  padding: 12px;
  border-radius: 6px;
  border: 1px solid #ccc;
  font-size: 1em;
}

.register-btn {
  background-color: #1976d2;
  color: white;
  border: none;
  padding: 6px 10px;
  border-radius: 6px;
  cursor: pointer;
  font-size: 0.95em;
}

.register-btn:hover {
  background-color: #1565c0;
}

.books-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
  gap: 20px;
}

.book-card {
  background-color: white;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  text-align: center;
}

.book-card h3 {
  color: #1976d2;
  margin-bottom: 10px;
}

.price {
  font-size: 1.1em;
  color: #333;
  font-weight: bold;
}

.view-btn {
  margin-top: 15px;
  width: 100%;
  background-color: #1565c0;
  color: white;
  border: none;
  padding: 10px;
  border-radius: 6px;
  cursor: pointer;
}

.view-btn:hover {
  background-color: #0d47a1;
}
</style>