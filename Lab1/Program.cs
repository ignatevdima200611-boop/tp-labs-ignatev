using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите число от 0 до 4, где 0 это выход, а цифры 1-4 это задания");

            string ch = Console.ReadLine();

            switch (ch)
            {
                case "1":
                    Task1();
                    break;
                case "2":
                    Task3();
                    break;
                case "3":
                    Task3();
                    break;
                case "4":
                    Task4();
                    break;
                case "0":
                    Console.WriteLine("Выход из программы");
                    return;
                default:
                    Console.WriteLine("Неккоректный ввод, выберите число от 0-4");
                    break;
            }
        }

    }
}