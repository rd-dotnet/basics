<img src="..\..\resources\EPAM_LOGO_Primary.png?raw=true" width="330" />

# События в .NET

[1 Введение](#введение)

[2 Работа с событиями](#работа-с-событиями)

[2.1 Объявление события](#объявление-события)

[2.1.1 Безопасный вызов событий](#безопасный-вызов-событий)

[2.1.2 Модификаторы доступа и статичность события](#модификаторы-доступа-и-статичность-события)

[2.2 Объявление Обработчика события](#объявление-обработчика-события)

[2.2.1 Объявление обработчика события](#объявление-обработчика-события-1)

[2.2.2 Добавление и удаление обработчика события](#добавление-и-удаление-обработчика-события)

[3 Cтандартный шаблон .net для событий](#стандартный-шаблон-net-для-событий)

[3.1 Применение делегата EventHandler и класса EventArgs](#применение-делегата-eventhandler-и-класса-eventargs)

[3.1.1 Передача аргументов события. Класс EventArgs.](#передача-аргументов-события-класс-eventargs)

[3.1.2 Использование делагата EventHandler для объявления события](#использование-делагата-eventhandler-для-объявления-события)

[4 События vs Делегаты](#события-vs-делегаты)

[4.1 Зачем использовать события, если вместо их можно использовать делегаты](#зачем-использовать-события-если-вместо-их-можно-использовать-делегаты)

[4.2 Выбор между делегатом и событием](#выбор-между-делегатом-и-событием)

[Вопросы](#вопросы)

# Введение

Для начала давайте попробуем разобраться с тем что же такое события. На
самом деле события не несут никакого скрытого смысла и понимать их можно
в привычном контексте с аналогией в реальной жизни. Представьте, что Вы
сидите за рулем автомобиля перед красным сигналом светофора. Красный
свет меняется на желтый (это уже событие), затем на зеленый (это тоже
событие).

В языке программирования C\# все абсолютно идентично. С точки зрения
программы, событием называют сообщение, посланное объектом чтобы
проинформировать о совершении некоторого действия. Это действие может
быть вызвано взаимодействием с пользователем, например при нажатии
кнопки, или быть результатом некоторой логики программы, например,
изменение значения свойства. Класс, содержащий описание события
называется издателем (Publisher). Объект такого класса уведомляет другие
объекты подписавшиеся на событие, о том что это событие произошло.
Реакция на событие осуществляется с помощью так называемых обработчиков
события. Обработчик события – это обычный метод, который выполняет
некоторые действия в программе, в случае если состоялось
(сгенерировалось) событие. Подписчик (Subscriber) это объект, который
предоставляет обработчики событий. События позволяют издателю
(Publisher) уведомлять подписчиков (Subscribers) о возникновении
каких-либо ситуаций.

События работают в объединении с делегатами. Такое объединение позволяет
формировать списки (цепочки) обработчиков события (методов), которые
должны вызваться при вызове (запуске, генерировании) данного события.
Такой подход эффективен при написании больших программных систем,
поскольку он позволяет упорядочить большой сложный программный код в
котором очень легко допустить логическую ошибку.

И так кратко:

-   Класс издатель определяет в какой момент будет вызвано его событие.
    Подписчики определяют ответное действие, которое будет выполнено по
    наступлению события издателя.

-   На событие может подписаться несколько подписчиков. Подписчик может
    обрабатывать несколько событий от нескольких издателей.

-   Если у события несколько подписчиков, то при его возникновении
    происходит синхронный вызов обработчиков событий.

-   Для типа делегата, определяемого для события, тип возвращаемого
    значения должен быть void.

-   События основаны на делегатах. Делегаты поддерживают многоадресную
    рассылку, это обеспечивает поддержку нескольких подписчиков для
    любого источника событий.

События представляют собой специальный вид многоадресного делегата,
который можно вызвать только из класса или структуры, в которых он
объявлен (класс Publisher). Если другие классы или структуры
подписываются на событие, их методы обработчиков событий будут
вызываться, когда класс Publisher будет вызывать событие.

<https://docs.microsoft.com/ru-ru/dotnet/standard/events/>

# Работа с событиями

## Объявление события

События объявляются в классе с помощью ключевого слова event, после
которого указывается тип делегата, который представляет событие:

**\[&lt;спецификатор&gt;\] event &lt;делегат&gt; &lt;имя события&gt;;**

Для примера добавим класс издателя события - Worker, который
подразумеваем будет выполнять некоторую работу, по окончанию выполнения
которой будет генерировать событие Completed, передавая вместе с ним
информацию о количестве выполненной работы и дате её выполнения.

Для реализации этого примера создадим делегат WorkerHandler принимающий
два аргумента: количество выполненной работы и время выполнения.
Возвращаемый тип у делегата – void. Это основное требование к типу
делегата, так как событие обрабатывает список обработчиков.

Далее в классе Worker создаём публичное событие Completed с типом
WorkerHandler и внутренне свойство класса, которое в будущем будет
содержать количество выполненной работы.

```
delegate void WorkerHandler(int amountWork, DateTime completedDate);
class Worker
{
	int AmountWork { get; set; }
	public event WorkerHandler Completed;
}
```

Для генерации события и вызова подписавшихся на него обработчиков
необходимо добавить отдельный метод с названием OnCompleted.
Рекомендуется использовать именно такой шаблон именования данного
метода: **приставка On + название события**. Метод также рекомендуют
обозначать как virtual для возможности переопределения в дочерних
классах, вместе с модификатором protected.

В результате в класс будет добавлен следующий код:

```
protected virtual void OnCompleted()
{
	WorkerHandler handler = Completed;
	if (handler != null)
	{
		handler(this.AmountWork, DateTime.Now);
	}
}
```

### Безопасный вызов событий

В коде метода OnCompleted следует внимательно обратить на строку где
проверяется на null сам handler. Этим мы проверяем наличие методов
обратчиков от подписчиков на данное событие. В случае если обработчики
отсутствуют, переменная handler будет содержать null.

Существует два способа записи кода, для выполнения данной проверки:

```
// Способ №1
if (handler != null)
{
	handler(this.AmountWork, DateTime.Now);
}
// Способ №2
handler?.Invoke(this.AmountWork, DateTime.Now);
```

### Модификаторы доступа и статичность события

Событие может быть объявлено как статическое событие с помощью ключевого
слова static. Это делает событие доступным для вызывающих объектов в
любое время, даже если экземпляр класса не существует.

Событие может быть помечено как виртуальное событие с помощью ключевого
слова virtual. Это позволяет производным классам переопределять
поведение события с помощью ключевого слова override. Событие,
переопределяющее виртуальное событие, также может быть запечатанным
(sealed), что указывает, что для производных классов оно больше не
является виртуальным. И наконец, можно объявить событие абстрактным
(abstract), что означает, что компилятор не будет создавать блоки
доступа к событиям add и remove.

Событие можно объявлять в интерфейсе.

<https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/event>

## Объявление Обработчика события

### Объявление обработчика события

Обработчики событий - это именно то, что выполняется при вызове событий.
Нередко в качестве обработчиков событий применяются методы. Каждый
обработчик событий по списку параметров и возвращаемому типу должен
соответствовать делегату, который представляет событие.

Пример создания класса с методом обработчика:

```
class Manager
{
	public void WorkCompleted(int amountWork, DateTime completedDate)
	{
		Console.WriteLine("Work completed." +
			$"Amount work: {amountWork}. Date: {completedDate.Date}");
	}
}
```

Сигнатура объявленного метода WorkCompleted соответствует типу делегата
WorkerHandler приведённого ранее.

### Добавление и удаление обработчика события

Для добавления обработчика события применяется операция +=. Для одного
события можно установить несколько обработчиков и потом в любой момент
времени их удалить. Для удаления обработчиков применяется операция -=.

В простейшем случае, это выглядит следующим образом:

```
var worker = new Worker();
var manager = new Manager();

// Подписались на событие
worker.Completed += manager.WorkCompleted;

// Отписались от события
worker.Completed -= manager.WorkCompleted;
```

В качестве обработчиков могут использоваться не только обычные методы,
но также делегаты, анонимные методы и лямбда-выражения.

Пример добавления обработчика представленного ввиде лямбда-выражения:

```
worker.Completed += (amountWork, dateComleted) =>
{
	Console.WriteLine("Work completed!");
};
```

# Cтандартный шаблон .net для событий

## Применение делегата EventHandler и класса EventArgs

Теоритически для объявления события можно использовать любой делегат. Но
согласно стандартному шаблону определенного .NET Framework нужно
использовать один из специально предопределенных делегатов EventHandler
предназначенных именно для событий.

```
public delegate void EventHandler(object sender, EventArgs e);
public delegate void EventHandler<T>(object sender, T e);
```

Этот шаблон определили с целью обеспечения согласованности кода .NET
Framework и пользовательского кода.

Стандартный делегат для событий содержит два аргумента: sender –
отправитель события, e – аргументы события. Для аргумента sender по
соглашению следует использовать тип System.Object, даже если вероятно
вам известен более точный производный тип. Вторым аргументом является
тип производный от System.EventArgs, в котором можно указать необходимые
параметры события, которыми могут воспользоваться подписчики события при
вызове обработчика на своей стороне.

### Передача аргументов события. Класс EventArgs.

Данные, связанные с событием, могут быть предоставлены с помощью класса
данных события. .NET предоставляет множество классов данных событий,
которые можно использовать в приложениях. Если требуется создать
пользовательский класс данных события, создайте класс, производный от
класса EventArgs, а затем укажите все члены, необходимые для передачи
данных, связанных с событием.

Используя пример в предыдущих главах, класс аргументов события будет
выглядеть следующим образом:

```
class WorkInfoEventArgs : EventArgs
{
	public int AmountWork { get; }
	public DateTime CompletedDate { get; }

	public WorkInfoEventArgs(int amountWork, DateTime completedDate)
	{
		AmountWork = amountWork;
		CompletedDate = completedDate;
	}
}
```

В большинстве случаев следует использовать схему именования .NET и
завершать имя класса данных события ключевым словом **EventArgs**.
Именуется класс осмысленно в соответствии с содержащейся в нем
информацией (а не событием, для которого он будет использоваться).

**System.EventArgs** – предопределенный класс, имеющий только
статическое свойство Empty. EventArgs.Empty.

Также рекомендуется делать свойства в типе аргумента события
неизменяемыми (только для чтения). Таким образом, один подписчик не
сможет изменить значения до того, как их увидит другой подписчик.

### Использование делагата EventHandler для объявления события

Пример ранее приведенного класса Worker, используя стандартный делегат
EventHandler.

```
public class Worker
{
	int AmountWork { get; set; }
	public event EventHandler<WorkInfoEventArgs> Completed;

	protected virtual void OnCompleted()
	{
		EventHandler<WorkInfoEventArgs> handler = Completed;
		var args = new WorkInfoEventArgs(this.AmountWork, DateTime.Now);
		handler?.Invoke(this, args);
	}

	public void DoWork()
	{
		Console.WriteLine("Making some work...");
		AmountWork = 6;

		Console.WriteLine("Work completed");
		// Вызов события завершения работы.
		OnCompleted();
	}
}
```

В примере была использована generic версия делегата
EventHandler&lt;T&gt; с параметром класса WorkInfoEventArgs, который
представлен в предыдущей главе.

Перед вызовом события, создаётся экземпляр класса WorkInfoEventArgs с
данными о событии для передачи вторым аргументом в методе Invoke.

В случае если тип события не требует дополнительных аргументов,
необходимо предоставить оба аргумента для делегата. Существует
специальное значение EventArgs.Empty, которое следует использовать для
обозначения что событие не содержит никаких дополнительных сведений. В
таком случае событие должно быть представлено делегатом EventHandler
параметризированный стандартным классом EventArgs.

```
public class Worker
{
	public event EventHandler<EventArgs> Completed;

	protected virtual void OnCompleted()
	{
		EventHandler<EventArgs> handler = Completed;
		handler?.Invoke(this, EventArgs.Empty);
	}
	// .. other code
}
```

Класс Manager с объявлением обработчика на событие Completed будет
выглядеть следующим образом:

```
public class Manager
{
	public void WorkCompleted(object sender, WorkInfoEventArgs args)
	{
		Console.WriteLine("Work completed." +
			$"Amount work: {args.AmountWork}. Date: {args.CompletedDate.Date}");
	}
}
```

Метод WorkCompleted соответствует сигнатуре делегата
EventHandler&lt;WorkInfoEventArgs&gt;.

# События vs Делегаты

## Зачем использовать события, если вместо их можно использовать делегаты

Основное назначение событий – предотвращение влияния подписчиков друг на
друга. Если в коде убрать ключевое слово event, чтобы Comlpeted (в
классе Worker) стало обычным полем делегата, результаты будут теми же
самыми. Однако класс станет менее надежным, потому что подписчики смогут
предпринимать следующие действия, влияя друг на друга:

\- заменять других подписчиков, переустанавливая свойство (вместо
операции +=)

\- очищать всех подписчиков (устанавливая поле в null)

\- выполнять групповую рассылку другим подписчикам путем вызова
делегата.

Пробежимся по отличию event'а в классе от публичного поля делегатного
типа.

Рассмотрим случай, когда event реализован «по умолчанию», то есть, с
неявным делегатным полем. Отличие состоит в том, что для делегатного
поля у вас полный доступ к нему. Вы можете — также и снаружи класса! —
разобрать MulticastDelegate на части и собрать новый, вы можете заменить
его на свой или присвоить ему null, вы можете его вызвать, можете его
скопировать себе в переменную. У вас есть полный доступ, как и к любому
публичному полю. (Это, разумеется, вопиющим образом нарушает
инкапсуляцию.)

Для event'а, вы можете лишь написать instance.Event += handler и
instance.Event -= handler, что отображается на функции add и remove,
которые в свою очередь снова-таки вызывают += и -= для автоматически
реализованного делегата. Никакого другого доступа у вас нету. Внутри
класса вы, однако, можете получить на чтение значение этого делегата,
используя имя event'а.

### 

## Выбор между делегатом и событием

Часто возникает вопрос о том, что выбрать: структуру на
основе delegates или на основе events. Это сложный вопрос, так как эти
две возможности языка очень похожи. Более того, события основаны на тех
же средствах языка, которые обеспечивают поддержку делегатов.

1.  Важным фактором является обяательность наличия подключенного
    подписчика. Если ваш код должен вызывать код, предоставленный
    подписчиком, следует использовать структуру на основе
    делегатов. Если код может выполнить все задачи, не вызывая
    подписчики, следует использовать структуру на основе событий.

2.  Еще одним аспектом является прототип метода, который требуется для
    метода делегата. Как вы уже видели, все делегаты, используемые для
    событий, имеют тип возвращаемого значения void. В случае если в
    методе нужно использовать возвращаемое значение, то предпочтение
    скорее всего следует отдать обычным делегатам.

<https://docs.microsoft.com/en-us/dotnet/csharp/distinguish-delegates-events>

### Вопросы

1.  Что такое событие?

2.  Какие предопределенные типы существуют в .Net для работы с
    событиями?

3.  Какой используется тип возвращаемого значения для делегата события?

4.  Как вызывается список обработчиков событий - синхронно или
    асинхронно?

5.  Что необходимо сделать перед непосредственным вызовом события?

6.  Какое существует требование к методу-обработчика для события?

7.  Какие образом можно подписаться/отписаться от события?

8.  Назовите аргументы метода описываемым стандартным делегатом
    EventHandler для событий? Для чего они предназначены?

9.  Какие существуют рекомендации создания класса для передачи
    аргументов событию?

10. В чём состоит ключевая особенность использования событий вместо
    обычных делегатов?