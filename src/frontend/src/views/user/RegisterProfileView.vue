<template>
  <div class="page">
    <nav>
      <div class="nav-title">ReadHub</div>
      <div class="nav-buttons">
        <button @click="goToCatalog">Catálogo</button>
        <button>Perfil</button>
      </div>
    </nav>

    <div class="container">
      <div class="form-box">
        <h2>Registrar Perfil</h2>

        <form @submit.prevent="registerUser">
          <label>Nombre de Usuario</label>
          <input type="text" v-model="username" required>

          <label>Email</label>
          <input type="email" v-model="email" required>

          <label>Contraseña</label>
          <input type="password" v-model="password" required>

          <label>Wallet de Bitcoin</label>
          <input type="text" v-model="walletAddress" required>

          <button type="submit" class="submit-btn">Registrar</button>
        </form>

        <!-- Botón para iniciar sesión -->
        <button class="login-btn" @click="goToLogin">
          ¿Ya tienes cuenta? Inicia sesión
        </button>

      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "RegisterProfileView",

  data() {
    return {
      username: "",
      email: "",
      password: "",
      walletAddress: ""
    };
  },

  methods: {
    goToCatalog() {
      this.$router.push("/books");
    },

    goToProfile() {
      this.$router.push("/profile");
    },

    goToLogin() {
      this.$router.push("/login");
    },

    async registerUser() {
      const userData = {
        id: 0,
        username: this.username,
        email: this.email,
        password: this.password,
        walletAddress: this.walletAddress
      };

      try {
        const response = await fetch("http://localhost:5000/api/users", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(userData)
        });

        if (!response.ok) {
          alert("Error al registrar el usuario");
          return;
        }

        const result = await response.json();
        console.log("Usuario registrado:", result);

        alert("Registro exitoso");
        this.$router.push("/login");

      } catch (error) {
        console.error("Error en la solicitud:", error);
        alert("No se pudo conectar con el servidor");
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
  max-width: 400px;
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

input {
  width: 100%;
  padding: 10px;
  margin-top: 5px;
  border: 1px solid #ccc;
  border-radius: 6px;
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

/* Botón de iniciar sesión */
.login-btn {
  margin-top: 15px;
  width: 100%;
  background-color: transparent;
  color: #1976d2;
  border: none;
  padding: 10px;
  font-size: 0.95em;
  cursor: pointer;
  text-decoration: underline;
}

.login-btn:hover {
  color: #0d47a1;
}
</style>