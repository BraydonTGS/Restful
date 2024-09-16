# Restful: A Custom WPF Postman Application

Restful is a WPF application designed to take the functionality of popular API testing tools like Postman and implement it with custom features from the ground up. 

Built using .NET 8, Entity Framework Core, Prism, and the MVVM framework, Restful offers a highly modular, scalable, and testable approach to API interaction and management.

## Project Structure

### Entity
The `Entity` project contains the Entity Objects that represent tables in the database. 
- These entities are used to map the database tables to object-oriented entities in the program.
- When using Entity Framework Core's Code First approach, you define your entity objects as classes, and the framework automatically creates the corresponding database tables based on these classes. 
- Each entity object represents a single table in the database.

### Core
The `Core` project contains the business logic of the application. It includes the following components:

- **RestfulDbContext:** The Restful project includes a RestfulDbContext class that represents the database context. This class is responsible for defining the database schema, configuring the entity mappings, and managing the interactions with the database. It sets up the DbSets for the entity types and provides the necessary configuration for Entity Framework Core.

- **Migrations:** Migrations are used to evolve the database schema over time as the application evolves. It allows you to easily apply changes to the database schema or roll back to previous versions. Using Entity Framework Core's migration tools, I can generate migration scripts based on the changes I have made to the DbContext and apply those migrations to the database.

- **Generic BaseRepository:** A generic base repository class that provides basic CRUD operations for interacting with the database using Entity Framework Core. This repository serves as a base class for specific repositories.

### Test Projects
The `Tests` Folder Contains all the Test Projects for the Application.
- **RestfulWeb.Core.Tests :**
- **RestfulWeb.Tests.Base :** 

### Global Project
The `Global` project serves as a central place for common files used across the application. It includes the following components:

- **Constants Class:** A Constants class that contains strings used across the application. This allows for centralized management and easy access to shared values.
  
## Technologies Used

- **.NET 8:** The project is built using the .NET 8 framework, which provides the latest features and improvements for developing robust applications.
- **Prism Framework:** For MVVM support and modularity.
- **MAH Apps Metro:** For custom controls and a polished UI.
- **Entity Framework Core - Version 8.0.1:** Entity Framework Core is used as the Object-Relational Mapping (ORM) framework to interact with the database. It simplifies data access and provides powerful querying capabilities.
- **MS Test:** The unit tests are written using the MS Test framework, which is a testing framework included with the .NET platform.
- **Serilog** an open-source logging library for .NET applications. It provides a flexible and efficient logging framework with a focus on structured logging.
