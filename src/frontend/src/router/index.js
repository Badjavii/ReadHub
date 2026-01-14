import { createRouter, createWebHistory } from 'vue-router'

// User views
import LoginView from '../views/user/LoginView.vue'
import RegisterProfileView from '../views/user/RegisterProfileView.vue'
import ProfileView from '../views/user/ProfileView.vue'

// Book views
import BookView from '../views/book/BookView.vue'
import CatalogView from '../views/book/CatalogView.vue'
import RegisterView from '../views/book/RegisterView.vue'

// App view
import AppView from '../views/AppView.vue'

const routes = [
    { path: '/', redirect: '/books' },

    // User routes
    { path: '/login', component: LoginView },
    { path: '/register', component: RegisterProfileView },
    { path: '/profile', component: ProfileView },

    // Book routes
    { path: '/books', component: CatalogView },
    { path: '/books/:id', component: BookView },
    { path: '/books/register', component: RegisterView },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router