# BookStore Management System

A full-stack e-commerce web application built with ASP.NET Web Forms and Entity Framework, featuring user authentication, shopping cart, wishlist, order management, and admin dashboard.

## Overview

This project demonstrates a complete end-to-end web application with clean separation of concerns, secure authentication, practical database modeling, and role-based access control. It covers real-world e-commerce workflows including user registration, product browsing, cart management, wishlist functionality, and order processing.
## 🔐 Authentication Enhancements

### JWT-Based Authentication
- Implemented **JWT (JSON Web Token)** authentication without OWIN
- JWT is generated on successful login
- Token is stored securely in an **HttpOnly cookie**
- JWT contains:
  - UserId
  - Email
  - Role (User / Admin)
- Token validation is handled centrally using a custom `BasePage`
- Role-based authorization is enforced using JWT claims (Admin/User)

---

## 🔄 Order Placement Improvements

### Transaction-Safe Order Processing
- Order placement is wrapped inside a **database transaction**
- The following operations execute atomically:
  - Order creation
  - Order items insertion
  - Cart cleanup
- If any step fails, the transaction is **rolled back**
- Ensures data consistency and prevents partial or corrupted orders

---

## ✅ Validation Enhancements

### Input Validation
- Numeric fields (Price, Quantity) are validated using `TryParse`
- Prevents runtime conversion errors
- Empty cart validation before placing orders
- Defensive null checks applied across pages

---

## 🚨 Exception Handling

### Robust Error Handling
- Critical operations wrapped inside `try-catch` blocks
- Safe transaction rollback on failure
- Application-level exceptions thrown where required
- Prevents application crashes during unexpected failures

---

## 🧑‍💻 Registration Improvements

### Duplicate User Validation
- Duplicate email validation implemented during registration
- Prevents multiple accounts using the same email
- User-friendly error message shown on duplicate entry

---

## 🔒 Security Improvements

- Passwords are hashed using **PBKDF2 (Rfc2898DeriveBytes)**
- Salt generated per user
- Passwords are never stored in plain text
- JWT cookies marked as **HttpOnly**

## Features

### User Features
- User registration and login with secure password hashing
- Browse and view all available books
- Search and filter books
- Add/remove books from shopping cart
- Manage wishlist (add/remove items)
- Place orders directly from cart
- View complete order history with order details
- Session-based authentication and authorization
- Secure logout with session cleanup

### Shopping Cart & Orders
- Quantity-based cart system with item management
- Real-time total and grand total calculation
- Single-click order creation from cart
- Multiple order items per order support
- Order confirmation page
- Automatic cart clearing after successful order
- Order history with detailed order items view

### Admin Features
- Dedicated admin-only dashboard
- Add new books to inventory
- Edit existing book details (title, price, description, stock)
- Delete books from catalog
- Role-based UI (admin panel hidden for regular users)
- Admin-only page access with role verification

## Tech Stack

| Component | Technology |
|-----------|-----------|
| Frontend | ASP.NET Web Forms |
| Styling | Bootstrap 5 |
| Backend | C# (.NET Framework 4.7.2) |
| ORM | Entity Framework 6 (Code First) |
| Database | SQL Server Express |
| Authentication | Session-based |
| Password Security | PBKDF2 (Rfc2898DeriveBytes) |

## Project Structure

```
BookStoreManagementSystem/
├── Admin/
│   └── Books.aspx              # Admin book management
│
├── App_Data/                   # Local database
├── App_Start/                  # Application startup config
├── Content/                    # CSS and Bootstrap files
├── Scripts/                    # JavaScript files
│
├── Data/
│   └── BookStoreContext.cs     # Entity Framework DbContext
│
├── Helpers/
│   └── PasswordHelper.cs       # Password hashing and verification
│
├── Models/
│   ├── User.cs                 # User entity
│   ├── Book.cs                 # Book entity
│   ├── Cart.cs                 # Cart entity
│   ├── Wishlist.cs             # Wishlist entity
│   ├── Order.cs                # Order entity
│   └── OrderItem.cs            # OrderItem entity
│
├── Pages/
│   ├── Books.aspx              # Browse books page
│   ├── Cart.aspx               # Shopping cart page
│   ├── Dashboard.aspx          # User dashboard
│   ├── Login.aspx              # Login page
│   ├── Register.aspx           # Registration page
│   ├── Logout.aspx             # Logout handler
│   ├── MyWishlist.aspx         # Wishlist page
│   ├── Orders.aspx             # Order history page
│   └── OrderSuccess.aspx       # Order confirmation page
│
├── Site.Master                 # Master layout template
├── Web.config                  # Configuration and connection strings
└── packages.config             # NuGet package references
```

## Database Design

### Tables

**Users**
- UserId (Primary Key)
- Username
- Email
- PasswordHash
- Role (User/Admin)
- CreatedDate

**Books**
- BookId (Primary Key)
- Title
- Author
- Description
- Price
- Stock
- CreatedDate

**Carts**
- CartId (Primary Key)
- UserId (Foreign Key)
- BookId (Foreign Key)
- Quantity
- AddedDate

**Wishlists**
- WishlistId (Primary Key)
- UserId (Foreign Key)
- BookId (Foreign Key)
- AddedDate

**Orders**
- OrderId (Primary Key)
- UserId (Foreign Key)
- OrderDate
- TotalAmount
- Status

**OrderItems**
- OrderItemId (Primary Key)
- OrderId (Foreign Key)
- BookId (Foreign Key)
- Quantity
- Price

### Relationships
- One User → Many Orders
- One User → Many Cart Items
- One User → Many Wishlist Items
- One Order → Many OrderItems
- One Book → Many Cart Items
- One Book → Many Wishlist Items
- One Book → Many OrderItems

## Security Implementation

### Password Security
- Passwords are never stored in plain text
- PBKDF2 hashing with salt using Rfc2898DeriveBytes
- Secure password verification during login
- Salt generated for each password

### Authentication & Authorization
- Session-based user authentication
- Session tokens stored securely
- Role-based access control (User/Admin)
- Admin pages check user role before loading
- Logout clears session data
- Secure session management

### Best Practices
- Input validation on all forms
- Parameterized queries to prevent SQL injection
- Secure session handling
- Password hashing on registration and login

## User Workflows

### Registration Flow
1. User navigates to Register page
2. Enters username, email, and password
3. Password is hashed with PBKDF2
4. User record created in database
5. Redirected to login page

### Login Flow
1. User enters credentials
2. System retrieves user from database
3. Password verified against stored hash
4. Session created for authenticated user
5. User redirected to dashboard

### Shopping Flow
1. User browses books on Books page
2. Selects quantity and adds to cart
3. Can view/modify cart anytime
4. Proceeds to checkout
5. Order created from cart items
6. Cart automatically cleared
7. User receives order confirmation

### Order History
1. User navigates to Orders page
2. Views all previous orders with dates and amounts
3. Can see detailed items for each order
4. Order information persists in database
