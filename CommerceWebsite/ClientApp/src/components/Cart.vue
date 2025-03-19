<template>
    <nav class="navbar">
        <div class="navbar-left">
            <a href="/">
                <img src="https://localhost:7112/images/logo.png" alt="Magento icon by Icons8" class="logo" />
            </a>
        </div>
        <div class="nav-center">
            <input v-model="searchQ"
                   type="text"
                   placeholder="Search for products"
                   class="search-bar"
                   @keyup.enter="search" />
        </div>
        <div class="nav-right">
            <a href="/cart">Cart</a>
            <a href="/orders">Orders</a>
            <a href="/">Account</a>
        </div>
    </nav>
    <div class="cart">
        <h1>Your Cart</h1>
        <div v-if="cart.length === 0">
            <p>Your cart is empty</p>
        </div>
        <div v-else>
            <div v-for="item in cart" :key="item.id" class="cart-item">
                <p>{{ item.name }} - ${{ item.price }} x {{ item.quantity }}</p>
                <button @click="removeFromCart(item.id)" class="btn btn-danger">Remove</button>
                <input v-model="item.quantity" @change="updateQuantity(item)" type="number" min="1" :max="item.Amount" />
            </div>
            <div>
                <strong>Total: ${{ cartTotal }}</strong>
            </div>
            <button @click="checkout" class="btn btn-primary">Proceed to Checkout</button>
        </div>
    </div>
</template>
<script lang="ts">
    import { defineComponent } from "vue";

    export default defineComponent({
        name: "Cart",
        data() {
            return {
                cart: [] as any[], 
            };
        },
        computed: {
            cartTotal() {
                return this.cart.reduce((total, item) => total + item.price * item.quantity, 0);
            },
        },
        mounted() {
            if (typeof window !== "undefined" && window.localStorage) {
                const savedCart = localStorage.getItem("cart");
                if (savedCart) {
                    this.cart = JSON.parse(savedCart);
                }
            }
        },
        methods: {
            removeFromCart(productId: number) {
                this.cart = this.cart.filter(item => item.id !== productId);
                localStorage.setItem("cart", JSON.stringify(this.cart));  
            },
            updateQuantity(item: any) {
                const cart = JSON.parse(localStorage.getItem("cart") || "[]");
                const productIndex = cart.findIndex(cartItem => cartItem.id === item.id);
                if (productIndex !== -1) {
                    cart[productIndex].quantity = item.quantity;
                    localStorage.setItem("cart", JSON.stringify(cart));  
                }
            },
            checkout() {
                this.$router.push('/checkout');
            },
        },
    });
</script>

<style scoped>
    .navbar {
        display: flex;
        justify-content: space-between;
        align-items: center;
        background-color: #232f3e;
        padding: 10px 20px;
        color: white;
        position: fixed;
        width: 100%;
        top: 0;
        left: 0;
        z-index: 100;
    }

    .navbar-left {
        display: flex;
        align-items: center;
    }

    .navbar-center {
        display: flex;
        justify-content: center;
        flex-grow: 1;
    }

    .nav-right {
        align-items: center;
    }

        .nav-right a {
            color: white;
            text-decoration: none;
            padding: 5px 10px;
            display: inline-block;
            transition: all 0.3s ease;
        }

            .nav-right a:hover {
                background-color: #555;
                border-radius: 4px;
            }


    .search-bar {
        width: 500px;
        padding: 10px;
        border-radius: 4px;
        border: none;
    }

    .logo {
        height: 90px;
        width: auto;
        display: block;
    }

    .cart {
        margin-top: 100px; 
        padding: 20px;
    }

    h1 {
        font-size: 2rem;
        margin-bottom: 20px;
    }

    .empty-cart {
        font-size: 1.5rem;
        color: #888;
    }

    .cart-item {
        display: flex;
        justify-content: space-between;
        align-items: center;
        background-color: lightgray;
        padding: 15px;
        border-radius: 8px;
        margin-bottom: 15px;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    }

    .item-info {
        display: flex;
        align-items: center;
        gap: 20px;
    }

    .item-name {
        font-size: 1.1rem;
        font-weight: bold;
    }

    .quantity-input {
        width: 60px;
        padding: 5px;
        font-size: 1rem;
        border: 1px solid #ccc;
        border-radius: 4px;
        text-align: center;
    }

    .remove-btn {
        padding: 8px 12px;
        background-color: #e74c3c;
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

        .remove-btn:hover {
            background-color: #c0392b;
        }

    .cart-total {
        font-size: 1.5rem;
        font-weight: bold;
        margin-top: 20px;
        text-align: right;
    }

    .checkout-btn {
        width: 100%;
        padding: 12px;
        font-size: 1.2rem;
        background-color: #007BFF;
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        margin-top: 20px;
    }

        .checkout-btn:hover {
            background-color: #0056b3;
        }

        .checkout-btn:disabled {
            background-color: #ccc;
            cursor: not-allowed;
        }
</style>
