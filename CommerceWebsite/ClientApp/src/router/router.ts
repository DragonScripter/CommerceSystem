import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../components/HelloWorld.vue';
import ProductPage from '../components/ProductDisplay.vue'; 
import CartPage from '../components/Cart.vue'; 

const routes = [
    {
        path: '/', 
        name: 'Home',
        component: HomePage,
    },
    {
        path: '/products',
        name: 'Product',
        component : ProductPage,
    },
    {
        path: '/cart',
        name: 'Cart',
        component: CartPage,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
