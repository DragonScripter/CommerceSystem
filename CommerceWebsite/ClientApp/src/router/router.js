import { createRouter, createWebHistory } from 'vue-router';

import HomePage from '@/components/ProductDisplay.vue'; // Your components

const routes = [
    {
        path: '/',
        name: 'Home',
        component: HomePage,
    },
];

const router = createRouter({
    history: createWebHistory(), // Enables HTML5 history mode
    routes,
});

export default router;
