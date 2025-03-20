<template>
    <nav class="navbar">
        <div class="navbar-left">
            <a href="/">
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
            <a href="/orders">Orders</a>
            <a href="/">Account</a>
        </div>
    </nav>
    <div class="container">
        <div class="card">
            <h2>Login</h2>

            <form @submit.prevent="login">
                <div class="input-group">
                    <label for="email">Email</label>
                    <input type="email" id="email" v-model="email" placeholder="Enter your email" required>
                </div>
                <div class="input-group">
                    <label for="password">Password</label>
                    <input type="password" id="password" v-model="password" placeholder="Enter your password" required>
                </div>

                <button type="submit" class="login-btn">Sign In</button>
            </form>
            <p class="register-text">
                Don't have an account? <a href="/sign">Sign up</a>
            </p>
        </div>
    </div>
</template>
<script lang="ts">
    import { defineComponent } from "vue";
    import { useRoute } from "vue-router";

    export default defineComponent({
        name: "Login",
        data() {
            return {
                email: '',
                password: '',
            };
        },
        methods: {
           async login() {
                const loginRequest = {
                    email : this.email,
                    password : this.password,
                };

                try {
                    const response = await fetch(`https://localhost:7112/api/Product/login`, {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json",
                        },
                        body: JSON.stringify(loginRequest),
                    });

                    const result = await response.json();
                    if (response.ok) {
                        localStorage.setItem('token', result.token);
                        const redirect = this.$route.query.redirect || '/';
                        this.$router.push(redirect);
                    }
                    else
                    {
                        alert(result.message); 
                    }
                }
                catch (error) {
                    console.error("Error logging in:", error);
                    alert("An error occurred. Please try again.");
                }
            }

        }
    })
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
    .container {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        background-color: #f3f3f3;
    }
    .card{
        background: white;
        padding: 20px;
        border-radius: 8px;
        box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
        width: 350px;
        text-align: center;
    }
    .input-group {
        text-align: left;
        margin-bottom: 15px;
    }

        .input-group label {
            display: block;
            font-size: 14px;
            font-weight: 600;
            margin-bottom: 5px;
        }

        .input-group input {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 14px;
        }

    .login-btn {
        width: 100%;
        background-color: #0073e6;
        color: white;
        padding: 10px;
        border: none;
        border-radius: 5px;
        font-size: 16px;
        cursor: pointer;
        transition: 0.3s;
    }

        .login-btn:hover {
            background-color: #005bb5;
        }

    .register-text {
        margin-top: 15px;
        font-size: 14px;
    }

        .register-text a {
            color: #0073e6;
            text-decoration: none;
        }

            .register-text a:hover {
                text-decoration: underline;
            }



</style>