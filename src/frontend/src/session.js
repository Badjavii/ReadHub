// Inicializa valores por defecto si no existen
if (localStorage.getItem("isLogged") === null) {
    localStorage.setItem("isLogged", "false");
}

if (localStorage.getItem("userEmail") === null) {
    localStorage.setItem("userEmail", "");
}