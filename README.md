# Blog Post

## Overview

This project is a **coursework for the Web Applications Development** module. The project, named **CWProject_00015662**, is built using the **.NET Framework** and follows a **client-server architecture**. The solution consists of two main projects:

- `Client_00015662`: Handles the client-side logic using **Windows Forms**.
- `Server_00015662`: Manages the server-side functionality, including **data access via Entity Framework**.

## Features

- **Client-Server Architecture**: A clear separation of concerns between the client and server components.
- **Entity Framework 6.4.4**: Used for data access, providing a robust ORM solution.
- **Windows Forms UI**: A rich, interactive user interface designed using **Windows Forms**.
- **Design Patterns**: The project employs several design patterns to enhance maintainability and scalability.

## Design Patterns

### Singleton Pattern

The **Singleton pattern** ensures that a class has only one instance and provides a global point of access to it. This is particularly useful for managing shared resources, such as database connections.

### Observer Pattern

The **Observer pattern** defines a one-to-many dependency between objects so that when one object’s state changes, all its dependents are notified and updated automatically. It is especially useful for implementing event handling in the application.

## Prerequisites

Before starting the project, ensure you have the following versions installed:

- **Visual Studio 2019** or later
- **.NET Framework 4.8**
- **Entity Framework 6.4.4**
- **Angular Version 19** (for the client-side app)
- **ASP.NET Core Version 6.0** (for the backend app)

## Setup Instructions

### Install Dependencies for the Client App (Angular)

1. Navigate to the client application directory.
2. Run the following command to install the necessary dependencies for the frontend application:
    ```bash
    npm install
    ```

### Running the Backend Application (ASP.NET Core)

Ensure that the backend application is running before starting the frontend. The frontend relies on the backend API for data handling.

### Running the Application

1. **Set up the backend**: Set `Server_00015662` as the startup project and run it.
2. **Set up the frontend**: Set `Client_00015662` as the startup project and run it.

### "Unexpected End of File" Error (Angular)

If you encounter the "unexpected end of file" error in the Angular project, it may be caused by a `package.json` file in a parent directory. To resolve this:

- Delete any `package.json` files found in the parent directories.
- For further details, refer to this [GitHub Issue](https://github.com/vitejs/vite/issues/2404).

## Troubleshooting

- Ensure that the **backend** is up and running before launching the **frontend**.
- If you face issues with missing or incorrect dependencies, running `npm install` in the client app directory should resolve them.

## License

This project is licensed under the **MIT License**.

