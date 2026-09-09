using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите число от 0 до 4, где 0 это выход, а цифры 1-4 это задания: ");

            string ch = Console.ReadLine();

            switch (ch)
            {
                case "1":
                    Task1();
                    break;
                case "2":
                    Task2();
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

        static void Task1()
        {
            Console.WriteLine("Задание 1: ");

            Console.Write("Введите n (0..20): ");

            if (!int.TryParse(Console.ReadLine(), out int n) || n < 0 || n > 20)
            {
                Console.WriteLine("Ошибка: нужно целое число от 0 до 20.");
                return;
            }

            Console.WriteLine($"{n}! = {Factorial(n)}");

            static long Factorial(int n)
            {
                long result = 1;
                for (int i = 2; i <= n; i++)
                    result *= i;
                return result;
            }

        }

        static void Task2()
        {
            Console.WriteLine("Задание 2: ");

        }

        static void Task3()
        {
            Console.WriteLine("Задание 3: ");

        }

        static void Task4()
        {
            Console.WriteLine("Задание 4: ");

        }
    }
}