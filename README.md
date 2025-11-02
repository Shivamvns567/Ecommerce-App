🛍️ MyShop – Auth + E-Commerce Microservice Demo

A dual-project .NET 8 solution showcasing modern JWT authentication and microservice integration.

This repository contains two lightweight, ready-to-run ASP.NET Core MVC applications:

🔐 AuthApp – Authentication Microservice

Handles user login with JWT issuance.

Includes a Bootstrap-styled login page with unified branding.

Configurable JWT settings and client redirect URLs in appsettings.json.

🛒 EcomApp – E-Commerce Client

Consumes JWTs issued by AuthApp.

Verifies tokens and manages authenticated sessions.

Displays a simple storefront and checkout page.

Unified UI theme shared with AuthApp (logo, colors, navigation).

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

✅ JWT-based Authentication Flow (AuthApp → EcomApp)

🔁 Cross-App Token Validation with shared secret key

🎨 Consistent Bootstrap UI & Branding

🧩 Modular Microservice Setup – independent ports, seamless linking

🔒 Demo Credentials: user@example.com / Pass123!

🚀 How to Run

Start AuthApp

cd AuthApp
dotnet run
Runs on https://localhost:5001


Start EcomApp

cd EcomApp
dotnet run
Runs on https://localhost:5002


Test the Flow

Visit: https://localhost:5002

Click Login → Redirects to AuthApp

Sign in with demo credentials

Redirects back to EcomApp (authenticated session active)

Access /Home/Checkout to see a protected page

🧠 Concepts Demonstrated

ASP.NET Core MVC

JWT Authentication & Validation

Cross-App Communication

Role-based claims foundation

Bootstrap 5 integration

Microservice architecture principles

🖼️ Screenshots (Optional)

Add screenshots of login and home page here for better presentation.

💡 Future Enhancements

🔄 Refresh tokens

🚪 Token invalidation on logout

👥 Role-based authorization

🔗 API-based user info sharing

⚙️ Centralized configuration service

📦 Tech Stack

.NET 8 / ASP.NET Core MVC

Bootstrap 5

JWT (System.IdentityModel.Tokens.Jwt)

C#

👨‍💻 Author

Shivam Singh
A minimal, educational project demonstrating real-world authentication flow between microservices in ASP.NET Core.
