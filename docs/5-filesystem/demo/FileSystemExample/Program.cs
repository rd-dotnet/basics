using System;
using System.IO;

namespace FileSystemExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Работа с дисками
            //GetDrivesInfo();

            // 2. Работа с директориями
            //DirectoryManager.CreateDirectory();
            //DirectoryManager.DeleteDirectory();
            //DirectoryManager.MoveDirectory();

            // 3. Работа с файлами
            //FileManager.GetFileInfo();
            //FileManager.CopyFile();

            // 4. Чтение и запись из файла
            //FileStreamManager.ReadAndWriteFromFile();

            // 5. Использование StreamWriter и StreamReader
            //StreamManager.WriteText();
            //StreamManager.ReadText();

            // 6. Использование BinaryWriter и BinaryReader
            //BinaryManager.BinaryWrite();
            //BinaryManager.BinaryRead();
        }

        public static void GetDrivesInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }
                Console.WriteLine();
            }
        }
    }
}
