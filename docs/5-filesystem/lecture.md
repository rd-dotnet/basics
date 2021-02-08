<img src="..\..\resources\EPAM_LOGO_Primary.png?raw=true" width="330" />

# Работа с файловой системой в .NET

[1 Введение](#введение)

[2 Работа с дисками](#работа-с-дисками)

[3 Работа с каталогами](#работа-с-каталогами)

[3.1 Directory](#directory)

[3.2 DirectoryInfo](#directoryinfo)

[4 Работа с файлами](#работа-с-файлами)

[4.1 File](#file)

[4.2 FileInfo](#fileinfo)

[5 Классы для чтения и записи](#классы-для-чтения-и-записи)

[5.1 FileStream](#filestream)

[5.2 StreamReader](#streamreader)

[5.3 StreamWriter](#streamwriter)

[5.4 BinaryWriter](#binarywriter)

[5.5 BinaryReader](#binaryreader)

[6 Environment](#environment)

[7 IDisposable](#idisposable)

[8 Вопросы](#вопросы)

# Введение

Множество задач в программировании связаны с работой с каталогами и
файлами. Часто требуется хранить информацию в файловой системе, для чего
требуется создавать различные файлы и директории, записывать в них
информацию, или что-то читать из них.

.NET предоставляет множество возможностей по взаимодействию с файловой
системой. Можно получать информацию о дисках и директориях, читать и
записывать файлы, получать информацию о текущем окружении (имя
пользователя, имя машины и прочее)

# Работа с дисками

Начнем знакомство с файловой системой с самого верхнего уровня – с
дисков. Для работы с ними существует класс DriveInfo. Он содержится в
пространстве имен **System.IO**.

Этот класс содержит несколько важных методов, при помощи которых можно
получать множество полезной информации о дисках. **DriveInfo** можно
использовать для того, чтобы определить доступность дисков, их емкость и
свободное место.

Рассмотрим некоторые важные методы и свойства.

**Методы:**

-   **GetDrives()** - Возвращает имена всех логических дисков на
    компьютере

**Свойства:**

-   **AvailableFreeSpace** - Указывает объем доступного свободного места
    на диске в байтах.

-   **DriveFormat** - Получает имя файловой системы, например NTFS или
    FAT32.

-   **DriveType** - Получает тип диска, такой как компакт-диск, съемный,
    сетевой или жесткий.

-   **IsReady** - Получает значение, указывающее, готов ли диск.

-   **Name** - Возвращает имя диска, например C:\\.

-   **RootDirectory** - Возвращает корневой каталог диска.

-   **TotalFreeSpace** - Возвращает общий объем свободного места,
    доступного на диске, в байтах.

-   **TotalSize** - Получает общий размер места для хранения на диске в
    байтах.

-   **VolumeLabel** - Получает или задает метку тома диска.

При помощи следующего примера можно получить информацию, о дисках,
видимых системой, а также некоторые их свойства:

```
DriveInfo[] drivers = DriveInfo.GetDrives();
foreach (DriveInfo drive in drivers)
{
	Console.WriteLine("Имя: {0}", drive.Name);
	Console.WriteLine("Тип: {0}", drive.DriveType);
	Console.WriteLine("Готовность: {0}", drive.IsReady);
}
```

<img src="lecture_media\media\image3.png" style="width:2.04015in;height:1.68116in" alt="Screen Clipping" />

# Работа с каталогами

Следующий важный этап работы с файловой системой – каталоги. Классы
[**Directory**](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.directory?view=netframework-4.8)
и
[**DirectoryInfo**](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.directoryinfo?view=netframework-4.8)
содержат в себе методы, позволяющие создавать, копировать, получать
важную информацию, а так же удалять каталоги. Их различие в том, что
**Directory** – статический класс, предоставляющий статические методы
для работы с директориями. **DirectoryInfo** – требует создания объекта
и предоставляет информацию о каком-либо конкретном каталоге.

## Directory 

Рассмотрим подробнее методы класса Directory:

**Методы**

-   **CreateDirectory(String)** - Создает все каталоги и подкаталоги по
    указанному пути, если они еще не существуют.

-   **Delete(String)** - Удаляет пустой каталог по заданному пути.

-   **Exists(String)** - Определяет, указывает ли заданный путь на
    существующий каталог на диске.

-   **GetCurrentDirectory()** - Получает текущий рабочий каталог
    приложения.

-   **GetDirectories(String)** - Возвращает имена подкаталогов (включая
    пути) в указанном каталоге.

-   **GetFiles(String)** - Возвращает имена файлов (с указанием пути к
    ним) в указанном каталоге.

-   **Move(String, String)** - Перемещает файл или каталог со всем его
    содержимым в новое местоположение.

**Пример создания каталога:**

```
Directory.CreateDirectory(filename);
Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(filename));
```

## DirectoryInfo

Класс DirectoryInfo содержит похожие на Directory методы и свойства:

**Методы и свойства**

-   **Create()** - Создает каталог.

-   **Delete()** - Удаляет каталог если он пуст.

-   **Exists**- Свойство, позволяет узнать, существует ли каталог.

-   **GetDirectories()** - Возвращает подкаталоги текущего каталога.

-   **GetFiles()** - Возвращает список файлов текущего каталога.

-   **MoveTo(String)** - Перемещает экземпляр DirectoryInfo и его
    содержимое в местоположение, на которое указывает новый путь.

-   **Parent -** Получает родительский каталог.

Пример создания и удаления каталога:

```
DirectoryInfo di = new DirectoryInfo(@"c:\MyDir");
// Пытаемся создать директорию.
di.Create();
Console.WriteLine("The directory was created successfully.");
// Удаляем ее.
di.Delete();
Console.WriteLine("The directory was deleted successfully.");
```

# Работа с файлами

Подобно паре классов **Directory**/**DirectoryInfo** существует пара
классов
[**File**](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.file?view=netframework-4.8)
и
[**FileInfo**](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.fileinfo?view=netframework-4.8)
для работы с файлами. Они позволяют создавать, удалять, перемещать
файлы, получать их свойства и многое другое.

## File

Класс
[**File**](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.file?view=netframework-4.8)
можно использовать для стандартных операций копирования, перемещения,
переименования, создания, открытия, удаления и добавления в один файл за
раз. Класс **File** предоставляет статические методы для создания,
копирования, удаления, перемещения и открытия одного файла.

**Методы**

-   **Copy(String, String)** - Копирует существующий файл в новый файл.
    Перезапись файла с тем же именем не разрешена.

-   **Create(String)** - Создает или перезаписывает файл в указанном
    пути.

-   **Exists(String)** - Определяет, существует ли заданный файл.

-   **Move(String, String)** - Перемещает заданный файл в новое
    местоположение и разрешает переименование файла.

Пример создания файла и записи в него:

```
if (!File.Exists(filename))
{
	// Создаем файл для записи.
	using (StreamWriter sw = File.CreateText(filename))
	{
		sw.WriteLine("Hello there");
	}
}
```

## FileInfo

Класс
[**FileInfo**](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.fileinfo?view=netframework-4.8)
предоставляет свойства и методы экземпляра для создания, копирования,
удаления, перемещения и открытия файлов. Его методы и свойства так же
очень похожи на класс **File.**

**Методы и свойства**

-   **Length** - Получает размер текущего файла в байтах.

-   **Name** - Возвращает имя файла.

-   **DirectoryName -** Получает строку, представляющую полный путь к
    каталогу.

-   **Exists** - Получает значение, показывающее, существует ли файл.

-   **Extension -** Получает строку, содержащую расширение файла.

-   **Create() -** Создает файл.

-   **Delete()** - Удаляет файл без возможности восстановления

-   **MoveTo(String) -** Перемещает заданный файл в новое местоположение
    и разрешает переименование файла.

-   **CopyTo(String)** - Копирует существующий файл в новый файл и
    запрещает перезапись существующего файла.

Рассмотрим пример создания и записи текста в файл с использованием
класса FileInfo

```
var fileInfo = new FileInfo(filename);
// Создаем файл для записи.
using (StreamWriter sw = fileInfo.CreateText())
{
	sw.WriteLine("Hello there");
}
```

# Классы для чтения и записи

В .NET Существует целый набор классов предназначенных для чтения и
записи в файлы. Все они имеют немного отличающиеся друг от друга цели и
логику работы. Эти файлы позволяют работать как с текстовыми данными,
так и с бинарным форматом. Рассмотрим их подробнее.

## FileStream 

Класс
[**FileStream**](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.filestream?view=netframework-4.8)
предоставляет возможность чтения и записи как в текстовые так и в
бинарные файлы. Экземпляры этого класса можно создавать как напрямую
через конструкторы, так и через некоторые методы класса **File,**
например File.OpenRead(filename).

Рассмотрим подробнее методы чтения и записи, а так же их параметры:

-   **Read(Byte\[\] array, Int32 offset, Int32 count) -** Выполняет
    чтение блока байтов из файла в массив байт.

    -   **array Byte\[\] -** массив в который будут помещены считанные
        байты.

    -   **offset Int32 -** Смещение в байтах в массиве array, в который
        будут помещены считанные байты.

    -   **count Int32** - Максимальное число байтов, которые будут
        считаны.

<!-- -->

-   **Write (byte\[\] array, int offset, int count) -** записывает в
    файл данные из массива байтов.

    -   **array Byte\[\] -** Массив, предназначенный для записи.

    -   **offset Int32 -** Смещение байтов (начиная с нуля) массива
        array, с которого начинается копирование байтов в поток.

    -   **count Int32 -** Максимальное число байтов для записи.

Пример записи и чтения из файла:

```
string text = "Some text to write";
//Создаем файл с использование FileStream.
using (FileStream fileStream = File.Create(filename))
{
	fileStream.Write(new UTF8Encoding().GetBytes(text), 0, text.Length);
}

//Открываем FileStream для чтения из файла.
using (FileStream fileStream = File.OpenRead(filename))
{
	var buffer = new byte[1024];
	var encoding = new UTF8Encoding();
	while (fileStream.Read(buffer, 0, buffer.Length) > 0)
	{
		Console.WriteLine(encoding.GetString(buffer));
	}
}
```

**Методы и свойства**

-   **Append** – Открывает существующий, или создает. Если файл
    существует, то помещает новый текст в конец файла.

<!-- -->

-   **Create** – Создает новый файл или перезаписывает старые.

-   **CreateNew** – создает новый файл, если такой файл уже существует,
    то выбрасывает IOException

-   **Open** – открывает существующий файл. Если файл не существует –
    вызывается исключение.

-   **OpenOrCreate** – открывает файл или создает новый

-   **Truncate** – открывает существующий файл, и очищает его. Новый не
    создается

Также следует помнить, что при создании объекта FIleStream при помощи
класса File нужно указывать параметр **FileMode,** который отвечает за
режим открытия.

```
string filename = @"c:\Users\MyFile.txt";
FileStream fs = File.Open(filename, FileMode.OpenOrCreate);
```

В приведенном примере используется FileMode.OpenOrCreate. Это означает,
что файл будет открыт либо создан, если его не существует.

## StreamReader

Поскольку для работы с текстовыми файлами не очень удобно применять
класс **FileStream,** в пространстве имен **System.IO** существуют
классы
[**StreamReader**](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.stringreader?view=netframework-4.8)
и
**[StreamWriter](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.stringwriter?view=netframework-4.8).**

Класс
[**StreamReader**](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.stringreader?view=netframework-4.8)
удобно использовать для чтения из текстовых файлов. Он считывает символы
из потока байтов в определенной кодировке.

Рассмотрим его методы и свойства:

**Методы и свойства**

-   **Close()** - Закрывает объект StreamReader и основной поток и
    освобождает все системные ресурсы, связанные с устройством чтения.

-   **Peek()** - Возвращает следующий доступный символ, но не использует
    его.

-   **Read()** - Выполняет чтение следующего символа из входного потока
    и перемещает положение символа на одну позицию вперед.

-   **ReadLine()** - Выполняет чтение строки символов из текущего потока
    и возвращает данные в виде строки.

-   **ReadToEnd()** - Считывает все символы, начиная с текущей позиции
    до конца потока.

В приведенном примере показано чтение построчный вывод в консоль
содержимого текстового файла:

```
string path = "D://MyWonderfulProject//test.txt";
using (StreamReader sr = new StreamReader(path))
{
	string line;
	while ((line = sr.ReadLine()) != null)
	{
		Console.WriteLine(line);
	}
}
```

## StreamWriter

Класс
**[StreamWriter](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.stringwriter?view=netframework-4.8)**
используется для записи в текстовые файлы. Этот класс предназначен для
вывода символов в определенной кодировке, тогда как классы, производные
от Stream, предназначены для ввода и вывода байтов.

**Методы и свойства**

-   **Close()** - Закрывает текущий объект StreamWriter и базовый поток.

-   **Write(String)** - Записывает в поток строку.

-   **WriteLine(Char\[\])** - Записывает в текстовый поток массив
    символов, за которыми следует признак конца строки.

Получение имен всех каталогов в корне диска С и их запись в файл

```
string path = "D://MyWonderfulProject//test.txt";
//Получаем директории, расположенны на диске C://
DirectoryInfo[] cDirs = new DirectoryInfo(@"C:\").GetDirectories();
//Запишем полученные имена в файл
using (StreamWriter sw = new StreamWriter(path))
{
	foreach (DirectoryInfo dir in cDirs)
	{
		sw.WriteLine(dir.Name);
	}
}
```

## BinaryWriter

Класс
**[BinaryWriter](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.binarywriter?view=netframework-4.8)
з**аписывает примитивные типы в двоичный поток и поддерживает запись
строк в заданной кодировке. Он предоставляет методы, упрощающие запись
примитивных типов данных в поток. Например, можно использовать
метод Write для записи логического значения в поток в виде однобайтового
значения. Класс включает методы записи, поддерживающие различные типы
данных.

**Методы и свойства**

-   **Close()** - Закрывает текущий BinaryWriter и базовый поток.

-   **Write(String)** - Записывает в текущий поток строку, предваряемую
    ее длиной, используя текущую кодировку BinaryWriter, и перемещает
    позицию в потоке вперед в соответствии с используемой кодировкой и
    количеством записанных в поток символов.

Пример записи различных типов данных в файл:

```
string path = "D://MyWonderfulProject//test.txt";
using (BinaryWriter writer = new BinaryWriter(File.Open(path,
	FileMode.Create)))
{
	writer.Write(1.250F);
	writer.Write(@"c:\Temp");
	writer.Write(10);
	writer.Write(true);
}
```

В самом файле данные будут представлены следущим образом:

<img src="lecture_media\media\image4.png" style="width:3.22917in;height:2.27083in" />

## BinaryReader

Класс
**[BinaryReader](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.binaryreader?view=netframework-4.8)**
нужен для чтения данных в бинарном формате. Класс BinaryReader
предоставляет методы, упрощающие чтение примитивных типов данных из
потока. Например, можно использовать метод ReadBoolean для считывания
следующего байта в качестве логического значения.

**Методы и свойства**

-   **Close()** - Закрывает текущий поток чтения и связанный с ним
    базовый поток.

-   **ReadBoolean()** - Считывает значение Boolean из текущего потока и
    перемещает текущую позицию в потоке на один байт вперед.

-   **ReadByte()** - Считывает из текущего потока следующий байт и
    перемещает текущую позицию в потоке на один байт вперед.

-   **ReadInt32()** - Считывает целое число со знаком длиной 4 байта из
    текущего потока и перемещает текущую позицию в потоке на четыре
    байта вперед.

-   **ReadDouble()** - Считывает число с плавающей запятой длиной 8 байт
    из текущего потока и перемещает текущую позицию в потоке на восемь
    байт вперед.

-   **ReadString()** - Считывает строку из текущего потока. Строка
    предваряется значением длины строки, которое закодировано как целое
    число блоками по семь битов.

Пример чтения, записанного ранее файла и его вывода на консоль:

```
string path = "D://MyWonderfulProject//test.txt";
if (File.Exists(path))
{
	float aspectRatio;
	string tempDirectory;
	int autoSaveTime;
	bool showStatusBar;

	using (BinaryReader reader = new BinaryReader(File.Open(path,
	FileMode.Open)))
	{
		aspectRatio = reader.ReadSingle();
		tempDirectory = reader.ReadString();
		autoSaveTime = reader.ReadInt32();
		showStatusBar = reader.ReadBoolean();
	}

	Console.WriteLine("Aspect ratio set to: " + aspectRatio);
	Console.WriteLine("Temp directory is: " + tempDirectory);
	Console.WriteLine("Auto save time set to: " + autoSaveTime);
	Console.WriteLine("Show status bar: " + showStatusBar);
}
```

В результате на консоль будут выведены данные:

<img src="lecture_media\media\image5.png" style="width:6.5in;height:3.96181in" />

# Environment 

Класс
[Environment](https://docs.microsoft.com/ru-ru/dotnet/api/system.environment?view=netframework-4.8)
позволяет получить различную информацю, относящуюся к операционной
системе

**Методы и свойства**

-   **CurrentDirectory** - Возвращает или задает полный путь к текущей
    рабочей папке.

-   **OSVersion** - Возвращает объект OperatingSystem, который содержит
    идентификатор текущей платформы и номер версии.

-   **SystemDirectory** - Возвращает полный путь к системному каталогу.

-   **UserName** - Возвращает имя пользователя, сопоставленное с текущим
    потоком.

-   **MachineName** - Возвращает имя NetBIOS данного локального
    компьютера.

Пример использования:

```
Console.WriteLine("OSVersion: {0}", Environment.OSVersion.ToString());
Console.WriteLine("Version: {0}", Environment.Version.ToString());
```

Результат:

<img src="lecture_media\media\image6.png" style="width:3.94792in;height:0.61458in" />

# IDisposable

При работе с файлами задействуются неуправляемые ресурсы (например -
дескриптор открытого файла в операционной системе). .NET Framework
понятия не имеет о том, что происходит там, где его нет. А если он
ничего об этом не знает, он не освободит память. Есть два пути решения
этой проблемы при работе с фалами –
[Dispose](https://docs.microsoft.com/ru-ru/dotnet/standard/garbage-collection/implementing-dispose)
и
[Using](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/language-specification/statements#the-using-statement)

Можно явно вызывать Dispose() при завершени работы с фалом:

```
BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create));
writer.Dispose();
```

Однако, это не всегда удобно и часто приводит к проблемам, особенно,
если забыть это сделать.

Интерфейс **IDisposable** предоставляет механизм для освобождения
неуправляемых ресурсов. А оператор **using -** Предоставляет удобный
синтаксис, обеспечивающий правильное использование объектов IDisposable.

Конструкции **Using** является «синтаксическим сахаром», то есть она
будет преобразована на этапе компиляции в блок try-finally с
автоматическим вызовом Dispose внутри.

```
// До
using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.Create)))
{
	binaryWriter.Write("text");
}

//После
BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.Create));
try
{
	binaryWriter.Write("text");
}
finally
{
	if (binaryWriter != null)
	{
		((IDisposable)binaryWriter).Dispose();
	}
}
```

# Вопросы

1.  Для чего используется класс Directory и чем от отличается от
    DirectoryInfo?

2.  Для чего используется класс File и чем он отличается от FileInfo?

3.  Для чего используется класс FileStream и как с ним работать?

4.  Чем класс StreamReader отличается от FileRStream и BinaryReader?

5.  Как открыть файл для добавления в него контента используя
    StreamWriter?

6.  Как открыть файл только для чтения?

7.  Как считать все поддиректории?
