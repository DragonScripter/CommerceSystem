--Commerce System <br>
--A project to mimic real world purchases and transactions. It is made using ASP.NET CORE, and Vue.ts. <br>

<br>--Finished Features
   <br>-- User Authentication(100%)- Secure login, registration, JWT-based authentication.
   <br>-- Shopping Cart Functionality(100%)- Add or remove products, update quantities, cart persist state.
   <br>-- Payment stimulation(100%)- Stimulated transactions, Order placement, and payment confirmation.
   
<br>--Additional Features(To be implemented)
     <br>-- Product Management- Crud for products.
     <br>-- Administrator panel(30%) - Dashboard for user, product and orders management.

--Architecture Diagram


![Architecture Diagram](Image/Ecommerce.drawio.png)
     
<br>--Tech-Used
   <br>--Frontend:
      <br>--Vue.TS: Building user interface with vue.ts
      <br>--Scoped styles: Component styles
   <br>--Backend:
      <br>--ASP.NET WEB CORE API: For building API
      <br>--ClassLibrary: For organizing reusable code for sharing throughout the system.(data models, validation e.t.c)
      <br>--Migrations: migrations for database schema and versioning.
      <br>--Tester class: A class used during development to test core features.
   <br>--Database:
      <br>--Sql Server: Relational database for storing product, user, and order data.
   <br>--Authentication:
      <br>--JWT Authentication: For securing APIs.
   <br>--Other:
      <br>--Entity framework core: ORM and dat access.
      <br>--Restful API: Communication between frontend and backend.
<br>--API Endpoints
      <br>--User Authentication:
         <br>--POST /api/Product/login: Logs a user
         <br>--Post /api/Product/register: Registers a user
      <br>--Shopping cart/ Orders:
         <br>--POST /api/Product/place: Places an order
         <br>--Get /api/Product/order/${id}: Get all orders for a user
      <br>--Products:
         <br>--Get /api/Product: Gets all orders
         <br>--Get /api/Product/${id}: Gets a specific product
         <br>--Post /api/Product/UploadImage: Uploads an image for a product

<br>--DEMO

      
