<template>
    <h1>Admin Dashboard - Product Management</h1>

    <div>
        <h1>Admin Panel</h1>
        <h2>Manage Existing Products</h2>
        <div class="container">
        <div v-for="product in products" :key="product.id" class="product-item">
            <img :src="product.imageUrl" alt="Product Image" />
            <h3>{{ product.name }}</h3>
            <p>{{ product.description }}</p>
            <p>{{ product.price }}$</p>
            <div class="b-container">
                <button @click="editProduct(product.id)">Edit</button>
                <button @click="deleteProduct(product.id)">Delete</button>
                <label class="upload-btn">
                    Upload Image
                    <input type="file" @change="uploadImage($event, product.id)" hidden />
                </label>
            </div>
        </div>
        </div>

        <h2>Add New Product</h2>
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
        color: black;
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
    }
    .b-container {
        display: flex; 
        gap: 8px; 
        margin-top: 8px;
    }

    .edit-btn, .delete-btn {
        padding: 8px 12px;
        font-size: 0.9rem;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        color: #fff;
    }
    .upload-btn {
        margin: 8px 4px;
        padding: 8px 12px;
        font-size: 0.9rem;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        background-color: #42b983;
        color: #fff;
        display: inline-block;
    }

        .upload-btn input[type="file"] {
            display: none;
        }

</style>
