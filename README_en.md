# Order Processing System

## Description

Order Processing System is an educational backend project built with ASP.NET Core Web API. It implements a simplified order processing flow with PostgreSQL persistence and Apache Kafka event publishing.

The project demonstrates core backend development skills:

* building REST API endpoints;
* working with PostgreSQL through EF Core;
* using Docker Compose for local infrastructure;
* publishing integration events to Apache Kafka;
* separating the application into architectural layers;
* applying Dependency Injection;
* using asynchronous database access;
* validating incoming requests;
* handling errors through centralized middleware.

The project is developed step by step, gradually increasing architectural complexity while keeping the business logic simple and understandable.

---

## Architecture

The solution is separated into several layers:

* **OrderProcessingSystem.API** — HTTP API, controllers, middleware, dependency registration.
* **OrderProcessingSystem.Application** — application use cases, service interfaces, DTOs, request validation.
* **OrderProcessingSystem.Domain** — domain entities and business rules.
* **OrderProcessingSystem.Infrastructure** — PostgreSQL persistence, EF Core, Kafka producer.

Main order creation flow:

```text
POST /api/orders
→ OrdersController
→ CreateOrderService
→ Order domain model
→ IOrderRepository
→ OrderRepository
→ AppDbContext
→ PostgreSQL
→ Kafka OrderCreated event
```

---

## Technologies Used

* ASP.NET Core Web API
* C#
* PostgreSQL
* EF Core
* Apache Kafka
* Docker / Docker Compose
* FluentValidation
* Dependency Injection
* Bash scripts for local API and Kafka checks

---

## Current Features

### Orders

The project currently supports:

* creating an order;
* getting an order by ID;
* getting a list of orders;
* saving orders and order items to PostgreSQL;
* publishing an `OrderCreated` event to Kafka after successful order creation.

### Validation

Order creation requests are validated with FluentValidation.

Validation rules:

* an order must contain at least one item;
* product name must not be empty;
* price must be greater than zero;
* quantity must be greater than zero.

### Error Handling

The project includes centralized exception handling middleware.

It converts exceptions into proper HTTP responses:

* validation errors — `400 Bad Request`;
* domain/application errors — `400 Bad Request`;
* unexpected errors — `500 Internal Server Error`.

---

## API Endpoints

| Method | Endpoint           | Description        |
| ------ | ------------------ | ------------------ |
| POST   | `/api/orders`      | Create a new order |
| GET    | `/api/orders/{id}` | Get an order by ID |
| GET    | `/api/orders`      | Get all orders     |

---

## Create Order Example

```http
POST /api/orders
Content-Type: application/json
```

```json
{
  "items": [
    {
      "productName": "Keyboard",
      "price": 100,
      "quantity": 1
    }
  ]
}
```

Example response:

```json
{
  "orderId": "8f709eb3-6572-46ce-a1c5-1f6e09550d29"
}
```

---

## Get Order by ID Example

```http
GET /api/orders/8f709eb3-6572-46ce-a1c5-1f6e09550d29
```

---

## Get Orders Example

```http
GET /api/orders
```

---

## Infrastructure

Docker Compose starts the local infrastructure:

* PostgreSQL;
* Apache Kafka.

Infrastructure file:

```text
docker-compose.yml
```

Start infrastructure:

```bash
docker compose up -d
```

Stop infrastructure:

```bash
docker compose down
```

Stop infrastructure and remove stored volume data:

```bash
docker compose down -v
```

---

## Application Settings

Main application settings are located in:

```text
src/OrderProcessingSystem.API/appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "Postgres": "Host=localhost;Port=5432;Database=orders_db;Username=postgres;Password=postgres"
  },
  "Kafka": {
    "BootstrapServers": "localhost:9092",
    "OrderCreatedTopic": "orders.created"
  }
}
```

---

## Database Migrations

EF Core CLI is used for database migrations.

Apply migrations:

```bash
dotnet ef database update \
  --project src/OrderProcessingSystem.Infrastructure \
  --startup-project src/OrderProcessingSystem.API
```

Create a new migration:

```bash
dotnet ef migrations add MigrationName \
  --project src/OrderProcessingSystem.Infrastructure \
  --startup-project src/OrderProcessingSystem.API
```

---

## Running the API

After starting PostgreSQL and Kafka through Docker Compose, run the API:

```bash
dotnet run --project src/OrderProcessingSystem.API
```

By default, the API runs at:

```text
http://localhost:5260
```

---

## Testing with Scripts

The project includes shell scripts for quick local checks.

Create an order:

```bash
./scripts/create-order.sh
```

Get all orders:

```bash
./scripts/get-orders.sh
```

Get order by ID:

```bash
./scripts/get-order-by-id.sh <order-id>
```

Consume `OrderCreated` Kafka events:

```bash
./scripts/consume-order-created.sh
```

If scripts cannot be executed because of permissions:

```bash
chmod +x scripts/*.sh
```

---

## Kafka

After an order is successfully created, the application publishes an event to the topic:

```text
orders.created
```

Example event:

```json
{
  "OrderId": "8f709eb3-6572-46ce-a1c5-1f6e09550d29",
  "CreatedAt": "2026-05-03T07:02:52.188714Z",
  "ItemsCount": 1
}
```

For local verification, Kafka console consumer is started through the script:

```bash
./scripts/consume-order-created.sh
```

---

## Project Structure

```text
Order-Processing-System/
├── src/
│   ├── OrderProcessingSystem.API/
│   ├── OrderProcessingSystem.Application/
│   ├── OrderProcessingSystem.Domain/
│   └── OrderProcessingSystem.Infrastructure/
├── scripts/
├── docker-compose.yml
└── README.md
```

---

## What This Project Demonstrates

The project demonstrates:

* building ASP.NET Core Web API endpoints;
* working with PostgreSQL through EF Core;
* applying database migrations;
* using Docker Compose for local infrastructure;
* publishing events to Apache Kafka;
* separating responsibilities between layers;
* using interfaces and Dependency Injection;
* asynchronous database access;
* request validation;
* centralized exception handling.

