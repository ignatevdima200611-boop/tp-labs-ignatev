using System.Diagnostics;

int counter = 0;

var threads = Enumerable.Range(0, 4)
    .Select(_ => new Thread(() =>
    {
        for (int i = 0; i < 100_000; i++)
            counter++;
    }))
    .ToList();

var sw = Stopwatch.StartNew();
threads.ForEach(t => t.Start());
threads.ForEach(t => t.Join());
sw.Stop();

Console.WriteLine($"Получилось: {counter}");
Console.WriteLine("Ожидалось: 400000");
Console.WriteLine($"Время:  {sw.ElapsedMilliseconds} мс");