using System.Collections.Generic;
public interface IMovable
{
    double X { get; }
    double Y { get; }
    void Move(double dx, double dy);
}
public abstract class Shape : IMovable
{
    private double _x;
    private double _y;
    public double X => _x;
    public double Y => _y;
    protected Shape(double x = 0, double y = 0)
    {
        _x = x;
        _y = y;
    }
    public void Move(double dx, double dy)
    {
        _x += dx;
        _y += dy;
    }

    public override string ToString()
    {
        return $"{GetType().Name} в({X}, {Y}): S ={Area():F2}, P ={Perimeter():F2}";
    }

    public abstract double Area();
    public abstract double Perimeter();
    public abstract void Scale(double factor);
}

public class Circle : Shape
{
    private double _radius;
    public double Radius => _radius;
    public Circle(double radius, double x = 0, double y = 0) :base(x, y)
    {
        if (radius <= 0) throw new ArgumentOutOfRangeException(nameof(radius));
        _radius = radius;
    }
    public override double Area() => Math.PI * _radius * _radius;
    public override double Perimeter() => 2 * Math.PI * _radius;
    public override void Scale(double factor)
    {
        if (factor <= 0) throw new ArgumentOutOfRangeException();
        _radius *= factor;
    }
    public override string ToString()
    {
        return $"Круг R={_radius} в ({X}, {Y}): S={Area():F2}, P={Perimeter():F2}";
    }
}

public class Rectangle : Shape
{
    private double _width;
    private double _height;
    public double Width => _width;
    public double Height => _height;
    public Rectangle(double width, double height, double x = 0, double y = 0) : base(x, y)
    {
        if (width <= 0 || height <= 0) throw new ArgumentOutOfRangeException();
        _width = width;
        _height = height;
    }

    public override double Area() => _width * _height;
    public override double Perimeter() => 2 * (_width + _height);
    public override void Scale(double factor)
    {
        if (factor <= 0) throw new ArgumentOutOfRangeException();
        _width *= factor;
        _height *= factor;
    }
}

public class Square : Rectangle
{
    public Square(double side, double x = 0, double y = 0) : base(side, side, x, y) { }
}
public class Triangle : Shape
{
    private double _a, _b, _c;
    public double A => _a;
    public double B => _b;
    public double C => _c;

    public Triangle(double a, double b, double c, double x = 0, double y = 0) : base(x, y)
    {
        if (a <= 0 || b <= 0 || c <= 0) throw new ArgumentOutOfRangeException();
        if ((a + b) <= c || (a + c) <= b || (b + c) <= a) throw new ArgumentException("Треугольника с такими сторонами не существует");
        _a = a; _b = b; _c = c;
    }
    public override double Area() 
    {
        double p = (_a + _b + _c) /2;
        return Math.Sqrt(p * (p - _a) * (p - _b) * (p - _c));
    }
    public override double Perimeter() => _a + _b + _c;
    public override void Scale(double factor)
    {
        if (factor <= 0) throw new ArgumentOutOfRangeException();
        _a *= factor;
        _b *= factor;
        _c *= factor;
    }
    public override string ToString()
    {
        return $"Треугольник {_a}x{_b}x{_c} в ({X}, {Y}): S={Area():F2}, P={Perimeter():F2}";
    }
}
class Program
{
    static void Main()
    {
        List<Shape> shapes = new()
        {
            new Circle(5, 0, 0),
            new Rectangle(4, 6, 10, 10),
            new Square(3, 20, 20),
            new Triangle(3, 4, 5, 30, 30),
            new Circle(2, 50, 50),
        };

        foreach (Shape s in shapes)
        {
            Console.WriteLine(s);
            Console.WriteLine($"  Площадь: {s.Area():F2}");
            Console.WriteLine($"  Периметр: {s.Perimeter():F2}");
            s.Move(10, 10);
            Console.WriteLine($"  После перемещения(10,10): {s}");
            s.Scale(2);
            Console.WriteLine($"  После масштабирования(2): {s}");
            Console.WriteLine();
        }
    }
}