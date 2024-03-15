namespace ConsoleApplication1
{
    public class CoolingContainer : Container
    {
        public PossibleProducts Product { get; set; }
        public double Temp { get; private set; }
        public CoolingContainer(double cargoWeight, double containerDepth, 
            double containerHeight, double containerWeight, PossibleProducts product) : base(cargoWeight, 
            "C", containerDepth, containerHeight, containerWeight)
        {
            Product = product;
            SetTemp();
        }

        private void SetTemp()
        {
            Temp = (double)((int)Product)/10;
        }
    }
}