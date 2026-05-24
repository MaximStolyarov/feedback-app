# 📩 Feedback App (Fullstack)
Fullstack приложение формы обратной связи.

## </> Технологии

### Frontend
- Angular 21

### Backend
- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- PostgreSQL
- Swagger


## 📝 Описание проекта

Приложение представляет собой форму обратной связи, в которой пользователь может отправить сообщение.

После отправки:
- данные сохраняются в PostgreSQL
- создаётся контакт (если он не существует)
- сообщение связывается с темой и контактом
- сервер возвращает сохранённое сообщение

## 🌐 API Endpoints

### Получить темы

GET /api/messages/themes


### Отправить сообщение

POST /api/messages


### Получить сообщение по ID

GET /api/messages/{id}

## 🖥️ Запуск проекта
### Backend
- В `appsettings.json` прописать строку подключения к PostgreSQL:
   ```json
   "DefaultConnection": "Host=localhost;Port=5432;Database=feedback_db;Username=postgres;Password=yourpassword"
   ```
Выполнить команды:
- cd feedback-backend
- dotnet run

#### Swagger:
https://localhost:7178/swagger

### Frontend
Выполнить команды:
- cd feedback-client
- npm install
- ng serve

Приложение откроется на `http://localhost:4200`
URL бэкенда задаётся в `feedback.service.ts`
