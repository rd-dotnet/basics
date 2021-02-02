using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    // 0. Что такое делегат
    class Program
    {
        // 1. Объявляем тип делегата
        /// <summary>Наш делегат!</summary>
        public delegate void PrintMessageDelegate(string message);

        public delegate object ReturnObjectDelegate();

        public static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            // 2. Создаем объект делегата. Несколько синтаксисов
            PrintMessageDelegate printMessageDelegate = PrintMessage;
            //PrintMessageDelegate printMessageDelegate = new PrintMessageDelegate(PrintMessage);
            //printMessageDelegate = (inputMessage) =>
            //{
            //    Console.WriteLine(inputMessage);
            //    Console.WriteLine(inputMessage);
            //};
            // 2.1. Основные свойства делегата
            Console.WriteLine(printMessageDelegate.Method);
            Console.WriteLine(printMessageDelegate.Target);

            // 3. Вызов делегата
            printMessageDelegate("hello world!");
            // 3.1 Элвис-оператор
            printMessageDelegate?.Invoke("123");

            // 4. Использование стандартных делегатов
            // 		Action и Func<T>
            Action<string> action = PrintMessage;
            action("hello from Action");
            //Func<string> func = GetString;
            //Console.WriteLine(func());

            // 4.1 Для чего создавать кастомные делегаты?
            // 1. Action<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int,> action;
            // 2. out, ref, params.
            // 3. Func<T, T, int> x Comparison<T> 
            //Array.Sort(Comparison<T>)

            // 5. Delegate vs MulticastDelegate vs delegate 
            // Delegate
            // MulticastDelegate
            // PrintMessageDelegate : MulticastDelegate
            var delegateTypes = new List<Type>
            {
                // наши кастомные типы делегатов
                typeof(PrintMessageDelegate),
                typeof(ReturnObjectDelegate),
                // предопределенные типы делегатов
                typeof(Action),
                typeof(Predicate<string>),
                typeof(Func<bool>),
                typeof(Comparison<int>),
                typeof(object),
                typeof(int),
                typeof(Type)
            };
            foreach (var delegateType in delegateTypes)
            {
                var inheritedFromMulticastDelegate = delegateType.IsSubclassOf(typeof(MulticastDelegate));
                Console.WriteLine($"{delegateType.Name} subclass of {nameof(MulticastDelegate)}: {inheritedFromMulticastDelegate}");
            }

            // 6. Делегаты с несколькими методами возвращают 
            //    значение только последнего вызываемого метода
            //printMessageDelegate("123");
            //printMessageDelegate += (str) => throw new Exception();
            //printMessageDelegate += (str) => Console.WriteLine(str + str);
            //printMessageDelegate("321");
            Func<int> func = () => 1;
            func += () => 2;
            func += GetInt;

            //func -= GetInt;

            foreach (Func<int> funcInt in func.GetInvocationList())
            {
                Console.WriteLine(funcInt());
            }

            // 7. А для чего всё это надо?
            // LINQ.Select
            var intArray = new int[] { 1, 2, 3, 4, 5, 6 };
            var stringEnumerable = intArray.Select(i => i.ToString());
            //var stringEnumerable = intArray.Select(GetStringFromInt); - то же самое

            foreach (var stringItem in stringEnumerable)
            {
                Console.WriteLine(stringItem + " " + stringItem.GetType());
            }
        }

        public static string GetStringFromInt(int i)
        {
            return i.ToString();
        }

        public static int GetInt()
        {
            return 15;
        }
    }
}
