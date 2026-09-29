using System;
using System.Collections.Generic;
using System.IO;

public class FileSearcher
{
    public List<FileInfo> Search(string folder, string keyword)
    {
        if (string.IsNullOrWhiteSpace(folder))
            throw new ArgumentException("Путь к папке не может быть пустым");

        if (string.IsNullOrWhiteSpace(keyword))
            throw new ArgumentException("Ключевое слово не может быть пустым");

        if (!Directory.Exists(folder))
            throw new FileNotFoundException($"Папка не найдена: {folder}");

        var result = new List<FileInfo>();

        foreach (string path in Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories))
        {
            string fileName = Path.GetFileName(path);

            if (fileName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(new FileInfo(path));
            }
        }
        return result;
    }
}


class Program
{
    static void Main()
    {
        Console.Write("Введите путь к папке: ");
        string folder = Console.ReadLine()!;

        Console.Write("Введите ключевое слово: ");
        string keyword = Console.ReadLine()!;

        var searcher = new FileSearcher();

        try
        {
            var results = searcher.Search(folder, keyword);

            if (results.Count == 0)
            {
                Console.WriteLine("Файлы не найдены!");
                return;
            }

            Console.WriteLine($"Найдено файлов: {results.Count}");
            Console.WriteLine();

            foreach (var f in results)
            {
                Console.WriteLine($"{f.Name} - {f.Length} байт - {f.LastWriteTime}");
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Файл не найден: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка ввода: {ex.Message}");
        }
    }
}