﻿
# Пояснення до практичих

---

## Практична 25

1. Які шаблони ви використали?
	- Singleton - клас DeliveryService.
	- Factory - клас ParcelFactory.
	- Strategy - інтерфейс IDeliveryStrategy, класи StandardDeliveryStrategy і ExpressDeliveryStrategy.
	- Observer - інтерфейс IObserver, класи CustomerObserver і ManagerObserver.
2. Яку проблему вирішує кожен обраний шаблон?
	- Singleton - забезпечує єдиний екземпляр служби доставки.
	- Factory - створює відповідний тип посилки залежно від її ваги.
	- Strategy - дозволяє змінювати алгоритм розрахунку вартості доставки без зміни основного коду служби.
	- Observer - автоматично повідомляє клієнта та менеджера про зміну статусу посилки.
3. Як ви перевірили правильність їхньої роботи?
	- Singleton - отримав екземпляр DeliveryService через Instance.
	- Factory - створив посилку вагою 3 кг і перевірив, що її тип — SmallParcel.
	- Strategy - використав ExpressDeliveryStrategy для розрахунку вартості доставки.
	- Observer - підписав клієнта й менеджера та змінив статус посилки, після чого перевірив повідомлення виводу.

---

1. Результат виконання програми

[result.txt](Output/result.txt)