using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Interfases
{
    interface I1
    {
        void M();
    }

    interface I2
    {
        void M();
    }

    class A:I1,I2
    {
        public void M() { Console.WriteLine("A.M()"); }
        void I1.M()
        {
            WriteLine("I1.A.M()");
        }

        void I2.M()
        {
            WriteLine("I2.A.M()");
        }

    }

    class B : A,I1,I2
    {
        public new void M() { Console.WriteLine("B.M()"); }
        void I1.M()
        {
            WriteLine("I1.B.M()");
        }

        void I2.M()
        {            
            WriteLine("I2.B.M()");            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            //Реализация всех методов
            A a = new A();
            a.M();
            ((I1)a).M();
            ((I2)a).M();


            B b = new B();
            b.M();
            ((I1)b).M();
            ((I2)b).M();

            ReadKey();
        }
    }
}
