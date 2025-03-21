--Commerce System <br>
--A project to mimic real world purchases and transactions. <br>

<br>--Finished Features
   <br>-- User Authentication(100%)- Secure login, registration, JWT-based authentication.
   <br>-- Shopping Cart Functionality(100%)- Add or remove products, update quantities, cart persist state.
   <br>-- Payment stimulation(100%)- Stimulated transactions, Order placement, and payment confirmation.
   
<br>--Additional Features(To be implemented)
     <br>-- Product Management- Crud for products.
     <br>-- Administrator panel(30%) - Dashboard for user, product and orders management.

--Architecture Diagram


![Architecture Diagram](Image/Ecommerce.drawio.png)
     
--Tech-Used
   --Frontend:
      --Vue.TS: Building user interface with vue.ts
      --Scoped styles: Component styles
   --Backend:
      --ASP.NET WEB CORE API: For building API
      --ClassLibrary: For organizing reusable code for sharing throughout the system.(data models, validation e.t.c)
      --Migrations: migrations for database schema and versioning.
      --Tester class: A class used during development to test core features.
   --Database:
      --Sql Server: Relational database for storing product, user, and order data.
   --Authentication:
      --JWT Authentication: For securing APIs.
   --Other:
      --Entity framework core: ORM and dat access.
      --Restful API: Communication between frontend and backend.
      
