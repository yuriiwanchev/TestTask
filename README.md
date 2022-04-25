# Test Task  

Функционал разделён по слоям и назначению на сборки:
```

                        | <- ConsoleApp
Domain <- DataAccess <- | 
                        | <- RestApi
```

Domain - слой с базовыми моделями и интерфейсами.

DataAccess - слой с функционалом по работе с базой данных (Repository \ DAL).

RestApi - слой для взаимодействия с пользователями.

RestApi - слой для загрузки в БД сведений о курсах валют из открытого веб-сервиса.
 
---
○ Обработка ошибок

• При конфликтах и исключительных ситуациях библиотека выбрасывает типизированные "кастомные" исключения, определенные в ней.

• Ошибки, произошедшие внутри библиотеки логируются.

• Библиотека не имеет зависимости на конкретный логгер.

---

Консольное приложение
1.	Загрузка в БД сведений о курсах валют из открытого веб-сервиса http://www.cbr.ru/scripts/XML_daily.asp за N дней, начиная с текущего.
2.	N - задается в настройках приложения.
3.	При повторном получении данных за тот же день, ранее полученные данные — перезаписываются.
---
API
1.	GET /currencies — возвращает список курсов валют в JSON
3.	GET /currency/id — возвращает последний курс валюты на текущий день или последний имеющийся для переданного id
4.	API закрыто bearer авторизацией.



○ Функционал приложения:

• Сущности: Currency (дата, код валюты, курс), CurrencyData (все данные об валюте).

• У Currency код и курс это составной ключ. У CurrencyData ключ это parentCode.

• CRUD операции для работы с Currency

• CRUD операции для работы с CurrencyData




---

Также в приложении были реализованы unit тесты.

В качестве Ui используется Swagger Ui.

---
○ Стек:

• C#, .NET 5

• Postgress DB

• Moq, NUnit

• Microsoft ILogger, Serilog

• EF Core