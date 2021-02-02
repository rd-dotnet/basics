using System;
using System.IO;

namespace FileSystemExample
{
    public static class FileManager
    {
        public static void GetFileInfo()
        {
            string path = @"C:\Users\Roman_Kitar\Desktop\Lectures\Files\Dot_Net_oop_concepts.pptx";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);
            }
        }

        public static void CopyFile()
        {
            string path = @"C:\Users\Roman_Kitar\Desktop\Lectures\Files\Dot_Net_oop_concepts.pptx";
            string newPath = @"C:\Users\Roman_Kitar\Desktop\Dot_Net_oop_concepts.pptx";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.CopyTo(newPath, true);
            }
        }
    }
}
