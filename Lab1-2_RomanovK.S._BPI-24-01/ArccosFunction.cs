using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2_RomanovK.S._BPI_24_01
{
    public class ArccosFunction : Function
    {
        public ArccosFunction() : base("arccos(x)") //конструктор
        {
        }

        public override double Calculate() //Переопределение виртуала
        {
            double x = X;
            if (x < -1 || x > 1)
                throw new ArgumentOutOfRangeException("x", "Аргумент арккосинуса должен быть в диапазоне [-1, 1].");

            return Math.Acos(x);
        }

        public override Function GetDerivative()
        {
            return new DerivativeFunction(this);
        }
    }
}
