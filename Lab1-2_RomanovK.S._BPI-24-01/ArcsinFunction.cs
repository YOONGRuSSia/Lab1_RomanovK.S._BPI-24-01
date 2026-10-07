using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2_RomanovK.S._BPI_24_01
{
    public class ArcsinFunction : Function
    {
        public ArcsinFunction() : base("arcsin(x)")
        {
        }

        public override double Calculate()
        {
            double x = X;
            if (x < -1 || x > 1)
                throw new ArgumentOutOfRangeException("x", "Аргумент арксинуса должен быть в диапазоне [-1, 1].");

            return Math.Asin(x);
        }

        public override Function GetDerivative()
        {
            return new DerivativeFunction(this);
        }

        public override string Describe()
        {
            return base.Describe() + " (Область определения: [-1, 1])";
        }

        public string Describe(bool includeDerivative)
        {
            string baseDesc = Describe();
            return includeDerivative ? baseDesc + " [Производная доступна]" : baseDesc;
        }
    }
}
