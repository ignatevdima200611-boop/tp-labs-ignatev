using Geometry.Core;

var collection = new ShapeCollection();
var storage = new FileResultStorage("results.txt");
var manager = new ShapeManager(collection, storage);

while (true)
{
    Console.WriteLine("\n1-круг 2-прямоугольник 3-треугольник 4-трапеция 5-итоги 0-выход");
    switch (Console.ReadLine())
    {
        case "1":
            Console.Write("Радиус: ");
            manager.AddAndSave(new Circle(double.Parse(Console.ReadLine()!)));
            break;
        case "2":
            Console.Write("Ширина и высота через пробел: ");
            var p = Console.ReadLine()!.Split();
            manager.AddAndSave(new Rectangle(double.Parse(p[0]), double.Parse(p[1])));
            break;
        case "3":
            Console.Write("Три стороны через пробел: ");
            var t = Console.ReadLine()!.Split();
            manager.AddAndSave(new Triangle(double.Parse(t[0]), double.Parse(t[1]), double.Parse(t[2])));
            break;
        case "4":
            Console.Write("Основание и высота через пробел: ");
            var tr = Console.ReadLine()!.Split();
            manager.AddAndSave(new Trapezoid(double.Parse(tr[0]), double.Parse(tr[1]), double.Parse(tr[2])));
            break;
        case "5":
            manager.ReportAndSave();
            break;
        case "0": return;
    }
}