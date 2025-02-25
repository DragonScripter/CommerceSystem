<template>
    <div class="product-detail" v-if="product">
        <img :src="'https://localhost:7112/images/' + product.id + '.jpg'" alt="Product Image" />
        <h1>{{ product.name }}</h1>
        <p>{{ product.description }}</p>
        <p><strong> Price:</strong> ${{ product.price }}</p>
    </div>
    <div v-else>
        <p>Loading...</p>
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
    }

    export default defineComponent({
        name: "ProductDetail",
        data() {
            return {
                product: null as Product | null,
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
                        const productData: Product = await response.json(); 
                        this.product = productData;
                    } else {
                        console.error("Failed to fetch product");
                       
                    }
                } catch (error) {
                    console.error("Error fetching product", error);
                    
                }
            },
        },
    });
</script>

<style scoped>
    .product-detail img {
        width: 200px;
        height: 200px;
        object-fit: cover;
    }
</style>
