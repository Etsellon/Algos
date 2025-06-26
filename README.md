# Тестовое задание на позицию **Middle c# developer**.

Реализован базовый CRUD-интерфейс для управления компаниями. Добавлены представления, стили, и контроллер с поддержкой:
- Просмотра списка компаний с пагинацией.
- Создания, редактирование и удаление компаний.
- Просмотра деталей компании.
- Страница "О нас" с контактной информацией.

Проект выполнен с использованием чистой архитектуры. Модели разделены по слоям:
- Algos.Core - ядро, содержит доменные модели, интерфейсы и абстракции
- Algos.Application - реализует use cases и бизнес сценарии
- Algos.DataAccess - база данных. Реализует интерфейсы из Core
- Algos.SharedKernel - содержит общие сущности и контракты
- Algos.Web - ASP .NET Core MVC, работает через Application слой и отображает данные пользователю.

# Быстрый запуск

1. Подымаем postgres в докере
```bash
cd Algos.Web
docker compose up -d
```

2. Создание миграции (InitialMigration уже была создана)
```bash
dotnet ef migrations add init -s ./Algos.Web -p ./Algos.DataAccess
```
 
 3. Применение миграции
```bash
dotnet ef database update -s ./Algos.Web -p ./Algos.DataAccess
```

4. Запуск приложения
```bash
dotnet run --project Algos.Web
```
