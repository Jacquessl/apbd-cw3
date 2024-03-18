using System;
using System.Data.Odbc;

namespace ConsoleApplication1
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public PossibleLiquidProducts Product { get; set; }
        public bool Dangerous { get; private set; }
        public LiquidContainer(double cargoWeight, double containerDepth, 
            double containerHeight, double containerWeight, double cargoMax, PossibleLiquidProducts product) : base(cargoWeight, "L", 
            containerDepth, containerHeight, containerHeight, cargoMax)
        {
            Product = product;
            setDangerous();
            CheckWeight();
        }

        public void setDangerous()
        {
            if ((int)Product % 2 == 0)
            {
                Dangerous = true;
            }
            else
            {
                Dangerous = false;
            }
        }
        
        public override void Load(double cargoWeight)
        {
            if (Dangerous && (cargoWeight+CargoWeight) > (CargoMax*0.5))
            {
                NotifyHazard("Dangerous liquid product over 50% container capacity");
                throw new OverfillException();
            }
            if ((cargoWeight + CargoWeight) > (CargoMax * 0.9))
            {
                NotifyHazard("Liquid product over 90% container capacity");
                throw new OverfillException();
            }
            base.Load(cargoWeight);
        }

        public override void CheckWeight()
        {
            if (Dangerous && (CargoWeight) > (CargoMax*0.5))
            {
                NotifyHazard("Dangerous liquid product over 50% container capacity");
                throw new OverfillException();
            }
            if ((CargoWeight) > (CargoMax * 0.9))
            {
                NotifyHazard("Liquid product over 90% container capacity");
                throw new OverfillException();
            }
            base.CheckWeight();
        }

        public void NotifyHazard(string msg)
        {
            Console.WriteLine($"{msg} - {SerialNumber}");
        }

        public override string ToString()
        {
            string result = $"Liquid Container {SerialNumber}\n" +
                            $"Height {ContainerHeight}\n" +
                            $"Depth {ContainerDepth}\n" +
                            $"Weight {ContainerWeight}\n" +
                            $"Maximum Cargo Weight {CargoMax}\n" +
                            $"Cargo Weight {CargoWeight}\n" +
                            $"Product {Product.ToString()}";
            result += Dangerous ? " (dangerous)" : "";
            return result;

        }
    }
}