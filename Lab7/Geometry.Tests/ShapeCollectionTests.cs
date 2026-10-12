using Geometry.Core;

namespace Geometry.Tests;

public class ShapeCollectionTests
{
    [Fact]
    public void TotalArea_TwoShapes_IsSum()
    {
        var c = new ShapeCollection();
        c.Add(new Rectangle(2, 3));
        c.Add(new Square(4));
        Assert.Equal(22, c.TotalArea(), 1e-9);
    }
}
