import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import Home from './components/HelloWorld.vue';
import Product from './components/ProductDisplay.vue'; 
import Cart from './components/Cart.vue'; 

const routes : RouteRecordRaw[] = [
    {
        path: '/', 
        name: 'Home',
        component: Home,
    },
    {
        path: '/products',
        name: 'Product',
        component : Product,
    },
    {
        path: '/cart',
        name: 'Cart',
        component: Cart,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
