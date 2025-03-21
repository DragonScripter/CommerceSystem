<template>
    <nav class="navbar">
        <div class="navbar-left">
            <a href="/">
                <img src="https://localhost:7112/images/logo.png" alt="Magento icon" class="logo" />
            </a>
        </div>
        <div class="navbar-center">
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
    <div>
        <h1>Order List</h1>
        <div class="container">
            <div class="orders">
                <div v-for="order in orders" :key="order.id" class="order-item">
                    <p><strong>Customer:</strong> {{ order.customerName }}</p>
                    <p><strong>Status:</strong> {{ order.orderStatus }}</p>
                    <p><strong>Total Price:</strong> ${{ order.totalPrice }}</p>
                    <p><strong>Order Date:</strong> {{ new Date(order.date).toLocaleDateString() }}</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    interface Order {
        id: number;
        customerId: number;
        customerName: string;
        orderStatus: string;
        totalPrice: number;
        date: string;
        paymentStatus: string;
    }

    export default defineComponent({
        name: "OrdersPage",
        data() {
            return {
                orders: [] as Order[],
            };
        },
        mounted() {
            this.fetchOrders();
        },
        methods: {
            async fetchOrders(): Promise<void> {
                try {
                    const token = localStorage.getItem("token");
                    if (!token) {
                        alert("Please log in.");
                        return;
                    }

                    const userId = this.getUserIdFromToken(token);
                    if (!userId) {
                        alert("Invalid user.");
                        return;
                    }
                    const response = await fetch(`https://localhost:7112/api/Product/order/${userId}`);
                    if (response.ok) {
                        this.orders = await response.json();
                    } else {
                        console.error("Failed to fetch orders");
                    }
                } catch (error) {
                    console.error("Error fetching data", error);
                }
            },
            getUserIdFromToken(token: string) {
                try {
                    const payloadBase64 = token.split('.')[1];
                    const payloadJson = atob(payloadBase64);
                    const payload = JSON.parse(payloadJson);

                    console.log(payload);


                    return payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
                } catch (error) {
                    console.error("Error decoding the token:", error);
                    return null;
                }
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

    h1 {
        display: flex;
        color: #42b983;
        align-items: center;
        justify-content: center;
    }

    .container {
        display: flex;
        flex-wrap: wrap;
        gap: 10px;
        justify-content: space-evenly;
        margin: 20px 0;
        margin-top: 80px;
    }

    .order-item {
        width: 250px;
        border: 1px solid #ddd;
        border-radius: 8px;
        padding: 16px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        background-color: #fff;
        display: flex;
        flex-direction: column;
        align-items: center;
        text-align: center;
        transition: transform 0.3s ease, box-shadow 0.3s ease;
        flex-shrink: 0;
    }

        .order-item:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 12px rgba(0, 0, 0, 0.15);
        }

        .order-item img {
            width: 50px;
            height: 50px;
            object-fit: cover;
        }

    .orders {
        display: flex;
        flex-wrap: wrap;
        gap: 20px;
        justify-content: space-evenly;
    }
</style>