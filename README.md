# RapidPayWebApi

RapidPayWebApi is a payment API built with ASP.NET Core that allows users to create cards, make payments, and check card balances.

## Table of Contents

- [Assumptions](#assumptions)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
  - [Endpoints](#endpoints)
    - [Create Card](#create-card)
    - [Make Payment](#make-payment)
    - [Check Card Balance](#check-card-balance)
    - [User Authentication](#user-authentication)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## Assumptions

1. **Card Type and Balance**: The API assumes a basic card model with a card number and balance. The card type and balance are not explicitly specified, so we assume a generic card with a numerical balance.

2. **Payment Fee Calculation**: The API includes a payment fee calculation using the `IUfeService`. The exact logic for calculating fees is not detailed in the code; you may customize it based on your business requirements.

## Getting Started

Follow the steps below to set up and run the RapidPayWebApi.

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed
- A SQL Server database for data storage

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/your-username/RapidPayWebApi.git
    ```

2. Navigate to the project directory:

    ```bash
    cd RapidPayWebApi
    ```

3. Update the database connection string in `appsettings.json`:

    ```json
    "ConnectionStrings": {
        "DefaultSQLConnection": "your-database-connection-string"
    }
    ```

4. Run the application:

    ```bash
    dotnet run
    ```

## Usage

### Endpoints

#### Create Card

Endpoint: `POST /api/Payment/CreateCard`

Creates a new card with a balance of 100.

#### Make Payment

Endpoint: `POST /api/Payment/Pay`

Makes a payment using the specified card. The payment request should include the card details and the payment amount.

#### Check Card Balance

Endpoint: `GET /api/Payment/GetBalance`

Retrieves the remaining balance of a card based on the card number.

#### User Authentication

Endpoints: 

- `POST /api/UsersAuth/login`: Login for existing users.
- `POST /api/UsersAuth/register`: Register a new user.

## API Endpoints

- `POST /api/Payment/CreateCard`: Creates a new card.
- `POST /api/Payment/Pay`: Makes a payment.
- `GET /api/Payment/GetBalance`: Retrieves card balance.
- `POST /api/UsersAuth/login`: User login.
- `POST /api/UsersAuth/register`: User registration.

For detailed API documentation, you can explore the Swagger UI at `https://localhost:5001/swagger/index.html` after running the application.

## Contributing

Contributions are welcome! If you find any issues or have suggestions, please open an [issue](https://github.com/your-username/RapidPayWebApi/issues) or create a [pull request](https://github.com/your-username/RapidPayWebApi/pulls).

## License

This project is licensed under the [MIT License](LICENSE).
