using Geometry.Core;

var collection = new ShapeCollection();

while (true)
{
    Console.WriteLine("\n1-круг 2-прямоугольник 3-итоги 0-выход");
    switch (Console.ReadLine())
    {
        case "1":
            Console.Write("Радиус: ");
            collection.Add(new Circle(double.Parse(Console.ReadLine()!)));
            break;
        case "2":
            Console.Write("Ширина и высота через пробел: ");
            var p = Console.ReadLine()!.Split();
            collection.Add(new Rectangle(double.Parse(p[0]), double.Parse(p[1])));
            break;
        case "3":
            Console.WriteLine($"Фигур: {collection.Shapes.Count}, S общая: {collection.TotalArea():F2}");
            Console.WriteLine($"Крупнейшая: {collection.Largest()}");
            break;
        case "0": return;
    }
}