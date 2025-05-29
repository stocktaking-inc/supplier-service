# 📦 Supplier Service — Usage Guide

Микросервис `supplier-service` управляет поставщиками в системе Inventory Management. Он позволяет выполнять CRUD-операции с поставщиками и получать список товаров от конкретного поставщика.



## :wrench: Установка

Установите .NET 6.0+

Настройте строку подключения в appsettings.json


Запустите программу:

```bash
dotnet run
```

## :closed_lock_with_key: Аутентификация

Все запросы защищены с помощью JWT.

## :blue_book: Эндпоинты

### :clipboard: Получить список всех поставщиков

```bash
GET /suppliers
```

**Пример ответа:**

```json
  {
    "id": 1,
    "name": "TechSupplier Inc.",
    "contactPerson": "John Doe",
    "email": "john@techsupplier.com",
    "phone": "123-456-7890",
    "category": "Electronics",
    "status": "active"
  }
```
### :page_facing_up: Получить поставщика по ID

```bash
GET /suppliers/{id}
```
**Пример ответа:**

```json
{
  "id": 1,
  "name": "TechSupplier Inc.",
  "contactPerson": "John Doe",
  "email": "john@techsupplier.com",
  "phone": "123-456-7890",
  "category": "Electronics",
  "status": "active"
}
```
**Если не найден:**
```json
{
  "message": "Поставщик не найден"
}
```
### :heavy_plus_sign: Создать нового поставщика

```bash
POST /suppliers
```

**Пример тела запроса:**
```json
{
  "name": "New Supplier",
  "contactPerson": "Alice Smith",
  "email": "alice@newsupplier.com",
  "phone": "111-222-3333",
  "category": "Office Supplies",
  "status": "active"
}
```
**Пример ответа:**
```json
{
  "id": 2,
  "name": "New Supplier",
  "contactPerson": "Alice Smith",
  "email": "alice@newsupplier.com",
  "phone": "111-222-3333",
  "category": "Office Supplies",
  "status": "active"
}
```
### :pencil2: Обновить поставщика

```bash
PUT /suppliers/{id}
```
**Пример:**
```json
{
  "name": "Updated Supplier",
  "contactPerson": "Alice Smith",
  "email": "alice@updated.com",
  "phone": "000-111-2222",
  "category": "Electronics",
  "status": "inactive"
}
```

### :x: Удалить поставщика

```bash
DELETE /suppliers/{id}
```

**Ответ:**
```css
204 No Content
```
**Если не найден:**
```json
{
  "message": "Поставщик не найден"
}
```

### 📦 Получить товары поставщика

```bash
GET /suppliers/{id}/items
```

**Пример ответа:**
```json
  {
    "id": 1,
    "name": "Laptop",
    "article": "LP001",
    "category": "Electronics",
    "quantity": 50,
    "location": "Main Warehouse",
    "status": "available"
  }
```
**Ошибки:**
+ 404 Not Found: поставщик не найден

+ 503 Service Unavailable: сервис товаров недоступен