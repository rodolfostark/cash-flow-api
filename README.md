## About the project

This **API**, developed using **.NET 8**, adopts the principles of **Domain-Driven Design (DDD)** to provide a structured and effective solution for managing personal expenses. The main objective is to allow users to record their expenses, detailing information such as title, date and time, description, amount, and payment type, with data securely stored in a **MySQL** database.

The architecture of the **API** is based on **REST**, utilizing standard **HTTP** methods for efficient and simplified communication. Additionally, it is complemented by **Swagger** documentation, providing an interactive graphical interface for developers to explore and test the endpoints easily.

Among the NuGet packages used, **AutoMapper** is responsible for mapping between domain objects and request/response objects, reducing the need for repetitive and manual code. **FluentAssertions** is used in unit tests to make assertions more readable, helping to write clear and comprehensible tests. For validations, **FluentValidation** is used to implement validation rules in a simple and intuitive way in request classes, keeping the code clean and easy to maintain. Finally, **EntityFramework** acts as an ORM (Object-Relational Mapper) that simplifies interactions with the database, allowing the use of .NET objects to manipulate data directly, without the need to deal with SQL queries.

### Features

- **Domain-Driven Design (DDD)**: Modular structure that facilitates the understanding and maintenance of the application domain.
- **Unit Tests**: Comprehensive tests with FluentAssertions to ensure functionality and quality.
- **Report Generation**: Capability to export detailed reports to **PDF and Excel**, offering a visual and effective analysis of expenses.
- **RESTful API with Swagger Documentation**: Documented interface that facilitates integration and testing by developers.

### Built with

![badge-dot-net]
![badge-windows]
![badge-visual-studio]
![badge-mysql]
![badge-swagger]

## Getting Started

To get a local copy up and running, follow these simple steps.

### Requirements

* Visual Studio version 2022+ or Visual Studio Code
* Windows 10+ or Linux/MacOS with [.NET SDK][dot-net-sdk] installed
* MySQL Server

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/rodolfostark/cash-flow-api.git
    ```

2. Fill in the information in the `appsettings.Development.json` file.
3. Run the API and enjoy testing it :)

<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-windows]: https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=fff&style=for-the-badge
[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge
[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge
