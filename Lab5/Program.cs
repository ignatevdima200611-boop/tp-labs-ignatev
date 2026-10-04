using System.Collections.Concurrent;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.Write("Введите путь к папке: ");
        string folder = Console.ReadLine()!;

        Console.Write("Введите фразу для поиска: ");
        string phrase = Console.ReadLine()!;

        Console.Write("Введите количество потоков: ");
        if (!int.TryParse(Console.ReadLine(), out int threadCount) || threadCount < 1)
        {
            Console.WriteLine("Число долдно быть положительным!");
            return;
        }

        if (!Directory.Exists(folder)) 
        {
            Console.WriteLine("Введенная папка не найдена!");
            return;
        }

        var files = Directory.GetFiles(folder, "*.cs", SearchOption.AllDirectories);
        Console.WriteLine($"Найдено файлов: {files.Length}");
        Console.WriteLine($"Фраза: {phrase}");
        Console.WriteLine($"Потоков: {threadCount}");

        SearchSequential(files, phrase);

        var swSeq = Stopwatch.StartNew();
        var seqResults = SearchSequential(files, phrase);
        swSeq.Stop();

        Console.WriteLine("\nПоследовательно");
        Console.WriteLine($"Время: {swSeq.ElapsedMilliseconds} мс");
        Console.WriteLine($"Найдено: {seqResults.Count}");
        PrintResults(seqResults);


        var swPar = Stopwatch.StartNew();
        var parResults = SearchParallel(files, phrase, threadCount);
        swPar.Stop();

        Console.WriteLine($"\nПараллельно");
        Console.WriteLine($"Время: {swPar.ElapsedMilliseconds} мс");
        Console.WriteLine($"Найдено: {parResults.Count}");
        PrintResults(parResults);


        double speedup = swSeq.ElapsedMilliseconds / (double)swPar.ElapsedMilliseconds;
        Console.WriteLine($"Ускорение: {speedup:F2}");
    }

    static List<string> SearchSequential(string[] files, string phrases)
    {
        var results = new List<string>();
        foreach (var file in files)
        {
            SearchInFile(file, phrases, results);
        }
        return results;
    }

    static List<string> SearchParallel(string[] files, string phrase, int threadCount)
    {
        var queue = new ConcurrentQueue<string>(files);

        var results = new ConcurrentBag<string>();

        var threads = new List<Thread>();
        for (int i = 0; i < threadCount; i++)
        {
            var t = new Thread(() =>
            {
                while (queue.TryDequeue(out string? file))
                {
                    var local = new List<string>();
                    SearchInFile(file, phrase, local);
                    foreach (var r in local)
                        results.Add(r);
                }
            });
            t.Start();
            threads.Add(t);
        }

        foreach ( var t in threads)
            t.Join();

        return results.ToList();
    }

    static void SearchInFile(string file, string phrase, List<string> results)
    {
        try
        {
            int lineNumber = 0;
            foreach (string line in File.ReadLines(file))
            {
                lineNumber++;
                if (line.Contains(phrase, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add($"{file}: строка {lineNumber}");
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка ввода-вывода данных: {ex.Message}");
        }
    }

    static void PrintResults(List<string> results) 
    { 
        foreach (var r in results)
        {
            Console.WriteLine($" {r}");
        }
    }
}