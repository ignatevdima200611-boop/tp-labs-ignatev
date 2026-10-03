using Lab4.Core;

namespace Lab4.Tests;

public class ShapeTests
{
    [Fact]
    public void Circle_Area_ReturnsCorrect()
    {
        var circle = new Circle(5);
        Assert.Equal(Math.PI * 25, circle.Area(), 1e-5);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Circle_InvalidRadius_Throws(double radius)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Circle(radius));
    }

    [Fact]
    public void Rectangle_Area_ReturnsCorrect()
    {
        var rect = new Rectangle(4, 6);
        Assert.Equal(24, rect.Area());
    }

    [Fact]
    public void Square_Area_ReturnsCorrect()
    {
        var square = new Square(5);
        Assert.Equal(25, square.Area());
    }

    [Fact]
    public void Triangle_Area_ReturnsCorrect()
    {
        var triangle = new Triangle(3, 4, 5);
        Assert.Equal(6, triangle.Area(), 1e-5);
    }

    [Theory]
    [InlineData(1, 1, 10)]
    [InlineData(1, 10, 1)]
    public void Triangle_InvalidSides_Throws(double a, double b, double c)
    {
        Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));
    }

    [Fact]
    public void Shape_Move_Changes_Position()
    {
        var circle = new Circle(5, 0, 0);
        circle.Move(10, 20);
        Assert.Equal(10, circle.X);
        Assert.Equal(20, circle.Y);
    }

    [Fact]
    public void Circle_Scale_ChangesRadius()
    {
        var circle = new Circle(5);
        circle.Scale(2);
        Assert.Equal(10, circle.Radius);
    }
}