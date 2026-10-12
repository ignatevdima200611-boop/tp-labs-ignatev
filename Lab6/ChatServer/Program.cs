using System.Net;
using System.Net.Sockets;
using System.IO;

const int Port = 5555;
var clients = new Dictionary <string,StreamWriter>();
var lockObj = new Object();  

var listener = new TcpListener(IPAddress.Any, Port);
listener.Start();
Console.WriteLine($"Сервер запущен на порту {Port}. Ctrl+C - остановка.");

while (true)
{
    TcpClient client = await listener.AcceptTcpClientAsync();
    _ = HandleClientAsync(client);
}
async Task HandleClientAsync(TcpClient client)
{
    var endpoint = client.Client.RemoteEndPoint;
    var stream = client.GetStream();
    var reader = new StreamReader(stream);
    var writer = new StreamWriter(stream) { AutoFlush = true };

    string? nick;
    while (true)
    {
        nick = await reader.ReadLineAsync();
        if (string.IsNullOrWhiteSpace(nick))
        {
            client.Close();
            return;
        }
        bool taken;
        lock (lockObj)
        {
            taken = clients.ContainsKey(nick);
            if (!taken)
                clients[nick] = writer;
        }
        if (taken)
        {
            await writer.WriteLineAsync("NICK_TAKEN");
            continue;
        }
        break;
    }

    await writer.WriteLineAsync("OK");

    Log($"{nick} подключился ({endpoint})");
    await BroadcastAsync($"*** {nick} вошел в чат ***");

    try
    {
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (line == "/exit") break;

            if (line == "/list")
            {
                List<string> nicksSnapshot;
                lock (lockObj) nicksSnapshot = clients.Keys.ToList();
                await writer.WriteLineAsync($"*** Подключены: {string.Join(", ", nicksSnapshot)} ***");
                Log($"{nick} запросил /list");
                continue;
            }

            if (line.StartsWith("/w "))
            {
                var parts = line.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3)
                {
                    await writer.WriteLineAsync("*** Использование: /w ник текст ***");
                    continue;
                }
                string targetNick = parts[1];
                string message = parts[2];

                StreamWriter? targetWriter = null;
                lock (lockObj)
                {
                    clients.TryGetValue(targetNick, out targetWriter);
                }
                if (targetWriter == null) 
                {
                    await writer.WriteLineAsync($"*** Пользователь {targetNick} не найден ***");
                }
                else
                {
                    await targetWriter.WriteLineAsync($"[Личное сообщение от {nick}] {message}");
                    await writer.WriteLineAsync($"[Личное сообщение к {targetNick}] {message}");
                    Log($"[Личное собщение] {nick} - {targetNick}: {message}");
                }
                continue;
            }
            await BroadcastAsync($"[{DateTime.Now:HH:mm:ss}] {nick}: {line}");
            Log($"{nick}: {line}");
        }
    }
    catch (IOException) { /* клиент оборвал соединение */ }
    finally
    {
        lock (lockObj) clients.Remove(nick);
        client.Close();
        Log($"{nick} отключился");
        await BroadcastAsync($"*** {nick} покинул чат ***");
    }
}

void Log(string message)
{
    string logLine = $"[{DateTime.Now:HH:mm:ss}] {message}";
    Console.WriteLine(logLine);
    File.AppendAllText("chat.log", logLine + Environment.NewLine);
}
async Task BroadcastAsync(string message)
{
    List<StreamWriter> snapshot;
    lock (lockObj) snapshot = clients.Values.ToList();

    foreach (var w in snapshot)
    {
        try { await w.WriteLineAsync(message); }
        catch (IOException) { /* отвалившийся клиент удалится в своей задаче */}
    }
}