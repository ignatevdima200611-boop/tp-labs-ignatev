namespace Lab4.Core;

public static class MathAlgorithms
{
    public static long Factorial(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n));
        if (n > 20)
            throw new ArgumentOutOfRangeException(nameof(n), "переполнен long");

        long result = 1;
        for (int i = 2; i <= n; i++)
            result *= i;
        return result;
    }

    public static IReadOnlyList<long> Fibonacci(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
        var result = new List<long>();
        long a = 0;
        long b = 1;
        for (int i = 0; i < count; i++)
        {
            result.Add(a);
            (a, b) = (b, a + b);
        }
        return result;
    }

    public static double ExpTaylor(double x, double eps = 1e-6)
    {
        double sum = 1.0;
        double term = 1.0;
        int n = 0;

        while (Math.Abs(term) > eps)
        {
            n++;
            term *= x / n;
            sum += term;
        }
        return sum;
    }

    public static double VarFunc(double x)
    {
        if (x == 5)
            throw new ArgumentException("x = 5, деление на ноль", nameof(x));

        if ((x / (x - 5)) <= 0)
            throw new ArgumentException("Под логарифмом не положительное число", nameof(x));

        if ((Math.Log(x / (x - 5))) < 0)
            throw new ArgumentException("Под корнем получается отрицательное число", nameof(x));

        return Math.Sqrt(Math.Log(x / (x - 5))) + (x + Math.Exp(x - 1)) - Math.Atan(x / 2);
    }
}