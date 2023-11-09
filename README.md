# ProductHub

ProductHub is a web application designed to manage and showcase various products, allowing users to register, authenticate, and interact with the product database.

## Table of Contents

- [Overview](#overview)
- [Functional Requirements](#functional-requirements)
- [Folder Structure](#folder-structure)
- [Database Models](#database-models)
- [How to Run](#how-to-run)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## Overview

ProductHub is built using ASP.NET Core and follows a Clean Architecture with Domain-Driven Design (DDD) principles. It provides functionalities for user registration, authentication, product management, and category management.

## Functional Requirements

The application supports the following user roles:

- **Admin:**
  - Full access to all API endpoints and functionalities.
  - Create, read, update, and delete products and categories.
  - Manage user accounts and roles.

- **User:**
  - Retrieve product information and categories.
  - Limited ability to create, update, and delete their own products.
  - Search and filter products based on specific criteria.

## Folder Structure

The project follows a modular and organized folder structure:


For details on each folder and its contents, refer to the [Folder Structure](./ProductHub.Api/README.md) documentation.

## Database Models

The application uses Entity Framework Core for database operations. The main data models include:

- **User:**
  - Attributes: Username, email, password
  - Relationships: One-to-many with products (user's listings)

- **Product:**
  - Attributes: Name, description, category, pricing, availability
  - Relationships: Many-to-one with user (product owner), many-to-one with category

- **Category:**
  - Attributes: Name, description
  - Relationships: One-to-many with products

For detailed information on database models and relationships, refer to the [Data Models](./ProductHub.Domain/README.md) documentation.

## How to Run

To run the application locally, follow these steps:

1. Clone the repository.
2. Navigate to the `ProductHub.Api` folder.
3. Run the application using `dotnet run`.

For more detailed instructions, refer to the [How to Run](./ProductHub.Api/README.md#how-to-run) documentation.

## API Endpoints

The application exposes various API endpoints for user authentication, product management, and category management. Refer to the [API Documentation](./ProductHub.Api/README.md#api-documentation) for details on available endpoints and their functionalities.

## Contributing

We welcome contributions! If you would like to contribute to the project, please follow our [Contribution Guidelines](./CONTRIBUTING.md).

## License

This project is licensed under the [MIT License](./LICENSE).
