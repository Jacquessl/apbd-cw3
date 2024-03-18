using System;

namespace ConsoleApplication1
{
    public class GasContainer : Container, IHazardNotifier
    {
        public GasContainer(double cargoWeight, double containerDepth, 
            double containerHeight, double containerWeight) : base(cargoWeight, "G", 
            containerDepth, containerHeight, containerWeight)
        {
            
        }

        public void NotifyHazard(string msg)
        {
            Console.WriteLine($"{msg} - {SerialNumber}");
        }
        public override void Unload()
        {
            CargoWeight = CargoWeight*0.05;
        }

        public override void Load(double cargoWeight)
        {
            if (CargoWeight+cargoWeight > CargoMax)
            {
                NotifyHazard("Gas container reached its capacity");
                throw new OverfillException();
            }

            CargoWeight += cargoWeight;
        }

        public override string ToString()
        {
            return $"Liquid Container {SerialNumber}\n" +
                   $"Height {ContainerHeight}\n" +
                   $"Depth {ContainerDepth}\n" +
                   $"Weight {ContainerWeight}\n" +
                   $"Maximum Cargo Weight {CargoMax}\n" +
                   $"Cargo Weight {CargoWeight}\n";
        }
    }
}