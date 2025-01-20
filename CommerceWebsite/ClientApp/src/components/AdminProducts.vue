<template>
    <h1>Admin Dashboard - Product Management</h1>

    <div>
        <h1>Admin Panel</h1>
        <h2>Manage Existing Products</h2>
        <div class="container">
            <div v-for="product in products" :key="product.id" class="product-item">
                <img :src="'https://localhost:7112/images/' + product.id + '.jpg'" alt="Product Image" />


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
        description: string; // Fixed typo here
        price: number;
        imageUrl: string;
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
            async uploadImage(event: Event, productId: number): Promise<void> {
                const target = event.target as HTMLInputElement;
                const file = target.files?.[0];
                if (!file) return;

                try {
                    if (!file.type.startsWith("image/")) {
                        alert("Please upload a valid image file.");
                        return;
                    }
                    if (file.size > 5 * 1024 * 1024) {
                        alert("File size must not exceed 5MB.");
                        return;
                    }

                    const formData = new FormData();
                    formData.append("image", file);
                    formData.append("productId", productId.toString());

                    const response = await fetch(`https://localhost:7112/api/Product/${productId}/UploadImage`, {
                        method: "POST",
                        body: formData,
                    });

                    if (response.ok) {
                        console.log("Image uploaded successfully!");
                        await this.fetchProducts();
                       
                        const product = this.products.find((product) => product.id === productId);
                        if (product) {
                            product.imageUrl = `/images/${productId}.jpg`; // Update the image URL
                        }
                        alert("Image uploaded successfully!");
                    } else {
                        console.error("Failed to upload image.");
                        alert("Failed to upload image. Please try again.");
                    }
                } catch (error) {
                    console.error("Error uploading image", error);
                    alert("An error occurred while uploading the image.");
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
