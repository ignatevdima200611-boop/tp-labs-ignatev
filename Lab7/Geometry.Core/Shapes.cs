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
}
