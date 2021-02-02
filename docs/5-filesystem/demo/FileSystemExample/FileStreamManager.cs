using System;
using System.IO;
using System.Text;

namespace FileSystemExample
{
    public class FileStreamManager
    {
        public static void ReadAndWriteFromFile()
        {
            FileStream fstream = null;
            try
            {
                fstream = new FileStream(@"C:\Users\Roman_Kitar\Desktop\Lectures\Files\Sample.dat", FileMode.OpenOrCreate);
                string text = "Sample information";
                byte[] array = Encoding.Default.GetBytes(text);

                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");

                fstream = File.OpenRead(@"C:\Users\Roman_Kitar\Desktop\Lectures\Files\Sample.dat");
                array = new byte[fstream.Length];
                
                fstream.Read(array, 0, array.Length);

                string textFromFile = Encoding.Default.GetString(array);
                Console.WriteLine($"Текст из файла: {textFromFile}");

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (fstream != null)
                    fstream.Close();
            }
        }
    }
}
