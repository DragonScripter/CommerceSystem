<template>
    <div>
        <h1>Product List</h1>
        <div class="container">
            <div class="products">
                <div v-for="product in products" :key="product.id" class="product-item">
                    <img :src="'https://localhost:7112/images/' + product.id + '.jpg'" alt="Product Image" />
                    <h2>{{ product.name }}</h2>
                    <p>{{ product.description }}</p>
                    <p><strong>Price:</strong> ${{ product.price }}</p>
                </div>
            </div>
        </div>
       </div>
</template>


<script lang="ts">
    import { defineComponent } from "vue";

    interface Product {
        id: number;
        name: string;
        descrption: string;
        price: number;
        
    }

    export default defineComponent({
        name: "ProductPage",
        data() {
            return {
                products: [] as Product[],
            };
        },
        mounted() {
            this.fetchProducts();
        },
        methods: {
            async fetchProducts(): Promise<void> {
                try {
                    const response = await fetch("https://localhost:7112/api/Product");
                    if (response.ok) {
                        this.products = await response.json();
                    } else {
                        console.error("Failed to fetch products");
                    }
                } catch (error) {
                    console.error("Error fetching data", error);
                }
            },
        },
    });
</script>

<style scoped>
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
    }

    .product-item {
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

        .product-item:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 12px rgba(0, 0, 0, 0.15);
        }

    .product-item img {
        width: 50px;
        height: 50px;
        object-fit: cover;
    }
    .products {
        display: flex;
        flex-wrap: wrap;
        gap: 20px;
        justify-content: space-evenly;
    }
</style>
