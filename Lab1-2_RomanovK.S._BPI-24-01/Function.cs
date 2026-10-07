using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2_RomanovK.S._BPI_24_01
{
    public class Function
    {
        public string Name { get; set; }
        public double X { get; set; } = 0;
        public Function(string name)
        {
            Name = name;
           
            Console.WriteLine($"Вызван конструктор базового класса для {name}");
        }
        public virtual double Calculate() // заглушка
        {
            throw new NotImplementedException(
                "Метод Calculate должен быть переопределён в наследнике.");
        }

        public virtual Function GetDerivative()
        {
            throw new NotImplementedException("Метод должен быть переопределен в наследниках.");
        }

        public virtual string Describe()
        {
            return $"Функция: {Name}, X = {X}";
        }

        public string Describe(string prefix)
        {
            return $"{prefix} {Describe()}";
        }
    }
}
