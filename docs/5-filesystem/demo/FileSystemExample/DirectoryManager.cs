using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSystemExample
{
    public static class DirectoryManager
    {
        public static void GetDirectories()
        {
            string dirName = "C:\\";

            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
                Console.WriteLine("Файлы:");
                string[] files = Directory.GetFiles(dirName);
                foreach (string s in files)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public static void CreateDirectory()
        {
            string path = @"C:\Users\Roman_Kitar\Desktop\Lectures";
            string subpath = @"SubDirectory";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subpath);
        }

        public static void GetDirectoryInfo()
        {
            string dirName = @"C:\Users\Roman_Kitar\Desktop\Lectures\SubDirectory";

            DirectoryInfo dirInfo = new DirectoryInfo(dirName);

            Console.WriteLine($"Название каталога: {dirInfo.Name}");
            Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
            Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");
            Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
        }

        public static void DeleteDirectory()
        {
            string dirName = @"C:\Users\Roman_Kitar\Desktop\Lectures\SubDirectory";

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                dirInfo.Delete(true);
                Console.WriteLine("Каталог удален");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void MoveDirectory()
        {
            string oldPath = @"C:\Users\Roman_Kitar\Desktop\Lectures\SubDirectory";
            string newPath = @"C:\Users\Roman_Kitar\Desktop\Lectures\SubDirectory2";

            DirectoryInfo dirInfo = new DirectoryInfo(oldPath);
            if (dirInfo.Exists && Directory.Exists(newPath) == false)
            {
                dirInfo.MoveTo(newPath);
            }
        }
    }
}
