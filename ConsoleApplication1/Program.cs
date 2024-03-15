using System;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //var container = new Container() { CargoWeight = 100 };
            // var list = new List<int>() {1,2,3};
            // Dictionary<string, int> dict = new Dictionary<string, int>();
            // Dictionary<PossibleProducts, double> products;
            // var con = new LiquidContainer(100);
            // var con2 = new LiquidContainer(100);
            // var con3 = new LiquidContainer(100);
            // var con4 = new LiquidContainer(100);
            // Console.WriteLine(con.getSerial());
            // Console.WriteLine(con2.getSerial());
            // Console.WriteLine(con3.getSerial());
            // Console.WriteLine(con4.getSerial());
            var con = new CoolingContainer(100, 100, 100, 100, PossibleProducts.Banana);
            string numerString = Convert.ToString(PossibleProducts.Banana);
            Console.WriteLine("String reprezentujący numer: " + numerString);
            Console.WriteLine(con.Temp);
            //con.Load(1000);
        }
    }
}