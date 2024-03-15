namespace ConsoleApplication1
{
    public abstract class Container : IContainer
    {
        public double CargoWeight { get; set; }
        public string SerialNumber { get; private set; }
        private static int ContainerCounter = 0;
        public string ContainerType;
        public double ContainerHeight { get; set; }
        public double ContainerWeight { get; set; }
        public double ContainerDepth { get; set; }
        public double CargoMax { get; set; }
        protected Container(double cargoWeight, string containerType, double containerDepth, 
            double containerHeight, double containerWeight)
        {
            CargoWeight = cargoWeight;
            ContainerType = containerType;
            ContainerHeight = containerHeight;
            ContainerWeight = containerWeight;
            ContainerDepth = containerDepth;
            generateSerialNumber();
        }

        private void generateSerialNumber()
        {
            SerialNumber = $"KON-{ContainerType}-{(++ContainerCounter).ToString()}";
        }

        public virtual void Unload()
        {
            CargoWeight = 0;
        }

        public virtual void Load(double cargoWeight)
        {
            if (CargoWeight+cargoWeight > CargoMax)
            {
                throw new OverfillException();
            }

            CargoWeight += cargoWeight;
        }

        public void ChangeShip(Ship nowShip, Ship targetShip)
        {
            nowShip.RemoveContainer(SerialNumber);
            targetShip.AddContainer(this);
        }
    }
}