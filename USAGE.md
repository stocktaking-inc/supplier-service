# 📦 Supplier Service — Usage Guide

Микросервис `supplier-service` управляет поставщиками в системе Inventory Management. Он позволяет выполнять CRUD-операции с поставщиками и получать список товаров от конкретного поставщика.



## :wrench: Установка

ляляля я Семён Лобанов, у меня бошка из картошки

## :closed_lock_with_key: Аутентификация

Все запросы защищены с помощью JWT.

## :blue_book: Эндпоинты

### :clipboard: Получить список всех поставщиков

**Пример ответа:**
```json
[
  {
    "id": 1,
    "name": "TechSupplier Inc.",
    "contactPerson": "John Doe",
    "email": "john@techsupplier.com",
    "phone": "123-456-7890",
    "category": "Electronics",
    "status": "active"
  }
]
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
  "code": "NOT_FOUND",
  "message": "Поставщик не найден"
}
```
### :heavy_plus_sign: Создать нового поставщика


### :x: Удалить поставщика

```bash
DELETE /suppliers/{id}
```

**Ответ:**
```c#
204 No Content
```
**Если не найден:**
```json
{
  "code": "NOT_FOUND",
  "message": "Поставщик не найден"
}
```

### 📦 Получить товары поставщика

```bash
GET /suppliers/{id}/items
```

**Пример ответа:**
```json
[
  {
    "id": 1,
    "name": "Laptop",
    "article": "LP001",
    "category": "Electronics",
    "quantity": 50,
    "location": "Main Warehouse",
    "status": "available"
  }
]
```
**Ошибки:**
+ 404 Not Found: поставщик не найден

+ 503 Service Unavailable: сервис товаров недоступен