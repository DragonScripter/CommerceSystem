<template>
    <div>
        <h1>Product List</h1>
        <ul>
            <li v-for="product in products" :key="product.id">{{ product.name }}</li>
        </ul>
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
\

<style scoped>
    h1 {
        color: #42b983;
    }
</style>
