import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import Product from './components/ProductDisplay.vue'; 
import Cart from './components/Cart.vue';
import AdminP from './components/AdminProducts.vue';
import Dashboard from './components/Dashboard.vue';
import ProductDetail from './components/ProductDetail.vue';
import Checkout from './components/Checkout.vue';
import Login from './components/Login.vue';
import SignUp from './components/SignIn.vue';
import Order from './components/Orders.vue';
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
        beforeEnter: (to, from, next) => {
            if (!localStorage.getItem('token')) {
                next({ name: 'Login', query: { redirect: to.fullPath } });
            }
            else {
                next();
            }
        },
    },
    {
        path: '/Admin',
        name: 'Admin',
        component: AdminP,
    },
    {
        path: '/sign',
        name: 'SignUp',
        component: SignUp,
    },
    {
        path: '/login',
        name: 'Login',
        component: Login,
    },
    {
        path: '/orders',
        name: 'Orders',
        component: Order,
        beforeEnter: (to, from, next) => {
            if (!localStorage.getItem('token')) {
                next({ name: 'Login', query: { redirect: to.fullPath } });
            }
            else
            {
                next();
            }
        },
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
