using System;
using System.IO;
using System.Text;
using System.Threading;

namespace FileSystemExample
{
    public static class StreamManager
    {
        public static void WriteText()
        {
            string writePath = @"C:\Users\Roman_Kitar\Desktop\Lectures\Files\Sample.txt";

            string text = "Sample information";
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                {
                    sw.WriteLine(text);
                }

                using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
                {
                    sw.WriteLine("Дозапись");
                    sw.Write(4.5);
                }
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ReadText()
        {
            string path = @"C:\Users\Roman_Kitar\Desktop\Lectures\Files\Sample.txt";

            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    //Console.WriteLine(sr.ReadToEnd());

                    // Построчное чтение
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
