<template>
    <nav class="navbar">
        <div class="navbar-left">
            <a href="/products">
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
            <a href="/">Orders</a>
            <a href="/">Account</a>
        </div>
    </nav>
    <div class="product-detail" v-if="product">
        <div class="product-container">
            <div class="image-container">
                <img :src="'https://localhost:7112/images/' + product.id + '.jpg'" alt="Product Image" />
            </div>

            <div class="product-info">
                <h1>{{ product.name }}</h1>
                <p class="description">{{ product.description }}</p>
                <p><strong>Price:</strong> ${{ product.price }}</p>
            </div>
            <div class="side-panel">
                <div class="price-info">
                    <h2>Price: ${{ product.price }}</h2>
                    <p>Free delivery every Sunday</p>
                </div>

                <div class="quantity">
                    <label for="quantity">Quantity:</label>
                    <input type="number"
                           id="quantity"
                           v-model="quantity"
                           :max="product.amount"
                           min="1"
                           :disabled="product.amount === 0" />
                    <p v-if="quantity > product.amount" style="color:red;">Not enough stock available</p>
                </div>

                <div class="buttons">
                        <button @click="addToCart(product)" :disabled="quantity > product.amount">Add to Cart</button>
                        <button @click="buyNow(product)" :disabled="quantity > product.amount">Buy Now</button>
                </div>
            </div>
        </div>
    </div>
    <div v-else>
        <p>Loading product data...</p>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { useRoute } from "vue-router";

    interface Product {
        id: number;
        name: string;
        description: string;
        price: number;
        amount: number;
        ImageUrl: string | null;
    }

    export default defineComponent({
        name: "ProductDetail",
        data() {
            return {
                product: null as Product | null,
                quantity: 1,
            };
        },
        mounted() {
            const route = useRoute();
            const productId = Number(route.params.id);
            this.fetchProduct(productId);
        },
        methods: {
            async fetchProduct(id: number): Promise<void> {
                try {
                    const response = await fetch(`https://localhost:7112/api/Product/${id}`);
                    if (response.ok) {
                        const productData = await response.json();
                        this.product = productData;
                    } else {
                        console.error("Failed to fetch product");
                    }
                } catch (error) {
                    console.error("Error fetching product", error);
                }
            },
            addToCart(product: Product) {
                if (typeof window !== "undefined" && window.localStorage) {
                    let cart = JSON.parse(localStorage.getItem("cart") || "[]");
                    const productIndex = cart.findIndex(i => i.id == product.id);

                    if (productIndex !== -1) {
                        cart[productIndex].quantity += this.quantity;
                    } else {
                        product.quantity = this.quantity;
                        cart.push(product);
                    }

                    localStorage.setItem("cart", JSON.stringify(cart));
                    console.log(`${product.name} added to cart`);
                    this.$router.push('/cart');
                }
            },
            buyNow(product: Product) {
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

    .product-detail {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
        padding: 20px;
        background-color: #f4f4f4;
        min-height: 100vh;
    }

    .product-container {
        display: flex;
        justify-content: space-between;
        align-items: flex-start;
        width: 100%;
        max-width: 1200px;
        background-color: #fff;
        border-radius: 10px;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        padding: 20px;
        margin-top: 20px;
    }

    .image-container {
        flex: 1;
        max-width: 400px;
        margin-right: 20px;
    }

        .image-container img {
            width: 100%;
            height: auto;
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

    .price-info h2 {
        font-size: 1.5rem;
        color: #333;
    }

    .price-info p {
        color: #666;
        font-size: 1rem;
    }

    .quantity {
        margin-top: 20px;
        display: flex;
        flex-direction: column;
    }

        .quantity label {
            font-size: 1rem;
            color: #333;
        }

        .quantity input {
            margin-top: 5px;
            padding: 8px;
            font-size: 1rem;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

    .buttons {
        margin-top: 20px;
        display: flex;
        flex-direction: column;
    }

        .buttons button {
            padding: 12px;
            font-size: 1rem;
            background-color: #007BFF;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            margin-bottom: 10px;
        }

            .buttons button:hover {
                background-color: #0056b3;
            }
</style>
