<template>
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
        ImageUrl: string | null;
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
                        const productData = await response.json();
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
</style>
