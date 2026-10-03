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

            Console.Write("Введите n для вычисления последовательности Фибоначчи:");

            if (!int.TryParse(Console.ReadLine(), out int n) || n < 0)
            {
                Console.WriteLine("Ошибка: нужно целое число больше 0.");
                return;
            }

            long first_value = 0;
            long second_value = 1;

            Console.Write(first_value);

            if (n >= 1)
            {
                Console.Write($", {second_value}");
            }

            for (int i = 2; first_value <= n; i++)
            {
                long third_value = first_value + second_value;
                Console.Write($", {third_value}");
                first_value = second_value;
                second_value = third_value;
            }
            Console.WriteLine();

        }

        static void Task3()
        {
            Console.WriteLine("Задание 3: ");

            Console.Write("Введите x для решения функции:");

            if (!int.TryParse(Console.ReadLine(), out int x))
            {
                Console.WriteLine("Ошибка: число введенно не корректно.");
                return;
            }

            if (x == 5)
            {
                Console.WriteLine("Ошибка: деление на ноль (x = 5).");
                return;
            }

            if ((x / (x - 5)) <= 0)
            {
                Console.WriteLine("Ошибка: под логарифмом отрицательное число или ноль");
                return;
            }

            if ((Math.Log(x / (x - 5))) < 0)
            {
                Console.WriteLine("Ошибка: под корнем отрицательное число (ln(x/(x-5))");
            }

            double a = Math.Sqrt(Math.Log(x/(x-5))) + (x + Math.Exp(x-1)) - Math.Atan(x/2);

            Console.WriteLine(a);
        }

        static void Task4()
        {
            Console.WriteLine("Задание 4: ");

            Console.Write("Введите x:");

            if (!double.TryParse(Console.ReadLine(), out double x))
            {
                Console.WriteLine("Ошибка: число введенно не корректно.");
                return;
            }

            double epsilon = 1e-6;

            double sum = 0;
            double term = 1;
            int n = 0;
            int count = 0;

            while (Math.Abs(term) > epsilon)
            {
                sum += term;
                count++;
                n++;

                term *= x / n;
            }

            sum += term;
            count++;

            double mathexp = Math.Exp(x);

            Console.WriteLine($"Сумма ряда Тейлора: {sum}");
            Console.WriteLine($"Библиотечное значение: {mathexp}");
            Console.WriteLine($"Разница: {Math.Abs(sum - mathexp)}");
            Console.WriteLine($"Количество членов: {count}");
        }
    }
}