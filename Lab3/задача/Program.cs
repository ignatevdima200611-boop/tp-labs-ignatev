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