# Retail Management Backend Project

## Table of Contents

- [Overview](#prerequisites)
- [Technologies Used](#technologies-used)
- [Features](#features)
- [Installation and Setup](#installation-and-setup)
- [Project Layout](#project-layout)
- [Postman Setup for Testing](#postman-setup-for-testing)

## Overview
A simple backend system for managing retail purchases, built with **.NET 7** and **MSSQL**.

## Technologies used
- ASP.NET Core (.NET 7)
- Entity Framework Core
- Microsoft SQL Server
- HttpClient for API Requests
- Postman for testing

## Features
This project implements a RESTful API for managing a retail store, allowing the store owner to manage products, customers, and purchases. Key features include:

- **Product Management**: Add, update, and delete products in the store.
- **Customer Management**: Register, edit, and remove customers.
- **Purchase Management**: Create purchases for customers with multiple products and quantities.
- **Layered Architecture**: Organized into Minimal API, Service Layer, Repository Layer, and Database Context.
- **Unit of Work & Repository Pattern**: Ensures efficient database operations by grouping related changes into transactions.
- **Entity Framework Core**: Used for database interaction with MSSQL.
- **Validation & Error Handling**: Custom exceptions (NotFoundException, ValidationException) with structured API responses.
- **Postman Setup for Testing**: Includes automated pre-request & post-response scripts for test scenarios.

## Installation and Setup

### 1. Clone the repository:   
```bash
git clone https://github.com/mouoent/RetailManagement-be.git
cd RetailManagement-be/RetailManagement-be
```
### 2. Restore NuGet packages:
```bash
dotnet restore
```
### 3. Configure Environment Variables
#### 1. Keep `appsettings.json` as a template (committed, without secrets).
#### 2. Create `appsettings.Development.json` (ignored by Git, containing real credentials).
#### 3. Ensure `appsettings.Development.json` is added to `.gitignore`.

#### Example `appsettings.json` (Committed template):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "placeholder_connection_string"
  } 
}
```
#### Example `appsettings.Development.json` (Ignored in Git, contains real credentials):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
  }
}
```
#### 4. Run Database Migrations
```bash
dotnet ef database update
```
**Optional**: If you want to create your own migrations to update the database schema, make sure to specify the location of the Migrations folder:
```bash
dotnet ef migrations add <MigrationName> --output-dir Persistence/Migrations
```

#### 5. Run the Project
```bash
dotnet run
```
The API will be available at `https://localhost:7007/` and its documentation at `https://localhost:7007/swagger`

## Project Layout

	RetailManagement-be/
	│
	│── Endpoints/                 
	│   ├── CustomersEndpoints.cs
	│   ├── ProductsEndpoints.cs
	│   └── PurchasesEndpoints.cs
	│
	│── Models/                 
	│   ├── Entities
	│   └── DTOs/
	│	├── Customer/
	│	├── Product/
	│	└── Purchase/
	│
	│── Services/                   
	│   ├── CustomerService.cs
	│   ├── ProductService.cs
	│   └── PurchaseService.cs
	│		
	│── Interfaces/                 
	│   ├── ICustomerService.cs
	│   ├── IProductService.cs
	│   ├── IPurchaseService.cs
	│   └── IUnitOfWork.cs
	│
	│── Persistence/                
	│   ├── RetailManagementDbContext.cs
	│   ├── UnitOfWork.cs
	│   ├── Interfaces/	
	│   ├── Migrations/	
	│   └── Repositories/               
	│	├── BaseRepository.cs       
	│	├── CustomerRepository.cs
	│	├── ProductRepository.cs
	│	├── PurchaseRepository.cs
	│	└── PurchaseProductRepository.cs
	│
	│── Middlewares/                     
	│   └── ExceptionHandlingMiddleware.cs	
	│
	│── Shared/                     
	│   └── Exceptions/	
	│
	│── Postman/                    
	│   ├── RetailManagement.postman_collection.json
	│   └── RetailManagement.postman_environment.json
	│
	│── Program.cs                  
	│── appsettings.json            
	└── README.md                   


### Key Design Patterns

This project follows several industry-standard design principles: 

- #### Minimal API Design

	- The application uses Minimal APIs (`app.MapGet`, `app.MapPost`, etc.) for simplicity and performance.
	
- #### Service Layer
	- Encapsulates business logic.
	- Prevents direct database access from endpoints.

- #### Repository Pattern
	- Abstracts database operations and centralizes data access logic.

- #### Unit of Work Pattern
	- Groups multiple database operations into a single transaction.
	- Improves performance by reducing `SaveChanges()` calls.

- #### Exception Handlind Middleware
	- Centralized handling of validation errors, not found exceptions, and server errors.

### Why This Structure?
- **Scalability**: New features can be added without modifying existing layers.
- **Maintainability**: Code is modular, reusable, and testable.
- **Efficiency**: Reduces redundant database queries by leveraging UoW + Repository.

## Postman Setup for Testing
### Importing the Postman Collection
To simplify API testing, both the Postman collection and environment are provided.
1. #### Download the files
	- **Collection**: `RetailManagement.postman_collection.json`  
	- **Ennvironment**: `RetailManagement.postman_environment.json`
2. #### Import into Postman
	- Open Postman
	- Click on File > Import
	- Select both the **collection** and **environment** files and import them.
3. #### Select the Environment
	- Go to the **Environments** tab in Postman.
	- Select **RetailManagement** as the active environment.
	- This will automatically load the necessary variables such as:
		- `url`: `http://localhost:7007`
		- `productsEndpoint`: products
		- `customersEndpoint`: customers
		- `purchasesEndpoint`: purchases
### Running Tests
This collection includes **Pre-request** and **Post-response** scripts to dynamically generate test data and validate responses.
- #### Pre-request Scripts:
	- Automatically fetches existing products/customers before executing `POST /purchases`.
	- Ensures valid `ProductIds` and `CustomerId` for testing
- #### Post-request Scripts:
	- Validates that the created/updated entity is properly stored in the database.
	- For `POST /purchases`, an additional GET request is sent to confirm the purchase exists.

To run the tests:
1. Open **Postman**.
2. Select the **RetailManagement** collection.
3. Click `Run Collection` to execute all tests.

### Modifying Test Data
By default, test products/customers are prefixed with `"TestProduct-"` and `"TestCustomer-"`.
To customize, edit the **Collection variables** `testProductPrefix` and `testCustomerPrefix`.