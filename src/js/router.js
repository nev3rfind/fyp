import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../components/HomePage';
import LoginPage from '../components/LoginPage';
import NotFound from '../components/NotFound';
import PatientSearch from '../components/PatientSearch';
import store from "./store";

const routes = [
    {
        path: '/',
        name: 'Home',
        component: HomePage,
        meta: { requiresAuth: true },
    },
    {
        path: '/login',
        name: 'Login',
        component: LoginPage,
        meta: { requiresAuth: false },
    },
    {
        path: '/:pathMatch(.*)*', // Add a wildcard route for non-existing paths
        name: 'NotFound',
        component: NotFound, // 404 component
    },
    {
        path: '/search',
        name: 'Search',
        component: PatientSearch,
        meta: { requiresAuth: true },
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    const isLoggedIn = store.getters.isLoggedIn;
    const requiresAuth = to.meta.requiresAuth;

    if (to.name === 'NotFound') {
        // If trying to access a non-existing route, proceed to the 404 component
        next();
    } else if (requiresAuth && !isLoggedIn) {
        // If the route requires authentication and the user is not logged in, redirect to the login page
        next({ name: 'Login' });
    } else if (!requiresAuth && isLoggedIn) {
        // If the route doesn't require authentication and the user is already logged in, redirect to the home page
        next({ name: 'Home' });
    } else {
        // If none of the conditions above apply, proceed to the requested route
        next();
    }
});


export default router;