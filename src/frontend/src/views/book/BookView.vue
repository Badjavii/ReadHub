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
      <div class="book-layout">

        <!-- COLUMNA IZQUIERDA: IMAGEN -->
        <div class="image-column">
          <div class="image-box">
            <img
              v-if="book.coverImageUrl && book.coverImageUrl.trim() !== ''"
              :src="book.coverImageUrl"
              alt="Portada del libro"
            >
            <div v-else class="no-image">
              Imagen no disponible
            </div>
          </div>
        </div>

        <!-- COLUMNA DERECHA: INFORMACIÓN -->
        <div class="info-column">
          <h2>{{ book.title }}</h2>

          <div class="field"><strong>Autor:</strong> {{ book.author }}</div>
          <div class="field"><strong>Editorial:</strong> {{ book.publisher }}</div>
          <div class="field"><strong>Año:</strong> {{ book.year }}</div>
          <div class="field"><strong>Descripción:</strong> {{ book.description }}</div>
          <div class="field"><strong>Precio:</strong> {{ book.priceBTC }} BTC</div>
          <div class="field"><strong>Cantidad disponible:</strong> {{ book.quantity }}</div>

          <button class="buy-btn" @click="buyBook">
            Comprar libro
          </button>

          <button class="back-btn" @click="goToCatalog">
            Volver al catálogo
          </button>
        </div>

      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BookView",

  data() {
    return {
      book: {
        id: 0,
        userId: 0,
        title: "",
        description: "",
        author: "",
        publisher: "",
        year: "",
        coverImageUrl: "",
        priceBTC: "",
        quantity: 0
      }
    };
  },

  created() {
    this.fetchBook();
  },

  methods: {
    goToCatalog() {
      this.$router.push("/books");
    },

    goToProfile() {
      this.$router.push("/profile");
    },

    async fetchBook() {
      const id = this.$route.params.id;

      try {
        const response = await fetch(`http://localhost:5000/api/books/${id}`);

        if (!response.ok) {
          alert("No se pudo obtener la información del libro");
          return;
        }

        const data = await response.json();
        this.book = data;

      } catch (error) {
        console.error("Error al obtener el libro:", error);
        alert("Error al conectar con el servidor");
      }
    },

    async buyBook() {
  const buyerEmail = localStorage.getItem("userEmail");

  if (!buyerEmail) {
    alert("Debes iniciar sesión para comprar");
    this.$router.push("/login");
    return;
  }

  try {
    // 1. Obtener el ID del comprador desde el backend
    const userResponse = await fetch(`http://localhost:5000/api/users/by-email?email=${encodeURIComponent(buyerEmail)}`);

    if (!userResponse.ok) {
      alert("No se pudo obtener la información del comprador");
      return;
    }

    const buyerData = await userResponse.json();
    const buyerId = buyerData.id;

    // 2. Construir la transacción EXACTAMENTE como el backend la espera
    const transaction = {
      buyerUserId: buyerId,
      sellerUserId: this.book.userId,
      bookId: this.book.id,
      amountBTC: this.book.priceBTC,
      date: new Date().toISOString()
    };

    // 3. Enviar la transacción al backend
    const response = await fetch("http://localhost:5000/api/transactions", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(transaction)
    });

    if (!response.ok) {
      alert("No se pudo completar la compra");
      return;
    }

    alert("Compra realizada con éxito");
    this.$router.push("/books");

  } catch (error) {
    console.error("Error al comprar:", error);
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
  padding: 40px;
}

/* 🔥 Columnas más largas y equilibradas */
.book-layout {
  display: flex;
  gap: 40px;
  width: 100%;
  max-width: 1100px;
  min-height: 500px; /* ← más largo */
}

/* COLUMNA IZQUIERDA */
.image-column {
  flex: 1;
  display: flex;
  align-items: stretch;
}

.image-box {
  width: 100%;
  height: 100%;
  min-height: 450px; /* ← más largo */
  border-radius: 10px;
  overflow: hidden;
  background-color: #e0e0e0;
  display: flex;
  justify-content: center;
  align-items: center;
}

.image-box img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.no-image {
  font-size: 1.1em;
  color: #555;
}

/* COLUMNA DERECHA */
.info-column {
  flex: 1;
  padding: 25px;
  border: 1px solid #c7c7c7; /* ← marco delgadito */
  border-radius: 10px;
  background-color: white;
  min-height: 450px; /* ← misma altura que la imagen */
  display: flex;
  flex-direction: column;
}

.info-column h2 {
  color: #1976d2;
  margin-bottom: 20px;
}

.field {
  margin: 12px 0;
  font-size: 1em;
  color: #333;
}

.field strong {
  color: #1565c0;
}

/* BOTONES */
.buy-btn {
  margin-top: auto; /* empuja hacia abajo */
  width: 100%;
  background-color: #2e7d32;
  color: white;
  border: none;
  padding: 12px;
  border-radius: 6px;
  font-size: 1em;
  cursor: pointer;
}

.buy-btn:hover {
  background-color: #1b5e20;
}

.back-btn {
  margin-top: 10px;
  width: 100%;
  background-color: #1976d2;
  color: white;
  border: none;
  padding: 12px;
  border-radius: 6px;
  font-size: 1em;
  cursor: pointer;
}

.back-btn:hover {
  background-color: #1565c0;
}
</style>