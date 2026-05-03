# Order Processing System

## Описание

Order Processing System — учебный backend-проект на ASP.NET Core Web API, реализующий упрощённую систему обработки заказов.

Проект создан для демонстрации базовых навыков backend-разработки:

* построение REST API;
* работа с PostgreSQL через EF Core;
* использование Docker Compose для инфраструктуры;
* публикация событий в Apache Kafka;
* разделение проекта на архитектурные слои;
* применение Dependency Injection;
* асинхронная работа с базой данных;
* централизованная обработка ошибок;
* валидация входящих запросов.

Проект развивается поэтапно: от простого Web API до приложения с базой данных, Kafka-интеграцией и более чистой архитектурой.

---

## Архитектура

Проект разделён на несколько слоёв:

* **OrderProcessingSystem.API** — HTTP API, контроллеры, middleware, регистрация зависимостей.
* **OrderProcessingSystem.Application** — сценарии приложения, интерфейсы сервисов, DTO, валидация запросов.
* **OrderProcessingSystem.Domain** — доменные сущности и бизнес-правила.
* **OrderProcessingSystem.Infrastructure** — работа с PostgreSQL, EF Core, Kafka producer.

Основной поток создания заказа:

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

## Используемые технологии

* ASP.NET Core Web API
* C#
* PostgreSQL
* EF Core
* Apache Kafka
* Docker / Docker Compose
* FluentValidation
* Dependency Injection
* Bash scripts для локальной проверки API и Kafka

---

## Текущая функциональность

### Заказы

Проект поддерживает следующие операции:

* создание заказа;
* получение заказа по ID;
* получение списка заказов;
* сохранение заказов и позиций заказа в PostgreSQL;
* публикация события `OrderCreated` в Kafka после успешного создания заказа.

### Валидация

Для создания заказа используется FluentValidation.

Проверяется:

* заказ должен содержать хотя бы одну позицию;
* название товара не должно быть пустым;
* цена должна быть больше нуля;
* количество должно быть больше нуля.

### Обработка ошибок

В проекте добавлен централизованный middleware для обработки исключений.

Он преобразует ошибки в корректные HTTP-ответы:

* ошибки валидации — `400 Bad Request`;
* ошибки доменной логики — `400 Bad Request`;
* непредвиденные ошибки — `500 Internal Server Error`.

---

## API Endpoints

| Метод | Endpoint           | Описание                |
| ----- | ------------------ | ----------------------- |
| POST  | `/api/orders`      | Создать заказ           |
| GET   | `/api/orders/{id}` | Получить заказ по ID    |
| GET   | `/api/orders`      | Получить список заказов |

---

## Пример создания заказа

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

Пример ответа:

```json
{
  "orderId": "8f709eb3-6572-46ce-a1c5-1f6e09550d29"
}
```

---

## Пример получения заказа по ID

```http
GET /api/orders/8f709eb3-6572-46ce-a1c5-1f6e09550d29
```

---

## Пример получения списка заказов

```http
GET /api/orders
```

---

## Инфраструктура

Через Docker Compose поднимаются:

* PostgreSQL;
* Apache Kafka.

Файл инфраструктуры:

```text
docker-compose.yml
```

Запуск инфраструктуры:

```bash
docker compose up -d
```

Остановка инфраструктуры:

```bash
docker compose down
```

Остановка с удалением volume-данных:

```bash
docker compose down -v
```

---

## Настройки приложения

Основные настройки находятся в:

```text
src/OrderProcessingSystem.API/appsettings.json
```

Пример:

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

## Миграции базы данных

Для применения миграций используется EF Core CLI.

Применить миграции:

```bash
dotnet ef database update \
  --project src/OrderProcessingSystem.Infrastructure \
  --startup-project src/OrderProcessingSystem.API
```

Создать новую миграцию:

```bash
dotnet ef migrations add MigrationName \
  --project src/OrderProcessingSystem.Infrastructure \
  --startup-project src/OrderProcessingSystem.API
```

---

## Запуск API

После запуска PostgreSQL и Kafka через Docker Compose можно запустить API:

```bash
dotnet run --project src/OrderProcessingSystem.API
```

По умолчанию API доступен по адресу:

```text
http://localhost:5260
```

---

## Проверка через scripts

В проекте используются shell-скрипты для быстрой проверки API и Kafka.

Создать заказ:

```bash
./scripts/create-order.sh
```

Получить список заказов:

```bash
./scripts/get-orders.sh
```

Получить заказ по ID:

```bash
./scripts/get-order-by-id.sh <order-id>
```

Запустить Kafka consumer для чтения событий `OrderCreated`:

```bash
./scripts/consume-order-created.sh
```

Если скрипты не запускаются из-за прав доступа:

```bash
chmod +x scripts/*.sh
```

---

## Kafka

После успешного создания заказа приложение публикует событие в topic:

```text
orders.created
```

Пример события:

```json
{
  "OrderId": "8f709eb3-6572-46ce-a1c5-1f6e09550d29",
  "CreatedAt": "2026-05-03T07:02:52.188714Z",
  "ItemsCount": 1
}
```

Для локальной проверки используется Kafka console consumer через скрипт:

```bash
./scripts/consume-order-created.sh
```

---

## Структура проекта

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

## Что демонстрирует проект

Проект демонстрирует:

* построение ASP.NET Core Web API;
* работу с PostgreSQL через EF Core;
* применение миграций базы данных;
* использование Docker Compose для локальной инфраструктуры;
* публикацию событий в Apache Kafka;
* разделение ответственности между слоями;
* работу через интерфейсы и Dependency Injection;
* асинхронный доступ к базе данных;
* валидацию входящих запросов;
* централизованную обработку ошибок.
