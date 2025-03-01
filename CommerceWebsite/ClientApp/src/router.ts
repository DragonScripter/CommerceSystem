import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import Product from './components/ProductDisplay.vue'; 
import Cart from './components/Cart.vue';
import AdminP from './components/AdminProducts.vue';
import Dashboard from './components/Dashboard.vue';
import ProductDetail from './components/ProductDetail.vue';
import Checkout from './components/Checkout.vue';
const routes : RouteRecordRaw[] = [
    {
        path: '/', 
        name: 'Home',
        component: Product,
    },
    {
        path: '/product/:id',
        name: 'Product-Detail',
        component: ProductDetail,
        props: true
    },
    {
        path: '/cart',
        name: 'Cart',
        component: Cart,
    },
    {
        path: '/checkout',
        name: 'Checkout',
        component: Checkout,
    },
    {
        path: '/Admin',
        name: 'Admin',
        component: AdminP,
    },
    {
        path: '/Dash',
        name: 'DashBoard',
        component: Dashboard,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
