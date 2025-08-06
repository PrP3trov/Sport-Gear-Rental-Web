# SportGearRental 🏔️🎿

**SportGearRental** is a web-based ASP.NET Core MVC application that allows users to browse, search, and rent sports equipment online. Designed for both outdoor enthusiasts and rental providers, the platform supports user and administrator roles, responsive UI, and secure user interactions.

## 📌 Project Overview

This project is developed as part of the **ASP.NET Advanced – June 2025** course at SoftUni. It follows all mandatory requirements including user roles, Razor-based UI, Entity Framework Core, Identity system integration, and a modular MVC architecture.

## 🌟 Features

- 👤 **User and Admin roles**
  - Users can browse and rent equipment
  - Admins can manage gear, categories, brands, and more

- 🧾 **Gear Listings**
  - Filter by category, brand, or keyword
  - Card-style layout with image, price/day, availability, and remaining days

- 📅 **Rental Management**
  - View personal rentals
  - Automatic calculation of rental duration and cost

- 🔐 **Authentication and Authorization**
  - ASP.NET Core Identity with role-based access

- ⚙️ **Admin Panel (Area)**
  - CRUD operations for Brands, Categories, Equipment
  - Separate layouts using MVC Areas

- 🔎 **Search & Filtering**
  - By name, category, and brand

- 🌐 **Responsive Design**
  - Bootstrap 5 layout optimized for desktop and mobile

- 🧪 **Unit Testing**
  - >65% code coverage on business logic
  - Tests for services and controllers using mocking

- 📂 **Seed Data**
  - Pre-loaded categories, brands, and gear items

- 🛡️ **Security**
  - SQL injection prevention
  - AntiForgery tokens
  - Input validation on client & server

- ❌ Custom Error Pages
  - 404 Not Found
  - 500 Internal Server Error

## 🧰 Tech Stack

- **ASP.NET Core 8.0**
- **Razor Views & Layouts**
- **Entity Framework Core**
- **SQL Server**
- **Bootstrap 5**
- **XUnit / Moq (Unit Testing)**

## 🧱 Architecture

- Clean MVC with service layer abstraction
- Dependency Injection throughout
- Separation by Areas for Admin features
- AutoMapper (optional use)

## 🚀 Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/SportGearRental.git
   cd SportGearRental
   ```

2. **Set up database**
   - Update `appsettings.json` with your local SQL Server connection string.
   - Run migrations:
     ```bash
     dotnet ef database update
     ```

3. **Run the app**
   ```bash
   dotnet run
   ```

4. **Seeded Admin Credentials**
   ```
   Email: admin@sportgear.bg
   Password: Admin@123
   ```

## 🧠 Future Improvements

- Implement SignalR for live rental updates
- Switch front-end to React or Blazor
- Host on Azure App Service
- Add user reviews and ratings

## 👨‍💻 Author

Created by Preslav Petrov as part of the ASP.NET Advanced @ SoftUni - June 2025.

---

© 2025 SportGearRental - All Rights Reserved.
