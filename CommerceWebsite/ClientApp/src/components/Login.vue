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
            <a href="/">Orders</a>
            <a href="/">Account</a>
        </div>
    </nav>
</template>
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

</style>