# Order Processing System (ASP.NET Core + PostgreSQL + Kafka + Docker)

## Description

This project is a simplified order processing system implemented as a Web API.

The goal of the project is to demonstrate basic backend development skills:

* working with REST API
* interacting with a database (PostgreSQL)
* using a message producer (Kafka)
* containerizing the application (Docker)

The project is developed step by step with a gradual increase in architectural complexity.

## Architecture

The project is built with separation into layers:

* **API** — handling HTTP requests
* **Application** — business logic
* **Domain** — domain models
* **Infrastructure** — working with the database and external services

This approach allows you to:

* reduce component coupling
* simplify testing
* ensure scalability

## Technologies Used

* ASP.NET Core Web API
* PostgreSQL
* EF Core
* Apache Kafka
* Docker / Docker Compose

## Functionality

### Stage 1 — Basic API + Database

* Creating an order
* Getting an order by ID
* Getting a list of orders
* Saving data to PostgreSQL

### Stage 2 — Kafka Integration

* Publishing an event when an order is created
* Processing events through a consumer

### Stage 3 — Docker

* Containerizing the application
* Starting the environment via docker-compose:

  * API
  * PostgreSQL
  * Kafka

### Stage 4 — Logic Expansion

* Adding order statuses (Created, Paid, Cancelled)
* Simulating payment
* Generating additional events

## API Examples

### Create an order

POST /orders

### Get an order

GET /orders/{id}

### Get a list of orders

GET /orders


## Running the Project

```bash
docker-compose up --build
```

After startup, the API will be available at:

```
http://localhost:5000
```

## Development Plans

* Adding authorization
* Extended validation
* Logging and error handling
* Separation into microservices

## Project Goal

The project was created as an educational and demonstration project to show:

* C# programming skills
* understanding of backend development basics
* working with infrastructure tools
* ability to build an extensible architecture