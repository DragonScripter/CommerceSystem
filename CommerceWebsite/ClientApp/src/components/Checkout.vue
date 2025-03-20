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
            <a href="/orders">Orders</a>
            <a href="/">Account</a>
        </div>
    </nav>
    <div class="container">
        <div class="product-container" v-for="item in cart" :key="item.id">
            <div class="product-detail">
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
        <div class="right-container">
            <div class="button">
                <button @click="buyNow(product)">Buy Now</button>
            </div>
            <p>By placing your order, you agree to our store's <a href="/" target="_blank">terms and conditions</a>. Please review them carefully.</p>
            <div class="order">
                <h3>Order Summary</h3>
                <p>Items: ${{cartTotal}}</p>
                <p>Tax: ${{tax}}</p>
                <p>Delivery fee: ${{delivery}}</p>
            </div>
            <div class="order-total">
                <h3>Order Total: ${{total}}</h3>
            </div>

        </div>
    </div>



</template>

<script lang="ts">
    import { defineComponent } from "vue";

    interface Product {
        id: number;
        name: string;
        description: string;
        price: number;
        quantity: number;  
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
            cartTotal() {
                return this.cart.reduce((total, item) => total + item.price * item.quantity, 0);
            },
            tax() {
                return this.cartTotal * 0.10;
            },
            delivery() {
                return this.cartTotal * 0.005;
            },
            total() {
                return this.cartTotal + this.tax + this.delivery;
            }
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
            async buyNow() {
                const token = localStorage.getItem("token");
                if (!token) {
                    alert("Please log in to complete the purchase.");
                    this.$router.push({ name: "Login", query: { redirect: "/checkout" } });
                    return;
                }

                const userId = this.getUserIdFromToken(token);  
                const orderData = {
                    CustomerId: userId,
                    Products: this.cart.map(item => ({
                        ProductId: item.id,
                        Name: item.name,
                        Description: item.description,
                        Quantity: item.quantity,
                        Price: item.price
                    })),
                   
                };

                try {
                    const response = await fetch(`https://localhost:7112/api/Product/place`, {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json",
                            "Authorization": `Bearer ${token}`
                        },
                        body: JSON.stringify(orderData)
                    });

                    const result = await response.json();
                    if (response.ok) {
                        alert("Order placed successfully!");
                        localStorage.removeItem("cart");
                        this.$router.push("/orders");
                    } else {
                        alert(result.Message);
                    }
                } catch (error) {
                    console.error("Error placing order:", error);
                    alert("An error occurred. Please try again.");
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

        }
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
        display: flex;
        flex-direction: row;
        min-height: 100vh;
        margin-top: 120px;
        width: 100%;
        flex-wrap: nowrap;
    }

    .product-detail {
        display: flex;
        flex-direction: column;
        padding: 20px;
        background-color: #f4f4f4;
        width: 70%;
        align-items: flex-start;
        margin-bottom: 20px;
        flex-grow: 1;
    }

    .product-container {
        display: flex;
        flex-direction: row;
        justify-content: flex-start;
        align-items: flex-start;
        width: 100%;
        background-color: #fff;
        padding: 20px;
        margin-top: 20px;
        margin-bottom: 20px;
        border-radius: 10px;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    }

    .image-container {
        flex: 0 0 200px;
        margin-right: 20px;
    }

        .image-container img {
            width: 100%;
            height: auto;
            object-fit: cover;
            border-radius: 10px;
        }

    .product-info {
        flex: 1;
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

    .right-container {
        width: 30%;
        padding: 20px;
        background-color: #fff;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        border-radius: 8px;
        margin-top: 20px;
        margin-bottom: 20px;
        flex-shrink: 0;
        height: auto;
        display: flex;
        flex-direction: column;
        position: sticky;
        top: 120px;
    }

        .right-container .button {
            text-align: center;
            margin-bottom: 20px;
        }

            .right-container .button button {
                padding: 10px 20px;
                background-color: #232f3e;
                color: white;
                font-size: 1.2rem;
                border: none;
                border-radius: 5px;
                cursor: pointer;
                width: 100%;
            }

                .right-container .button button:hover {
                    background-color: #555;
                }

        .right-container p {
            font-size: 1rem;
            color: #666;
            margin: 10px 0;
        }

        .right-container a {
            color: #232f3e;
            text-decoration: none;
        }

        .right-container .order {
            margin-top: 20px;
            margin-bottom: 0px;
            padding-bottom: 20px;
            border-bottom: 1px solid #ddd;
        }

            .right-container .order h3 {
                font-size: 1.5rem;
                margin-bottom: 0px;
            }

            .right-container .order p {
                font-size: 1.2rem;
                margin: 5px 0;
                color: #333;
            }

        .right-container .order-total {
            font-size: 1.5rem;
            margin-top: 20px;
            margin-bottom: 0;
        }

            .right-container .order-total h3 {
                font-size: 1.8rem;
                font-weight: bold;
                color: #232f3e;
            }
</style>