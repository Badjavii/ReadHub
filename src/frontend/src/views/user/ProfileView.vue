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
      <div class="profile-box">
        <h2>Consultar Perfil</h2>

        <div class="field"><strong>Nombre de Usuario:</strong> {{ user.username }}</div>
        <div class="field"><strong>Email:</strong> {{ user.email }}</div>
        <div class="field"><strong>Wallet BTC:</strong> {{ user.walletAddress }}</div>
        <div class="field"><strong>Libros publicados:</strong> {{ user.books }}</div>
        <div class="field"><strong>Transacciones realizadas:</strong> {{ user.transactions }}</div>

        <button class="logout-btn" @click="logout">
          Cerrar sesión
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ProfileView",

  data() {
    return {
      user: {
        id: 0,
        username: "",
        email: "",
        walletAddress: "",
        books: 0,
        transactions: 0
      }
    };
  },

  created() {
    this.checkSession();
    this.fetchUserFromBackend();
  },

  methods: {
    goToCatalog() {
      this.$router.push("/books");
    },

    goToProfile() {
      this.$router.push("/profile");
    },

    checkSession() {
      const isLogged = localStorage.getItem("isLogged");

      if (isLogged !== "true") {
        alert("Debes iniciar sesión primero");
        this.$router.push("/register");
      }
    },

    async fetchUserFromBackend() {
      const email = localStorage.getItem("userEmail");

      if (!email || email.trim() === "") {
        alert("Error: no se encontró el correo del usuario");
        this.$router.push("/register");
        return;
      }

      try {
        // 1. Obtener datos del usuario
        const response = await fetch(`http://localhost:5000/api/users/by-email?email=${encodeURIComponent(email)}`);

        if (!response.ok) {
          alert("No se pudo obtener la información del usuario");
          return;
        }

        const data = await response.json();

        this.user.id = data.id;
        this.user.username = data.username;
        this.user.email = data.email;
        this.user.walletAddress = data.walletAddress;

        // 2. Obtener libros publicados por este usuario
        await this.countBooks();

        // 3. Obtener transacciones donde participa este usuario
        await this.countTransactions();

      } catch (error) {
        console.error("Error al obtener datos del usuario:", error);
        alert("Error al conectar con el servidor");
      }
    },

    async countBooks() {
      try {
        const response = await fetch("http://localhost:5000/api/books");

        if (!response.ok) {
          alert("No se pudieron obtener los libros");
          return;
        }

        const books = await response.json();

        // Contar libros donde userId == id del usuario
        const count = books.filter(book => book.userId === this.user.id).length;

        this.user.books = count;

      } catch (error) {
        console.error("Error al contar libros:", error);
      }
    },

    async countTransactions() {
      try {
        const response = await fetch("http://localhost:5000/api/transactions");

        if (!response.ok) {
          alert("No se pudieron obtener las transacciones");
          return;
        }

        const transactions = await response.json();

        // Contar transacciones donde el usuario es comprador o vendedor
        const count = transactions.filter(t =>
          t.buyerUserId === this.user.id || t.sellerUserId === this.user.id
        ).length;

        this.user.transactions = count;

      } catch (error) {
        console.error("Error al contar transacciones:", error);
      }
    },

    logout() {
      localStorage.setItem("isLogged", "false");
      localStorage.setItem("userEmail", "");

      alert("Sesión cerrada");

      this.$router.push("/books");
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

.profile-box {
  background-color: white;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 400px;
}

h2 {
  text-align: center;
  color: #1976d2;
  margin-bottom: 20px;
}

.field {
  margin: 15px 0;
  font-size: 1em;
  color: #333;
}

.field strong {
  color: #1565c0;
}

.logout-btn {
  margin-top: 25px;
  width: 100%;
  background-color: #d32f2f;
  color: white;
  border: none;
  padding: 12px;
  border-radius: 6px;
  font-size: 1em;
  cursor: pointer;
}

.logout-btn:hover {
  background-color: #b71c1c;
}
</style>