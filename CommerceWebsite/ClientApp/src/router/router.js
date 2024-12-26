import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../components/ProductDisplay.vue'; 

const routes = [
    {
        path: '/home', 
        name: 'Home',
        component: HomePage,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
