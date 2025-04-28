<template>
    <h1>Admin Dashboard - Product Management</h1>

    <div>
        <h1>Admin Panel</h1>
        <h2>Manage Existing Products</h2>
        <h2>Add New Product</h2>
        <div class="button-container">
            <button @click="showAddProductModal = true">Add</button>
        </div>
        <div v-if="showAddProductModal" class="modal">
            <div class="modal-content">
                <h3>Add New Product</h3>
                <form @submit.prevent="addProduct">
                    <label for="name">Product Name</label>
                    <input type="text" id="name" v-model="newProduct.name" />

                    <label for="description">Description</label>
                    <input type="text" id="description" v-model="newProduct.description" />

                    <label for="price">Amount</label>
                    <input type="number" id="amount" v-model="newProduct.amount" />

                    <label for="price">Price</label>
                    <input type="number" id="price" v-model="newProduct.price" />

                    <label for="category">Category</label>
                    <select id="category" v-model="newProduct.category">
                        <option disabled value="">Please select one</option>
                        <option value="Electronics">Electronics</option>
                        <option value="Clothing">Clothing</option>
                        <option value="Toys">Toys</option>
                        <option value="Home & Kitchen">Home & Kitchen</option>
                    </select>

                    <button type="submit">Add Product</button>
                    <button type="button" @click="showAddProductModal = false">Cancel</button>
                </form>
            </div>
        </div>
        <div v-if="showEditProductModal" class="modal">
            <div class="modal-content">
                <h3>Edit Product</h3>
                <form @submit.prevent="saveEditedProduct">
                    <label for="edit-name">Product Name</label>
                    <input type="text" id="edit-name" v-model="editProductData.name" />

                    <label for="edit-description">Description</label>
                    <input type="text" id="edit-description" v-model="editProductData.description" />

                    <label for="edit-price">Price</label>
                    <input type="number" id="edit-price" v-model="editProductData.price" />
                    <label>Category</label>
                    <select v-model="editProductData.category">
                        <option disabled value="">Please select one</option>
                        <option value="Electronics">Electronics</option>
                        <option value="Clothing">Clothing</option>
                        <option value="Toys">Home & Kitchen</option>
                    </select>


                    <button type="submit">Save Changes</button>
                    <button type="button" @click="showEditProductModal = false">Cancel</button>
                </form>
            </div>
        </div>


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
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    interface Product {
        id: number;
        name: string;
        description: string; 
        price: number;
        imageUrl: string;
        category: string,
    }

    export default defineComponent({
        name: "ProductPage",
        data() {
            return {
                products: [] as Product[],
                showAddProductModal: false,
                showEditProductModal: false,
                newProduct: {
                    name: "",
                    description: "",
                    amount: 0,
                    price: 0,
                },
                editProductData: null as Product | null,
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
            openAddProductModal() {
                this.showAddProductModal = true; 
            },
            closeAddProductModal() {
                this.showAddProductModal = false; 
                this.newProduct = { name: "", description: "", amount:0, price: 0 }; 
            },
            async editProduct(productId: number): Promise<void> {
                const product = this.products.find(p => p.id === productId);
                if (product) {
                   
                    this.editProductData = { ...product };
                    this.showEditProductModal = true;
                }
            },

            async saveEditedProduct(): Promise<void> {
                if (!this.editProductData) return;

                try {
                    const response = await fetch(`https://localhost:7112/api/Product/${this.editProductData.id}`, {
                        method: "PUT", 
                        headers: {
                            "Content-Type": "application/json",
                        },
                        body: JSON.stringify(this.editProductData),
                    });

                    if (response.ok) {
                        alert("Product updated successfully!");
                        this.showEditProductModal = false;
                        this.fetchProducts(); 
                    } else {
                        console.error("Failed to update product.");
                        alert("Failed to update product.");
                    }
                } catch (error) {
                    console.error("Error updating product", error);
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
    .product-item img {
        width: 50px; 
        height: 50px; 
        object-fit: cover; 
    }
    .button-container {
        margin-bottom: 20px;
    }

    button {
        background-color: #4CAF50; /* Green */
        color: white;
        border: none;
        padding: 10px 20px;
        cursor: pointer;
        border-radius: 5px;
        font-size: 16px;
    }

        button:hover {
            background-color: #45a049;
        }

    .modal {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: rgba(0, 0, 0, 0.5);
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .modal {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: rgba(0, 0, 0, 0.5);
        display: flex;
        justify-content: center;
        align-items: center;
        z-index: 9999;
    }

    .modal-content {
        background-color: white;
        padding: 20px;
        border-radius: 8px;
        width: 80%; 
        max-width: 500px;
        display: flex;
        flex-direction: column;
        gap: 20px; 
    }

       
        .modal-content h3 {
            margin: 0 0 15px;
        }

    
        .modal-content form {
            display: flex;
            flex-direction: column;
            gap: 10px; 
        }

        
        .modal-content .modal-buttons {
            display: flex;
            justify-content: flex-end;
            gap: 10px; 
        }

    .modal-close {
        position: absolute;
        top: 10px;
        right: 10px;
        background: transparent;
        border: none;
        font-size: 20px;
        cursor: pointer;
    }


</style>
