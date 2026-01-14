<template>
  <div class="page">
    <nav>
      <div class="nav-title">ReadHub</div>
      <div class="nav-buttons">
        <button @click="goToCatalog">Catálogo</button>
        <button @click="goToRegister">Registrarse</button>
      </div>
    </nav>

    <div class="container">
      <div class="form-box">
        <h2>Iniciar Sesión</h2>

        <form @submit.prevent="loginUser">
          <label>Email</label>
          <input type="email" v-model="email" required>

          <label>Contraseña</label>
          <input type="password" v-model="password" required>

          <button type="submit" class="submit-btn">Ingresar</button>
        </form>

        <button class="register-btn" @click="goToRegister">
          ¿No tienes cuenta? Regístrate aquí
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "LoginView",

  data() {
    return {
      email: "",
      password: ""
    };
  },

  methods: {
    goToCatalog() {
      this.$router.push("/books");
    },

    goToRegister() {
      this.$router.push("/register");
    },

    async loginUser() {
      const loginData = {
        email: this.email,
        password: this.password
      };

      try {
        const response = await fetch("http://localhost:5000/api/users/login", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(loginData)
        });

        if (!response.ok) {
          alert("Credenciales incorrectas");
          return;
        }

        const result = await response.json();

        if (result.success === true) {
          // Guardar sesión
          localStorage.setItem("isLogged", "true");
          localStorage.setItem("userEmail", this.email);

          alert("Inicio de sesión exitoso");

          // Redirigir al catálogo
          this.$router.push("/books");
        } else {
          alert("Correo o contraseña incorrectos");
        }

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

.register-btn {
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

.register-btn:hover {
  color: #0d47a1;
}
</style>