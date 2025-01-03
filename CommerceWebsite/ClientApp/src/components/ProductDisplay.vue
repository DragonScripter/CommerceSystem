<template>
    <div>
        <h1>Product List</h1>
        <div class="products">
            <div v-for="product in products"
                 :key="product.id"
                 class="p-card">
                <img :src="product.imageUrl" alt="Product Image" />
                <h2>{{ product.name }}</h2>
                <p>{{ product.description }}</p>
                <p><strong>Price:</strong> ${{ product.price }}</p>
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

</style>
