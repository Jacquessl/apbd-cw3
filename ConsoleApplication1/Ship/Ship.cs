using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Ship
    {
        public List<Container> Containers = new();

        public Ship(double speed, int maxContainers, double maxCargoWeight)
        {
            Speed = speed;
            MaxContainers = maxContainers;
            MaxCargoWeight = maxCargoWeight;
        }

        private double _actualCargoWeight = 0;
        public double Speed { get; set; }
        public int MaxContainers { get; set; }
        public double MaxCargoWeight { get; set; }

        public void AddContainer(Container con)
        {
            if (Containers.Count < MaxContainers && _actualCargoWeight + con.CargoWeight < MaxCargoWeight)
            {
                Containers.Add(con);
                _actualCargoWeight += con.CargoWeight;
            }
        }

        public void AddContainers(List<Container> cons)
        {
            foreach (Container con in cons)
            {
                AddContainer(con);
            }
        }

        public void RemoveContainer(string name)
        {
            foreach (Container con in Containers)
            {
                if (con.SerialNumber == name)
                {
                    Containers.Remove(con);
                    _actualCargoWeight -= con.CargoWeight;
                    break;
                }
            }
            Console.WriteLine(Containers.Count);
        }

        public void ReplaceContainer(string name, Container con)
        {
            RemoveContainer(name);
            AddContainer(con);
        }

        public void ChangeShip(Ship targetShip, string name)
        {
            foreach (Container con in Containers)
            {
                if (con.SerialNumber == name)
                {
                    con.ChangeShip(this, targetShip);
                    break;
                }
            }
        }
    }
}