


# 1.Project Description

This project is a backend server for a customer, product, and order management system for an online store.

The project description is provided in English and German.

Registration in the system with two access levels: 
- user for customers
- admin for employees.



# 2.Technologies:

ASP.NET Core 8.0 (C#) 
Entity Framework Core 8.0 
MySQL 8.0 
ASP.NET Core Identity Framework
JWT Authentication 
Logging (Serilog)


# 3. Project Structure

ProjectEverythingForHomeOnlineShop
│── Application
│   ├── DTOs        <!-- All dtos for Business logic with validation  -->       
│   ├── Services    <!-- Business logic for API requests -->
│   │   ├── Implementations
│   │   ├── IAuthService.cs
│   │   ├── ICustomerService.cs
│   │   ├── IOrderService.cs
│   │   ├── IProductService.cs
│── Controllers
│   ├── AuthController.cs
│   ├── CustomerController.cs
│   ├── OrderController.cs
│   ├── ProductController.cs
│── Core
│   ├── Models                               
│   │   ├── Customer.cs
│   │   ├── Product.cs
│   │   ├── ShopOrder.cs
│   │   ├── ShopOrderProduct.cs
│   │   ├── ShopOrderStatus.cs
│── DataAccess
│   ├── Migrations
│   ├── Persistence
│   │   ├── Identity        
│   │   │   ├── ApplicationUser.cs      <!-- class for enteties in Identety users table -->
│   │   ├── DatabaseInitializer.cs      <!-- creating database if it does not exist, making migration by starting app -->
│   │   ├── OnlineShopMySQLDatabaseContext.cs   <!-- database context with all settings for database -->
│   ├── Repositories
│   │   ├── Implementations
│   │   ├── ICustomerRepository.cs
│   │   ├── IOrderRepository.cs
│   │   ├── IProductRepository.cs
│   ├── JwtAuthentication
│   │   ├── JwtAuthenticationExtensions.cs
│   │   ├── TokenGenerator.cs
│   ├── LoggingConfig.cs
│   ├── ServiceResult.cs    <!-- class for some variants for spacial format responces to frontend -->
│── logs
│── appsettings.json
│── appsettings.Development.json
│── Program.cs
│── ProjectEverythingForHomeOnlineShop.http



# 4.Database Structure

 Table customers
- `CustomerID` – Unique customer ID
- `IdentityUserID`  – Customer's unique ID in ASP.NET Core Identity
- `CustomerEmail` 
- `CustomerLastName`
- `CustomerFirstName` 
- `CustomerCity` 
- `CustomerStreet` 
- `CustomerHausNumber` 
- `CustomerPostIndex` 
- `CustomerPhone` 

 Table products  
- `ProductID` – Unique product ID  
- `ProductSCU`  – Unique product SKU code  
- `ProductName`  
- `Category`  
- `UnitPrice` – Price per unit  
- `StockQuantity` – Available stock quantity

 Table shopOrders  
- `ShopOrderID`
- `CustomerID` 
- `ShopOrderStatusID` 
- `ShopOrderAcceptedAt` 
- `ShopOrderShippedAt`  
- `ShopOrderCompletedAt` 

 Table shopOrderProducts  
- `ShopOrderID` 
- `ProductID`  
- `ProductQuantity` – Quantity of the product in the order

 Table shopOrderStatuses  
- `ShopOrderStatusID` 
- `ShopOrderStatusName`  



# 5.API Endpoints


## 1. Admin Registration
POST /api/Auth/registerAdmin

Access Level: 
Admin Only (Requires authentication & admin role)

Description & Logic: 
Creating a new user with "Admin" access rights for company employees. 
A new administrator can only be registered by an existing one. 
When a new admin is created, a temporary password is generated, 
which the new administrator can change to their own after authentication.

Request Body:
json
{
  "adminName": "adminNew",
  "adminEmail": "adminNew@gmail.com"
}

Response (200 OK):
json
{
    "message": "User registered successfully",
    "adminId": "2bdaf47f-4196-4cbc-b0c0-c96f47b8e065",
    "temporaryPassword": "TemporaryPassword1!"
}

----------------------------------------------------------------------------------------------------------------------------------------------

## 2. Customer Registration
POST /api/Auth/registerCustomer

Access Level: 
Public (No authentication required)

Description & Logic: 

To register, a customer must provide all the required information, including a login and password. 
All customer data is validated for correct formatting.

After verifying that the email does not already exist in the database, a new user is created with a login, password, and email. 
Once the user is successfully created, a new record is added to the customers table containing all the customer's details.

For each new customer, two entities are created:
User – for authentication
Customer – for business processes

Request Body:
json
{
  "userName": "usernew",
  "password": "Hello12345!",
  "customerFirstName": "Marta",
  "customerLastName": "Mustermann",
  "customerStreet": "Musterstraße",
  "customerHausNumber": "12A",
  "customerPostIndex": "1234",
  "customerCity": "Wien",
  "customerEmail": "marta.mustermann@gmail.com",
  "customerPhone": "066455570770"
}

Response (200 OK):
json
{
    "message": "User registered successfully",
    "userId": "140ff0f2-e784-4ddb-a07e-e17a7e9209c1"
}

------------------------------------------------------------------------------------------------------------------------------------------------

## 3. Customer Registration
POST /api/auth/login

Access Level: 
Public (No authentication required)

Description & Logic: 

Used for authentication of both administrators and customers. 
The email and password are validated. 
In response, the user receives a token, token expiration date, and user ID.

Request Body:
json
{
   "userName": "usernew",
    "userPassword": "Hello12345!",
    "userEmail": "marta.mustermann@gmail.com"
}

Response (200 OK):
json
{
    "token": {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9
        .eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNl
        cm5ldyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsY
        WRkcmVzcyI6Im1hcnRhLm11c3Rlcm1hbm5AZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS
        93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImp0aSI6ImU4MmMwMmJmLTQ2ZDktNGU4NS0
        4NmM0LWE3NDUwM2Q5NDM2NyIsImV4cCI6MTczODQ5MTExNywiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAxNCIs
        ImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTQifQ
        ._ibZR7YxDbYOTtvtyN37AC0AlzT3poOHMpgjih5yU7U",
        "expiration": "2025-02-02T10:11:57Z",
        "userID": "140ff0f2-e784-4ddb-a07e-e17a7e9209c1"
    }
}

------------------------------------------------------------------------------------------------------------------------------------------------


## 4. Password Change
POST /api/auth/updatePassword/{userID}

Access Level: 
Admin or Customer (Requires authentication with either admin or customer role)

Description & Logic: 
The server must receive the old and new password from the authenticated user.

Request Body:
json
{
    "currentUserPassword": "TemporaryPassword1!",
    "newUserPassword": "admin2Password1!"
}

Response (200 OK):
json
{
    "message": "Password changed successfully"
}

------------------------------------------------------------------------------------------------------------------------------------------------

## 5. Get Customer By ID
GET /api/Customer/get/{userID}

Access Level: 
Admin or Customer (Requires authentication with either admin or customer role)

Description & Logic: 
Retrieving all customer data. 


Response (200 OK):
json
{
    "customerID": 3,
    "identityUserID": "140ff0f2-e784-4ddb-a07e-e17a7e9209c1",
    "customerEmail": "marta.mustermann@gmail.com",
    "customerLastName": "Mustermann",
    "customerFirstName": "Marta",
    "customerCity": "Wien",
    "customerStreet": "Musterstraße",
    "customerHausNumber": "12A",
    "customerPostIndex": "1234",
    "customerPhone": "066455570770",
    "shopOrders": null
}

------------------------------------------------------------------------------------------------------------------------------------------------


## 6. Get information about all customers
GET /api/Customer/getAll

Access Level: 
Admin Only (Requires authentication & admin role)

Description & Logic: 
Retrieving data about all customers.

Response (200 OK):
json
{
    "message": "Operation successful",
    "customers": [
        {
            "customerID": 1,
            "identityUserID": "90409e55-329a-4150-8465-7060183c8e1d",
            "customerEmail": "test1@gmail.com",
            "customerLastName": "Mustermann",
            "customerFirstName": "Peter",
            "customerCity": "Graz",
            "customerStreet": "Musterstraße",
            "customerHausNumber": "12A",
            "customerPostIndex": "1234",
            "customerPhone": "66499970770",
            "shopOrders": null
        },
        {
            "customerID": 2,
            "identityUserID": "5fc9e9fe-b96b-460a-a4e3-790809936291",
            "customerEmail": "test2@gmail.com",
            "customerLastName": "Mustermann",
            "customerFirstName": "Marta",
            "customerCity": "Wien",
            "customerStreet": "Musterstraße",
            "customerHausNumber": "12A",
            "customerPostIndex": "1234",
            "customerPhone": "66499970770",
            "shopOrders": null
        }
    ]
}

------------------------------------------------------------------------------------------------------------------------------------------------


## 7. Searching customer
GET /api/Customer/search

Access Level: 
Admin Only (Requires authentication & admin role)

Description & Logic: 
Searching for a customer using any number and combination of their properties. 
For string fields, a match is considered positive if the field equals or contains the search string from the request.

Example:
/api/Customer/search?CustomerID=&IdentityUserID=&CustomerEmail&CustomerLastName=&CustomerFirstName&CustomerCity=W
&CustomerStreet&CustomerHausNumber&CustomerPostIndex&CustomerPhone

Searching for cities that contain "W" in their name, with other parameters as empty strings.

Response (200 OK):
json
{
    "message": "Operation successful",
    "customers": [
        {
            "customerID": 2,
            "identityUserID": "5fc9e9fe-b96b-460a-a4e3-790809936291",
            "customerEmail": "test2@gmail.com",
            "customerLastName": "Mustermann",
            "customerFirstName": "Marta",
            "customerCity": "Wien",
            "customerStreet": "Musterstraße",
            "customerHausNumber": "12A",
            "customerPostIndex": "1234",
            "customerPhone": "66499970770",
            "shopOrders": null
        },
        {
            "customerID": 3,
            "identityUserID": "140ff0f2-e784-4ddb-a07e-e17a7e9209c1",
            "customerEmail": "marta.mustermann@gmail.com",
            "customerLastName": "Mustermann",
            "customerFirstName": "Marta",
            "customerCity": "Wien",
            "customerStreet": "Musterstraße",
            "customerHausNumber": "12A",
            "customerPostIndex": "1234",
            "customerPhone": "066455570770",
            "shopOrders": null
        }
    ]
}

------------------------------------------------------------------------------------------------------------------------------------------------


## 8. Add a new product
POST /api/Product/add

Access Level: 
Admin Only (Requires authentication & admin role)

Description & Logic:
Adding a new product. 
The SCU number is checked for uniqueness.

Request Body:
json
{
    "productSCU": "ST205",
    "productName": "Samsung A40",
    "category": "Smartphone",
    "unitPrice": 999.99,
    "stockQuantity": 100
}

Response (200 OK):
json
{
     "message": "Operation successful"
}

------------------------------------------------------------------------------------------------------------------------------------------------


## 9. Update the product
POST /api/Product/update/{productId}

Access Level: 
Admin Only (Requires authentication & admin role)

Description & Logic:
To update a product, a product ID is required. 
Any number of product properties can be updated. 
The update data is validated for correct format.

Request Body:
json
{
    "unitPrice": 888.99,
    "stockQuantity": 250
}

Response (200 OK):
json
{
     "message": "Operation successful"
}

------------------------------------------------------------------------------------------------------------------------------------------------


## 10. Get information about all products
GET /api/Product/getAll

Access Level: 
Admin Only (Requires authentication & admin role)

Description & Logic:
Getting full information about all pruducts in database for admins (employees).

Response (200 OK):
json
{
    "message": "Operation successful",
    "products": [
        {
            "productID": 1,
            "productSCU": "LT203",
            "productName": "Dell-21",
            "category": "Laptop",
            "unitPrice": 759.99,
            "stockQuantity": 48,
            "shopOrderProducts": null
        },
        {
            "productID": 2,
            "productSCU": "LT205",
            "productName": "HP-111",
            "category": "Laptop",
            "unitPrice": 888.99,
            "stockQuantity": 250,
            "shopOrderProducts": null
        }
    ]
}

------------------------------------------------------------------------------------------------------------------------------------------------


## 11. Add order
POST /api/Order/{userId}

Access Level: 
Customer Only (Requires authentication & user role)

Description & Logic:
A new order can only be placed by an authenticated user. 
To create an order, a UserID is required. An order can contain any number of products.

Before the order is accepted, the system checks whether there is enough stock available to fulfill it. 
If there are insufficient products in stock, the order is rejected, and the user receives a message specifying which products are out of stock.

Once the order is accepted, the order confirmation time is recorded, and its status is updated. 
After confirmation, the stock quantity is reduced based on the number of products in the order.

Request Body:
json
{
    "products": [
    {
      "productID": 1,
      "productName": "Laptop Dell-21",
      "productQuantity": 1
    },
    {
      "productID": 4,
      "productName": "Samsung A40",
      "productQuantity": 1
    }
  ]
}

Response (200 OK):
json
{
     "message": "Operation successful"
}

------------------------------------------------------------------------------------------------------------------------------------------------


## 12. Get all orders for particular customer
POST /api/Order/getAllFor{userId}

Access Level: 
Admin or Customer (Requires authentication with either admin or customer role)

Description & Logic:
The response includes information about all orders of the customer for the requested user ID. 
The information contains data from multiple tables, including the order status and details about the products in the order.

Response (200 OK):
json
{
     "message": "Operation successful",
    "data": {
        "shopOrders": [
            {
                "shopOrderID": 9,
                "shopOrderStatus": "Accepted",
                "products": [
                    {
                        "productID": 1,
                        "productName": "Laptop Dell-21",
                        "productQuantity": 1
                    },
                    {
                        "productID": 4,
                        "productName": "Samsung A40",
                        "productQuantity": 1
                    }
                ]
            }
        ]
    }
}




# DEUTSCH BESCHREIBUNG


# 1. Projektbeschreibung

Dieses Projekt ist ein Backend-Server für ein Kunden-, Produkt- und Bestellverwaltungssystem für einen Online-Shop.

Registrierung im System mit zwei Zugriffsebenen:

- Benutzer für Kunden
- Administrator für Mitarbeiter



# 2. Technologien:

ASP.NET Core 8.0 (C#) 
Entity Framework Core 8.0 
MySQL 8.0 
ASP.NET Core Identity Framework
JWT Authentication 
Logging (Serilog)

# 3. Projektstruktur

```
ProjectEverythingForHomeOnlineShop
│── Application
│   ├── DTOs         # Alle DTOs für die Geschäftslogik mit Validierung     
│   ├── Services     # Geschäftslogik für API-Anfragen 
│   │   ├── Implementations
│   │   ├── IAuthService.cs
│   │   ├── ICustomerService.cs
│   │   ├── IOrderService.cs
│   │   ├── IProductService.cs
│── Controllers
│   ├── AuthController.cs
│   ├── CustomerController.cs
│   ├── OrderController.cs
│   ├── ProductController.cs
│── Core
│   ├── Models                               
│   │   ├── Customer.cs
│   │   ├── Product.cs
│   │   ├── ShopOrder.cs
│   │   ├── ShopOrderProduct.cs
│   │   ├── ShopOrderStatus.cs
│── DataAccess
│   ├── Migrations
│   ├── Persistence
│   │   ├── Identity        
│   │   │   ├── ApplicationUser.cs      # Klasse für Entitäten in der Identität-Benutzertabelle 
│   │   ├── DatabaseInitializer.cs      # Erstellt die Datenbank, falls sie nicht existiert, führt Migrationen beim Start der App durch 
│   │   ├── OnlineShopMySQLDatabaseContext.cs   # Datenbankkontext mit allen Einstellungen für die Datenbank 
│   ├── Repositories
│   │   ├── Implementations
│   │   ├── ICustomerRepository.cs
│   │   ├── IOrderRepository.cs
│   │   ├── IProductRepository.cs
│   ├── JwtAuthentication
│   │   ├── JwtAuthenticationExtensions.cs
│   │   ├── TokenGenerator.cs
│   ├── LoggingConfig.cs
│   ├── ServiceResult.cs    # Klasse für spezielle Antwortformate für das Frontend 
│── logs
│── appsettings.json
│── appsettings.Development.json
│── Program.cs
│── ProjectEverythingForHomeOnlineShop.http
```

# 4.Datenbankstruktur

Tabelle customers
- `CustomerID` 
- `IdentityUserID` 
- `CustomerEmail` 
- `CustomerLastName`
- `CustomerFirstName` 
- `CustomerCity` 
- `CustomerStreet` 
- `CustomerHausNumber` 
- `CustomerPostIndex` 
- `CustomerPhone`

Tabelle products
- `ProductID` 
- `ProductSCU`   
- `ProductName`  
- `Category`  
- `UnitPrice`  
- `StockQuantity` Verfügbare Lagerbestände

Tabelle shopOrders
- `ShopOrderID`
- `CustomerID` 
- `ShopOrderStatusID` 
- `ShopOrderAcceptedAt` 
- `ShopOrderShippedAt`  
- `ShopOrderCompletedAt` 

Tabelle shopOrderProducts
- `ShopOrderID` 
- `ProductID`  
- `ProductQuantity` - Anzahl des Produkts in der Bestellung

Tabelle shopOrderStatuses
- `ShopOrderStatusID` 
- `ShopOrderStatusName`
  


# 5.API Endpoints



## 1. Administrator-Registrierung
POST /api/Auth/registerAdmin

Zugriffsebene:
Nur für Administratoren (Erfordert Authentifizierung & Admin-Rolle)

Beschreibung & Logik:
Erstellung eines neuen Benutzers mit Administratorrechten für Unternehmensmitarbeiter.
Ein neuer Administrator kann nur von einem bestehenden Administrator registriert werden.
Bei der Erstellung eines Administrators wird ein temporäres Passwort generiert, das später geändert werden kann.

Anfrage:

json
{
  "adminName": "adminNew",
  "adminEmail": "adminNew@gmail.com"
}

Antwort  (200 OK):
json
{
    "message": "User registered successfully",
    "adminId": "2bdaf47f-4196-4cbc-b0c0-c96f47b8e065",
    "temporaryPassword": "TemporaryPassword1!"
}



## 2. Kundenregistrierung
POST /api/Auth/registerCustomer

Zugriffsebene:
Öffentlich (Keine Authentifizierung erforderlich)

Beschreibung & Logik:
Ein Kunde muss alle erforderlichen Daten eingeben, einschließlich Login und Passwort.
Die E-Mail-Adresse wird überprüft, bevor der Benutzer erstellt wird.
Nach erfolgreicher Erstellung wird ein neuer Eintrag in der customers-Tabelle angelegt.

Anfrage::
json
{
  "userName": "usernew",
  "password": "Hello12345!",
  "customerFirstName": "Marta",
  "customerLastName": "Mustermann",
  "customerStreet": "Musterstraße",
  "customerHausNumber": "12A",
  "customerPostIndex": "1234",
  "customerCity": "Wien",
  "customerEmail": "marta.mustermann@gmail.com",
  "customerPhone": "066455570770"
}

Antwort (200 OK):
json
{
    "message": "User registered successfully",
    "userId": "140ff0f2-e784-4ddb-a07e-e17a7e9209c1"
}



## 3. Anmeldung eines Benutzers
POST /api/auth/login

Zugriffsebene:
Öffentlich (Keine Authentifizierung erforderlich)

Beschreibung & Logik:
Dient zur Authentifizierung von Administratoren und Kunden.
Bei erfolgreicher Anmeldung erhält der Benutzer ein Token, ein Ablaufdatum und eine Benutzer-ID.

Anfrage:
json
{
   "userName": "usernew",
    "userPassword": "Hello12345!",
    "userEmail": "marta.mustermann@gmail.com"
}

Antwort (200 OK):
json
{
    "token": {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9
        .eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNl
        cm5ldyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsY
        WRkcmVzcyI6Im1hcnRhLm11c3Rlcm1hbm5AZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS
        93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImp0aSI6ImU4MmMwMmJmLTQ2ZDktNGU4NS0
        4NmM0LWE3NDUwM2Q5NDM2NyIsImV4cCI6MTczODQ5MTExNywiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAxNCIs
        ImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTQifQ
        ._ibZR7YxDbYOTtvtyN37AC0AlzT3poOHMpgjih5yU7U",
        "expiration": "2025-02-02T10:11:57Z",
        "userID": "140ff0f2-e784-4ddb-a07e-e17a7e9209c1"
    }
}



## 4. Passwort ändern
POST /api/auth/updatePassword/{userID}

Zugriffsebene:
Administrator oder Kunde (Erfordert Authentifizierung mit Admin- oder Kundenrolle)

Beschreibung & Logik:
Der Server muss das alte und das neue Passwort vom authentifizierten Benutzer erhalten.

Anfrage:
json
{
    "currentUserPassword": "TemporaryPassword1!",
    "newUserPassword": "admin2Password1!"
}

Antwort (200 OK):
json
{
    "message": "Password changed successfully"
}



## 5. Kunde nach ID abrufen
GET /api/Customer/get/{userID}

Zugriffsebene:
Administrator oder Kunde (Erfordert Authentifizierung mit Admin- oder Kundenrolle)

Beschreibung & Logik:
Abrufen aller Kundendaten. 


Antwort (200 OK):
json
{
    "customerID": 3,
    "identityUserID": "140ff0f2-e784-4ddb-a07e-e17a7e9209c1",
    "customerEmail": "marta.mustermann@gmail.com",
    "customerLastName": "Mustermann",
    "customerFirstName": "Marta",
    "customerCity": "Wien",
    "customerStreet": "Musterstraße",
    "customerHausNumber": "12A",
    "customerPostIndex": "1234",
    "customerPhone": "066455570770",
    "shopOrders": null
}


## 6. Informationen über alle Kunden abrufen
GET /api/Customer/getAll

Zugriffslevel:
Nur für Administratoren (erfordert Authentifizierung & Administratorrolle)

Beschreibung & Logik:
Abrufen von Daten über alle Kunden.

Antwort (200 OK):
json
{
    "message": "Operation successful",
    "customers": [
        {
            "customerID": 1,
            "identityUserID": "90409e55-329a-4150-8465-7060183c8e1d",
            "customerEmail": "test1@gmail.com",
            "customerLastName": "Mustermann",
            "customerFirstName": "Peter",
            "customerCity": "Graz",
            "customerStreet": "Musterstraße",
            "customerHausNumber": "12A",
            "customerPostIndex": "1234",
            "customerPhone": "66499970770",
            "shopOrders": null
        },
        {
            "customerID": 2,
            "identityUserID": "5fc9e9fe-b96b-460a-a4e3-790809936291",
            "customerEmail": "test2@gmail.com",
            "customerLastName": "Mustermann",
            "customerFirstName": "Marta",
            "customerCity": "Wien",
            "customerStreet": "Musterstraße",
            "customerHausNumber": "12A",
            "customerPostIndex": "1234",
            "customerPhone": "66499970770",
            "shopOrders": null
        }
    ]
}



## 7. Kunden suchen
GET /api/Customer/search

Zugriffslevel:
Nur für Administratoren (erfordert Authentifizierung & Administratorrolle)

Beschreibung & Logik:
Suche nach einem Kunden anhand einer beliebigen Anzahl und Kombination seiner Eigenschaften.
Bei Zeichenkettenfeldern wird ein Treffer als positiv gewertet, wenn das Feld die Suchzeichenkette enthält oder ihr entspricht.

Beispiel:
/api/Customer/search?CustomerID=&IdentityUserID=&CustomerEmail&CustomerLastName=&CustomerFirstName
&CustomerCity=W&CustomerStreet&CustomerHausNumber&CustomerPostIndex&CustomerPhone

Sucht nach Städten, die den Buchstaben „W“ im Namen enthalten, während andere Parameter leer sind.

Antwort (200 OK):
json
{
    "message": "Operation successful",
    "customers": [
        {
            "customerID": 2,
            "identityUserID": "5fc9e9fe-b96b-460a-a4e3-790809936291",
            "customerEmail": "test2@gmail.com",
            "customerLastName": "Mustermann",
            "customerFirstName": "Marta",
            "customerCity": "Wien",
            "customerStreet": "Musterstraße",
            "customerHausNumber": "12A",
            "customerPostIndex": "1234",
            "customerPhone": "66499970770",
            "shopOrders": null
        },
        {
            "customerID": 3,
            "identityUserID": "140ff0f2-e784-4ddb-a07e-e17a7e9209c1",
            "customerEmail": "marta.mustermann@gmail.com",
            "customerLastName": "Mustermann",
            "customerFirstName": "Marta",
            "customerCity": "Wien",
            "customerStreet": "Musterstraße",
            "customerHausNumber": "12A",
            "customerPostIndex": "1234",
            "customerPhone": "066455570770",
            "shopOrders": null
        }
    ]
}



## 8. Neues Produkt hinzufügen
POST /api/Product/add

Zugriffslevel:
Nur für Administratoren (erfordert Authentifizierung & Administratorrolle)

Beschreibung & Logik:
Hinzufügen eines neuen Produkts.
Die SCU-Nummer wird auf ihre Einzigartigkeit überprüft.

Anfrage:
json
{
    "productSCU": "ST205",
    "productName": "Samsung A40",
    "category": "Smartphone",
    "unitPrice": 999.99,
    "stockQuantity": 100
}

Antwort (200 OK):
json
{
     "message": "Operation successful"
}




## 9. Produkt aktualisieren
POST /api/Product/update/{productId}

Zugriffslevel:
Nur für Administratoren (erfordert Authentifizierung & Administratorrolle)

Beschreibung & Logik:
Zur Aktualisierung eines Produkts ist eine Produkt-ID erforderlich.
Beliebige Produkteigenschaften können aktualisiert werden.
Die Aktualisierungsdaten werden auf ihr korrektes Format überprüft.

Anfrage:
json
{
    "unitPrice": 888.99,
    "stockQuantity": 250
}

Antwort (200 OK):
json
{
     "message": "Operation successful"
}



## 10. Informationen über alle Produkte abrufen
GET /api/Product/getAll

Zugriffslevel:
Nur für Administratoren (erfordert Authentifizierung & Administratorrolle)

Beschreibung & Logik:
Abrufen vollständiger Informationen über alle Produkte in der Datenbank für Administratoren (Mitarbeiter).

Antwort (200 OK):
json
{
    "message": "Operation successful",
    "products": [
        {
            "productID": 1,
            "productSCU": "LT203",
            "productName": "Dell-21",
            "category": "Laptop",
            "unitPrice": 759.99,
            "stockQuantity": 48,
            "shopOrderProducts": null
        },
        {
            "productID": 2,
            "productSCU": "LT205",
            "productName": "HP-111",
            "category": "Laptop",
            "unitPrice": 888.99,
            "stockQuantity": 250,
            "shopOrderProducts": null
        }
        {
            "productID": 4,
            "productSCU": "ST205",
            "productName": "Samsung A40",
            "category": "Smartphone",
            "unitPrice": 999.99,
            "stockQuantity": 100,
            "shopOrderProducts": null
        }
    ]
}

------------------------------------------------------------------------------------------------------------------------------------------------


## 11. Bestellung hinzufügen
POST /api/Order/{userId}

Zugriffslevel:
Nur für Kunden (erfordert Authentifizierung & Benutzerrolle)

Beschreibung & Logik:
Eine neue Bestellung kann nur von einem authentifizierten Benutzer aufgegeben werden.
Zur Erstellung einer Bestellung ist eine Benutzer-ID erforderlich.
Eine Bestellung kann eine beliebige Anzahl von Produkten enthalten.

Vor Annahme der Bestellung prüft das System, ob genügend Lagerbestand verfügbar ist, um die Bestellung zu erfüllen.
Falls nicht genügend Produkte auf Lager sind, wird die Bestellung abgelehnt und der Benutzer erhält eine Nachricht 
mit den nicht verfügbaren Produkten.

Nach der Bestellannahme wird die Bestätigungszeit gespeichert und der Bestellstatus aktualisiert.
Anschließend wird die Lagerbestandsmenge basierend auf der bestellten Anzahl reduziert.

Anfrage:
json
{
    "products": [
    {
      "productID": 1,
      "productName": "Laptop Dell-21",
      "productQuantity": 1
    },
    {
      "productID": 4,
      "productName": "Samsung A40",
      "productQuantity": 1
    }
  ]
}

Antwort (200 OK):
json
{
     "message": "Operation successful"
}

------------------------------------------------------------------------------------------------------------------------------------------------


## 12. Alle Bestellungen eines bestimmten Kunden abrufen
POST /api/Order/getAllFor{userId}

Zugriffslevel:
Administrator oder Kunde (erfordert Authentifizierung mit entweder Administrator- oder Kundenrolle)

Beschreibung & Logik:
Die Antwort enthält Informationen über alle Bestellungen des Kunden für die angeforderte Benutzer-ID.
Die Informationen stammen aus mehreren Tabellen, einschließlich des Bestellstatus und Details zu den bestellten Produkten.

Antwort (200 OK):
json
{
     "message": "Operation successful",
    "data": {
        "shopOrders": [
            {
                "shopOrderID": 9,
                "shopOrderStatus": "Accepted",
                "products": [
                    {
                        "productID": 1,
                        "productName": "Laptop Dell-21",
                        "productQuantity": 1
                    },
                    {
                        "productID": 4,
                        "productName": "Samsung A40",
                        "productQuantity": 1
                    }
                ]
            }
        ]
    }
}
