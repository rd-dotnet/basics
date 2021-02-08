<img src="..\..\resources\EPAM_LOGO_Primary.png?raw=true" width="330" />

# События в .NET

#  Задание

## Офис

Написать программу, описывающую небольшой офис, в котором работают
сотрудники – объекты класса Person, обладающие полем имя (Name). Каждый
из сотрудников содержит пару методов:

1.  приветствие сотрудника, пришедшего на работу (принимает в качестве
    аргументов объект сотрудника и время его прихода). В зависимости от
    времени суток, приветствие может быть различным: до 12 часов –
    «Доброе утро», с 12 до 17 – «Добрый день», начиная с 17 часов –
    «Добрый вечер».

```
public void SayHello(Person person, DateTime time)
{
}
```

2.  прощание с ним (принимает только объект сотрудника).

```
public void SayBye(Person person)
{
}
```

Каждый раз при входе очередного сотрудника в офис, все пришедшие ранее
его приветствуют. При уходе сотрудника домой с ним также прощаются все
присутствующие. Вызов процедуры приветствия/прощания производить через
[групповые
делегаты](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/delegates/how-to-combine-delegates-multicast-delegates).
Факт прихода и ухода сотрудника отслеживается через [генерируемые им
события](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/events/how-to-publish-events-that-conform-to-net-framework-guidelines).

### Рекомендации

Событие прихода можно описать делегатом, передающим в числе параметров
значение DateTime описывающее время прихода сотрудника, например:

```
public delegate void PersonCameEventHandler(Person person, DateTime time);

public delegate void PersonCameEventHandler(Person person, DateTime time);
class Person
{
	…
	public event PersonCameEventHandler OnCame;
	…
	public void GoToOffice()
	{
		…
		OnCame?.Invoke(this, DateTime.Now);
	}
}
```

Событие ухода можно описать делегатом, передающим в качестве параметра
сотрудника, который ушёл, например:

```
public delegate void PersonLeaveEventHandler(Person person);

class Person
{
	…
	public event PersonLeaveEventHandler OnLeave;
	…
	public void GoHome()
	{
		…
		OnLeave?.Invoke(this);
	}
}
```

## Демонстрация работы офиса

Написать консольное приложение, которое будет демонстрировать работу
офиса при последовательном приходе и уходе сотрудников.

Пример:

<img src="task_media\media\image2.png" style="width:3.11458in;height:2.29745in" />
