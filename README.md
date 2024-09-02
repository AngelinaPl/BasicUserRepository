# Локальный запуск приложения:
- Установить Docker (если нет)
- Запустить Docker
- Открыть терминал в папке с docker-compose.yml
- Запустить команду ```docker-compose up```
- Сервис будет доступен по адресу http://localhost:5000/swagger/index.html

# UsersController

`UsersController` предоставляет API для управления пользователями. Контроллер использует MediatR для обработки запросов.

## Методы

### GetUserById

Получить пользователя по ID.

- **URL:** `GET /Users/{id}`
- **Ответы:**
  - `200 OK`: Пользователь успешно найден.
  - `404 Not Found`: Пользователь не найден.
  - `400 Bad Request`: Некорректный ID пользователя.

### GetAllUsers

Получить всех пользователей.

- **URL:** `GET /Users/GetAllUsers`
- **Ответы:**
  - `200 OK`: Пользователи успешно найдены.

### AddUser

Добавить нового пользователя.

- **URL:** `POST /Users/AddUser`
- **Ответы:**
  - `201 Created`: Пользователь успешно создан.
  - `400 Bad Request`: Некорректные данные пользователя.

### UpdateUser

Обновить данные пользователя.

- **URL:** `PUT /Users/UpdateUser`
- **Ответы:**
  - `204 No Content`: Пользователь успешно обновлен.
  - `404 Not Found`: Пользователь не найден.
  - `500 Internal Server Error`: Внутренняя ошибка сервера.

### DeleteUser

Удалить пользователя.

- **URL:** `DELETE /Users/DeleteUser`
- **Ответы:**
  - `204 No Content`: Пользователь успешно удален.
  - `404 Not Found`: Пользователь не найден.
  - `500 Internal Server Error`: Внутренняя ошибка сервера.


