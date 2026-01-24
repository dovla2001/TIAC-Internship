# Full Stack Web Shop Application

A full stack application created during an internship at **TIAC d.o.o**. The application implements a web shop where employees can purchase products offered by the company, such as **t-shirts, backpacks, hoodies**, and other items.

## Technologies

- **Backend:** ASP.NET Core, MediatR, FastEndpoints, CQRS, FluentValidation, CleanArchitecture
- **Frontend:**  HTML, CSS, Angular, RxJS  
- **Database:** SQL Server

## Features

- User registration and login  
- Display available products  
- Add different products to the cart  
- Place orders and view order summary  
- Add new products (Admin)  
- View all orders (Admin)  
- Send email confirmation to both user and admin

## Installation / Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/dovla2001/TIAC-Internship.git

2. **Setup the backend**
      - Navigate to the backend folder:
    ```bash
    cd TIAC-Internship/backend
    ```
    
      -Restore NuGet packages:
    ```bash
    dotnet restore
    ```
    - Update the connection string in appsettings.json if necessary to point to your local SQL Server instance.
    - Apply database migrations (if any):
    ```dotnet ef database update
    dotnet ef database update
    ```
    - Run the backened
    ```bash 
    dotnet run

3. **Setup the frontend**
    - Navigate to the frontedn folder
    ```bash
    cd TIAC-Internship/frontend
    ```
    - Install npm dependencies:
    ```bash
    npm install
    ```
    - Run the frontend:
    ```bash
    npm start

4.  **Access the application**
    
    - Open your browser and go to:
    ```bash
    http://localhost:7112
    ```
    - You should see the Web Shop homepage and be able to test all features.

## Author
**Vladimir Mandić**  
Email: vmandic83@gmail.com

