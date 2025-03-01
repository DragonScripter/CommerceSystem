<template>
    <nav class="navbar">
        <div class="navbar-left">
            <a href="/">
                <img src="https://localhost:7112/images/logo.png" alt="Magento icon by Icons8" class="logo" />
            </a>
        </div>
        <div class="nav-center">
            <h1>Checkout</h1>
        </div>
        <div class="nav-right">
            <a href="/cart">Cart</a>
            <a href="/">Orders</a>
            <a href="/">Account</a>
        </div>
    </nav>
    <div class="container">
        <div class="product-detail" v-for="item in cart" :key="item.id">
            <div class="product-container">
                <div class="image-container">
                    <img :src="'https://localhost:7112/images/' + item.id + '.jpg'" alt="Product Image" />
                </div>

                <div class="product-info">
                    <h1>{{ item.name }}</h1>
                    <p class="description">{{ item.description }}</p>
                    <p><strong>Price:</strong> ${{ item.price }}</p>
                </div>
                <div class="side-panel">
                    <div class="price-info">
                        <p>Choose your delivery options: </p>
                        <label>
                            <input type="radio" value="tomorrow" v-model="deliveryDate" />
                            {{formattedTomorrow}}
                        </label>
                        <label>
                            <input type="radio" :value="weekAfter" v-model="deliveryDate" />
                            {{ formattedWeekAfter }}
                        </label>
                        <p>Your selected delivery date is: {{ deliveryDate }}</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="right-container">
    </div>

</template>

<script lang="ts">
    import { defineComponent } from "vue";

    interface Product {
        id: number;
        name: string;
        description: string;
        price: number;
        amount: number;
        ImageUrl: string | null;
    }

    export default defineComponent({
        name: "Checkout",
        data() {
            return {
                cart: [] as Product[],
                deliveryDate: "",
                tomorrow: this.getDate(1),
                weekAfter: this.getDate(7),
            };
        },
        mounted() {
            if (typeof window !== "undefined" && window.localStorage) {
                const savedCart = localStorage.getItem("cart");
                if (savedCart) {
                    this.cart = JSON.parse(savedCart);
                }
            }
        },
        computed: {
            formattedTomorrow() {
                return this.formatDate(this.tomorrow);
            },
            formattedWeekAfter() {
                return this.formatDate(this.weekAfter); 
            },
        },
        methods: {
            getDate(daysToAdd: number) {
                const date = new Date();
                date.setDate(date.getDate() + daysToAdd);
                return date.toISOString().split('T')[0];
            },
            formatDate(date: string) { 
                const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
                const formattedDate = new Date(date).toLocaleDateString(undefined, options);
                return formattedDate;
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
        z-index: 1;
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

    .logo {
        height: 90px;
        width: auto;
        display: block;
    }

    h1 {
        font-size: 3.5rem;
        margin-bottom: 20px;
    }

    .container {
        min-height: 100vh;
        margin-top: 120px;
        width: 100%;
    }

    .product-detail {
        display: flex;
        justify-content: flex-start;
        align-items: center;
        flex-direction: column;
        padding: 20px;
        background-color: #f4f4f4;
        width: 70%;
    }

    .right-container {
        width: 30%;
        padding: 20px;
        background-color: #f9f9f9;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    }

    .product-container {
        display: flex;
        justify-content: space-between;
        align-items: flex-start;
        width: 100%;
        max-width: 1200px;
        background-color: #fff;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        padding: 20px;
        margin-top: 0;
        margin-bottom: 0;
    }



    .image-container {
        flex: 1;
        width: 100%;
        max-width: 200px;
        margin-right: 20px;
    }

        .image-container img {
            width: 100%;
            height: 200px;
            object-fit: cover;
            border-radius: 10px;
        }


    .product-info {
        flex: 2;
        max-width: 600px;
    }

        .product-info h1 {
            font-size: 2rem;
            margin-bottom: 15px;
            color: #333;
        }

        .product-info .description {
            font-size: 1rem;
            color: #666;
            margin-bottom: 15px;
        }

        .product-info p {
            font-size: 1.2rem;
            color: #333;
        }

    .side-panel {
        width: 300px;
        background-color: white;
        padding: 20px;
        border-radius: 8px;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        position: sticky;
        top: 20px;
    }
</style>