🛍️ MyShop – Auth + E-Commerce Microservice Demo

A dual-project .NET 8 solution demonstrating JWT-based authentication and microservice communication between two ASP.NET Core MVC applications.

🔐 AuthApp – Authentication Microservice

AuthApp handles user authentication and issues JWT tokens.
Provides a Bootstrap-styled login page with unified site branding.
Generates and signs JWTs upon successful login.
Configuration for JWT keys, issuer, audience, and return URLs lives in appsettings.json.
After successful authentication, it redirects the user back to EcomApp with the token attached to the URL.

🛒 EcomApp – E-Commerce Client

EcomApp consumes and validates JWTs issued by AuthApp to manage secure user sessions.
Validates received JWTs using the same secret key and configuration.
Stores valid tokens in cookies and sets the authenticated user context.
Includes a simple storefront (Index) and a protected checkout page (Checkout).
Shares a consistent UI theme (logo, colors, and navigation) with AuthApp.

🔄 Authentication Flow

User visits EcomApp and clicks Login.
The app redirects to the AuthApp login page.
The user logs in → AuthApp generates a signed JWT.
The user is redirected back to EcomApp with the JWT in the query string.
EcomApp validates the token, stores it, and grants access to protected routes like /Home/Checkout.
This demonstrates a real-world microservice-based authentication flow, where one service issues secure tokens and another validates them independently.

🧭 Folder Structure

MySolution/
 
 ├─ AuthApp/
 │   ├─ Controllers/
 │   ├─ Views/
 │   ├─ wwwroot/
 │   ├─ appsettings.json
 │   └─ Program.cs
 
 └─ EcomApp/
     ├─ Controllers/
     ├─ Views/
     ├─ wwwroot/
     ├─ appsettings.json
     └─ Program.cs

⚙️ Key Features

✅ JWT-based authentication flow (AuthApp → EcomApp)
🔁 Cross-app token validation with shared secret key
🎨 Unified Bootstrap 5 UI and branding
🧩 Modular microservice setup with independent ports
🔒 Demo credentials: user@example.com
 / Pass123!

🧠 Concepts Demonstrated

ASP.NET Core MVC fundamentals
JWT creation and validation
Cross-application communication
Claims-based identity handling
Microservice architecture pattern
Bootstrap integration for responsive design

📦 Tech Stack

.NET 8 / ASP.NET Core MVC
Bootstrap 5
JWT (System.IdentityModel.Tokens.Jwt)
C#

👨‍💻 Author - Shivam Singh

A minimal, educational project demonstrating secure authentication and communication between microservices in ASP.NET Core.
