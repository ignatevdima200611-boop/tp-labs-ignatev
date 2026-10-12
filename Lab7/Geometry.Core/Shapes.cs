using System;
using System.Collections.Generic;
using System.Text;
using Geometry.Core;

namespace Geometry.Core
{
    public abstract class Shape
    {
        public abstract double Area();
        public abstract double Perimeter();
    }
    public class Circle : Shape
    {
        private double _radius;
        public double Radius => _radius;

        public Circle(double radius)
        {
            if (radius <= 0) throw new ArgumentOutOfRangeException(nameof(radius));
            _radius = radius;
        }

        public override double Area() => Math.PI * _radius * _radius;
        public override double Perimeter() => 2 * Math.PI * _radius;
        public override string ToString() => $"Круг R={_radius}: S={Area():F2}";
    }
    public class Rectangle : Shape
    {
        private double _width, _height;
        public double Width => _width;
        public double Height => _height;

        public Rectangle(double width, double height)
        {
            if (width <= 0 || height <= 0) throw new ArgumentOutOfRangeException();
            _width = width;
            _height = height;
        }

        public override double Area() => _height * _width;
        public override double Perimeter() => 2 * (_width + _height);
        public override string ToString() => $"Прямоугольник {_width}x{_height}: S={Area():F2}";
    }
    public class Square : Rectangle
    {
        public Square(double side) : base(side, side) { }
    }

    public class Triangle : Shape
    {
        private double _a, _b, _c;
        public double A => _a;
        public double B => _b;
        public double C => _c;
        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentOutOfRangeException(nameof(a), "Стороны должны быть больше 0");
            if (a + b <= c || a + c <= b || b + c <= c)
                throw new ArgumentException("Треугольника с такими сторонами не существуют");
            _a = a;
            _b = b;
            _c = c;
        }
        public override double Area()
        {
            double p = (_a + _b +  _c) / 2;
            return Math.Sqrt(p * (p - _a) * (p - _b) * (p - _c));
        }

        public override double Perimeter() => _a + _b + _c;
        public override string ToString() => $"Треугольник {_a}x{_b}x{_c}: S={Area():F2}";

    }
    public class Trapezoid : Shape
    {
        private double _baseA, _baseB, _height;
        public double BaseA => _baseA;
        public double BaseB => _baseB;
        public double Height => _height;

        public Trapezoid(double baseA, double baseB, double height)
        {
            if (baseA <= 0 || baseB <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException(nameof(baseA), "Все значения должны быть больше 0.");
            _baseA = baseA;
            _baseB = baseB;
            _height = height;
        }
        public override double Area() => (_baseA + _baseB) / 2 * _height;
        public override double Perimeter() => throw new NotSupportedException("Периметр трапеции зависит от углов.");
        public override string ToString() => $"Трапеция ({_baseA}, {_baseB}, h={_height}: S={Area():F2}";
    }
}
