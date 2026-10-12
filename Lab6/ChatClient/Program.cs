using System.Net.Sockets;

Console.Write("Адрес сервера (Enter = localhost): ");
string host = Console.ReadLine() is { Length: > 0 } h ? h : "localhost";

using var client = new TcpClient();
try
{
    await client.ConnectAsync(host, 5555);
}
catch (SocketException)
{
    Console.WriteLine("Сервер недоступен Проверьте адрес и порт.");
    return;
}
var stream = client.GetStream();
var reader =  new StreamReader(stream);
var writer = new StreamWriter(stream) { AutoFlush = true };

string nick;
while (true)
{
    Console.Write("Ваш ник: ");
    nick = Console.ReadLine() ?? "anon";

    await writer.WriteLineAsync(nick);
    string? response = await reader.ReadLineAsync();

    if (response == "OK")
    {
        Console.WriteLine("Ник универсален!");
        break;
    }
    else if (response == "NICK_TAKEN")
    {
        Console.WriteLine("Этот ник уже занят! введите другой.");
    }
}

_ = Task.Run(async () =>
{
    try
    {
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
            Console.WriteLine(line);
    }
    catch (Exception ex) when (ex is IOException or ObjectDisposedException or SocketException) { }
    Console.WriteLine("Соединение с сервером потеряно.");
});

Console.WriteLine("Подключено. Пишите сообщения, /exit - выход., /list - список пользователей");
while (true)
{
    string? msg = Console.ReadLine();
    if (msg == null) continue;
    await writer.WriteLineAsync(msg);
    if (msg == "/exit") break;
}