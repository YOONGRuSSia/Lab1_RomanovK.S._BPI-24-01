using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2_RomanovK.S._BPI_24_01
{
    public class DerivativeFunction : Function
    {
        private Function originalFunction;

        public DerivativeFunction(Function original) : base($"({original.Name})'")
        {
            originalFunction = original;
        }

        public override double Calculate()
        {
            double h = 0.0001;
            // (f(x+h) - f(x-h)) / 2h
            originalFunction.X = X + h;
            double fPlus = originalFunction.Calculate();

            originalFunction.X = X - h;
            double fMinus = originalFunction.Calculate();

            return (fPlus - fMinus) / (2 * h);
        }
        public override Function GetDerivative()
        {
            throw new NotImplementedException("Вычисление второй производной не предусмотрено.");
        }
    }
}
