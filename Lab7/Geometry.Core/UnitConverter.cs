using System;
using System.Collections.Generic;
using System.Text;

namespace Geometry.Core
{
    public static class UnitConverter
    {
        public static double Cm2ToM2(double cm2) => cm2 / 10_000;
        public static double M2ToCm2(double m2) => m2 * 10_000;
        public static double Convert(double value, string from, string to)
        {
            if (from == "cm2" && to == "m2") return Cm2ToM2(value);
            if (from == "m2" && to == "cm2") return M2ToCm2(value);
            if (from == to) return value;
            throw new ArgumentException($"Неизвестное преобразование: {from} - {to}");
        }
    }
}
