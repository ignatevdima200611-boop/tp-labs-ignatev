using System;
using System.Collections.Generic;
using System.Text;
using Geometry.Core;

namespace Geometry.Tests
{
    public class ShapeTests
    {
        [Fact]
        public void Circle_Area_ReturnsCorrent()
        {
            var c = new Circle(5);
            Assert.Equal(Math.PI * 25, c.Area(), 1e-9);
        }

        [Fact]
        public void Circle_InvalidRadius_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Circle(-1));
        }
        [Fact]
        public void Rectangle_Area_ReturnsCorrect()
        {
            var r = new Rectangle(4, 6);
            Assert.Equal(24, r.Area());
        }
        [Fact]
        public void Triangle_Area_ReturnsCorrect()
        {
            var t = new Triangle(3, 4, 5);
            Assert.Equal(6, t.Area(), 1e-9);
        }
        [Fact]
        public void Triangle_InvalidSides_Throws()
        {
            Assert.Throws<ArgumentException>(() => new Triangle(1, 1, 10));
        }
        [Fact]
        public void Trapezoid_Area_ReturnsCorrect()
        {
            var t = new Trapezoid(4, 6, 3);
            Assert.Equal(15, t.Area());
        }
        [Fact]
        public void ShapeManager_AddAndSave_SavesResult()
        {
            var collection = new ShapeCollection();
            var storage = new InMemoryResultStorage();
            var manager = new ShapeManager(collection, storage);

            manager.AddAndSave(new Circle(5));

            Assert.Single(storage.Saved);
            Assert.Contains("Круг", storage.Saved[0]);
        }
        [Fact]
        public void UnitConverter_Cm2ToM2_ReturnsCorrect()
        {
            Assert.Equal(1, UnitConverter.Cm2ToM2(10_000));
        }
    }
}
