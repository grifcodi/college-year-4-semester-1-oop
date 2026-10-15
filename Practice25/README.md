﻿
# Пояснення до практичих

---

## Практична 25

1. Які принципи SOLID були порушені у початковій версії коду?
	- SRP (Single Responsibility Principle) — клас BadOrderProcessor мав забагато обов’язків: зберігав товари, розраховував знижку, формував звіт, записував дані у файл і виводив повідомлення.
	- OCP (Open/Closed Principle) — правило розрахунку знижки було жорстко задане в коді, тому для зміни умов потрібно було редагувати наявний клас.
	- ISP (Interface Segregation Principle) — у початковій версії не було окремих інтерфейсів для різних обов’язків, що ускладнювало розділення та заміну компонентів.
	- DIP (Dependency Inversion Principle) — клас напряму залежав від конкретного способу збереження даних через File.WriteAllText, а не від абстракції.
2. Які конкретні зміни ви внесли, щоб усунути ці порушення?
	- SRP: розділив логіку на окремі класи: Order, ThresholdDiscountPolicy, ConsoleReportPrinter, FileOrderRepository та ConsoleNotificationService.
	- OCP: створив інтерфейс IDiscountPolicy, що дозволяє додавати нові правила знижок без зміни класу OrderProcessor.
	- ISP: розділив обов’язки між невеликими інтерфейсами IDiscountPolicy, IReportPrinter, IOrderRepository та INotificationService, щоб кожен компонент залежав лише від потрібної йому функціональності.
	- DIP: додав інтерфейси для друку звіту, збереження даних і сповіщень. OrderProcessor отримує необхідні залежності через конструктор.
---

1. Результат виконання програми

[result.txt](Output/result.txt)