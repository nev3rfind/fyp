import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../components/HomePage';
import LoginPage from '../components/LoginPage';

const routes = [
    {
        path: '/',
        name: 'Home',
        component: HomePage,
    },
    {
        path: '/login',
        name: 'Login',
        component: LoginPage,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;