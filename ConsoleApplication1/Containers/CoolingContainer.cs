namespace ConsoleApplication1
{
    public class CoolingContainer : Container
    {
        public PossibleProducts Product { get; set; }
        public double Temp { get; private set; }
        public CoolingContainer(double cargoWeight, double containerDepth, 
            double containerHeight, double containerWeight, double cargoMax,PossibleProducts product) : base(cargoWeight, 
            "C", containerDepth, containerHeight, containerWeight, cargoMax)
        {
            Product = product;
            SetTemp();
        }

        public void SetTemp()
        {
            Temp = (double)((int)Product)/10;
        }

        public override string ToString()
        {
            return $"Liquid Container {SerialNumber}\n" +
                $"Height {ContainerHeight}\n" +
                $"Depth {ContainerDepth}\n" +
                $"Weight {ContainerWeight}\n" +
                $"Maximum Cargo Weight {CargoMax}\n" +
                $"Cargo Weight {CargoWeight}\n" +
                $"Product {Product.ToString()}\n" +
                $"Temperature {Temp}";
        }
    }
}