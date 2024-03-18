using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication1.UI;

public class UserInterface
{
    private List<Ship> Ships = new List<Ship>();
    private List<Container> UnusedContainers = new List<Container>();
    public UserInterface()
    {
        UseUserInterface();
    }
    
    private void UseUserInterface()
    {
        int Choice = 0;
        while (Choice != -1)
        {
            Console.WriteLine("Choose option: \n" +
                              "1 - Create Ship \n" +
                              "2 - Edit Ship\n" +
                              "3 - Show Ships\n" +
                              "4 - Create Container\n" +
                              "5 - Edit Unused Container\n" +
                              "6 - Show Unused Containers\n" +
                              "7 - Exit\n");
            string ChoiceStr = Console.ReadLine();
            if (int.TryParse(ChoiceStr, out Choice))
            {
                switch (Choice)
                {
                    case 1 :
                        CreateShip();
                        break;
                    case 2:
                        try
                        {
                            ChoseShipToEdit();
                        }
                        catch (Exception)
                        {
                            WrongAnswer();
                        }

                        break;
                    case 3:
                        ShowShips();
                        break;
                    case 4:
                        CreateContainer();
                        break;
                    case 5:
                        try
                        {
                            EditUnusedContainer();
                        }
                        catch (Exception)
                        {
                            WrongAnswer();
                        }

                        break;
                    case 6:
                        try
                        {

                            ShowUnusedContainers();
                        }
                        catch (Exception)
                        {
                            WrongAnswer();
                        }

                        break;
                    case 7:
                        Choice = -1;
                        break;
                    default:
                        WrongAnswer();
                        break;
                }
            }
            else
            {
                WrongAnswer();
            }
        }
    }

    private void ShowUnusedContainers()
    {
        Console.WriteLine("Unused Containers: ");
        int counter = 0;
        foreach (var con in UnusedContainers)
        {
            Console.WriteLine($"{con.SerialNumber} : {++counter}");
        }
        Console.WriteLine("Choose container's index for more info: ");
        string ChocieStr = Console.ReadLine();
        int Choice;
        if (int.TryParse(ChocieStr, out Choice))
        {
            Console.WriteLine(UnusedContainers[Choice-1]);
            Console.WriteLine("Press Enter to go back to Main Menu");
            Console.ReadLine();
        }
        else
        {
            WrongAnswer();
        }
    }

    private void WrongAnswer()
    {
        Console.WriteLine("Wrong answer");
        Console.WriteLine("Press Enter to go back to Main Menu");
        Console.ReadLine();
    }

    private void CreateShip()
    {
        int Choice;
        
        Console.WriteLine("Choose option: \n" +
                          "1 - Create Radnom Ship \n" +
                          "2 - Create Personalized Ship\n" +
                          "3 - Back\n");
        string ChoiceStr = Console.ReadLine();
        if (int.TryParse(ChoiceStr, out Choice))
        {
            switch (Choice)
            {
                case 1:
                    GenerateRandomShip();
                    break;
                case 2:
                    CreatePersonalizedShip();
                    break;
                case 3:
                    UseUserInterface();
                    break;
                default:
                    WrongAnswer();
                    break;
            }
        }
    }

    private void GenerateRandomShip()
    {
        Random rnd = new();
        Ships.Add(new Ship(rnd.NextDouble()*30, (int)(rnd.NextDouble() * 100 ), rnd.NextDouble()*9000+1000));
    }

    private void CreatePersonalizedShip()
    {
        Console.WriteLine("Input Speed: ");
        string SpeedStr = Console.ReadLine();
        double Speed;
        Console.WriteLine("Input Maximum Number Of Containers: ");
        string MaxContainersStr = Console.ReadLine();
        int MaxContainers;
        Console.WriteLine("Input Maximum Weight Of Cargo: ");
        string MaxCargoWeightStr = Console.ReadLine();
        double MaxCargoWeight;
        
        if (double.TryParse(SpeedStr, out Speed) && int.TryParse(MaxContainersStr, out MaxContainers) && double.TryParse(MaxCargoWeightStr, out MaxCargoWeight))
        {
            if (Speed > 0 && MaxCargoWeight > 0 && MaxContainers > 0)
            {
                Ships.Add(new Ship(Speed, MaxContainers, MaxCargoWeight));
            }
            else
            {
                WrongAnswer();
            }
        }
        else
        {
            WrongAnswer();
        }
    }

    private void ChoseShipToEdit()
    {
        if (Ships.Count != 0)
        {
            int Counter = 0;
            Console.WriteLine("Ships:");
            foreach (var ship in Ships)
            {
                Console.WriteLine($"Ship {++Counter}");
            }
            Console.WriteLine("Choose ship by entering its index: ");
            int Choice;
            string ChoiceStr = Console.ReadLine();
            if (int.TryParse(ChoiceStr, out Choice))
            {
                EditShip(Choice - 1);
            }
        }
        else
        {
            WrongAnswer();
        }
    }

    private void EditShip(int ShipNumber)
    {
        int Choice;
        
        Console.WriteLine("Choose option: \n" +
                          "1 - Add Container \n" +
                          "2 - Edit Container\n" +
                          "3 - Remove Container\n" +
                          "4 - Add Containers\n" +
                          "5 - Replace Container\n" +
                          "6 - Place Container on another Ship\n" +
                          "7 - Back\n");
        string ChoiceStr = Console.ReadLine();
        if (int.TryParse(ChoiceStr, out Choice))
        {
            switch (Choice)
            {
                case 1:
                    AddContainer(ShipNumber);
                    break;
                case 2:
                    EditContainer(ShipNumber, null);
                    break;
                case 3:
                    RemoveContainer(ShipNumber);
                    break;
                case 4:
                    AddContainers(ShipNumber, new List<int>());
                    break;
                case 5:
                    ReplaceContainer(ShipNumber);
                    break;
                case 6:
                    ChangeShips(ShipNumber);
                    break;
                case 7:
                    ChoseShipToEdit();
                    break;
                default:
                    WrongAnswer();
                    break;
            }
        }
    }

    private void ChangeShips(int ShipNumber)
    {
        Console.WriteLine("Containers On Ship: ");
        int counter = 0;
        foreach (var container in Ships[ShipNumber].Containers)
        {
            Console.WriteLine($"{container.SerialNumber} : {++counter}");
        }
        Console.WriteLine("Choose container to remove by inputing its index: ");
        var choiceStr = Console.ReadLine();
        int choice;
        if (int.TryParse(choiceStr, out choice))
        {
            counter = 0;
            Console.WriteLine("Other Ships: ");
            foreach (var ship in Ships)
            {
                Console.WriteLine($"Ship : {++counter}");
            }
            Console.WriteLine("Choose destination ship by inputing its index: ");
            var choiceStr2 = Console.ReadLine();
            int choice2;
            if (int.TryParse(choiceStr2, out choice2))
            {
                Ships[ShipNumber].ChangeShip(Ships[choice2-1], Ships[ShipNumber].Containers[choice-1].SerialNumber);
            }
            else
            {
                WrongAnswer();
            }
        }
        else
        {
            WrongAnswer();
        }
    }
    private void ReplaceContainer(int ShipNumber)
    {
        Console.WriteLine("Containers On Ship: ");
        int counter = 0;
        foreach (var container in Ships[ShipNumber].Containers)
        {
            Console.WriteLine($"{container.SerialNumber} : {++counter}");
        }
        Console.WriteLine("Choose container to remove by inputing its index: ");
        var choiceStr = Console.ReadLine();
        int choice;
        if (int.TryParse(choiceStr, out choice))
        {
             Console.WriteLine("Unused Containers: ");
             counter = 0;
             foreach (var unused in UnusedContainers)
             {
                 Console.WriteLine($"{unused.SerialNumber} : {++counter}");
             }
             Console.WriteLine("Choose container to place by inputing its index: ");
             var choice2Str = Console.ReadLine();
             int choice2;
             if (int.TryParse(choice2Str, out choice2))
             {
                 Ships[ShipNumber].ReplaceContainer(Ships[ShipNumber].Containers[choice-1].SerialNumber, UnusedContainers[choice2-1]);
             }
             else
             {
                 WrongAnswer();
             }
        }
        else
        {
            WrongAnswer();
        }
    }
    private void AddContainers(int ShipNumber, List<int> choices)
    {
        Console.WriteLine("Unused Containers: ");
        int Counter = 0;
        foreach (var con in UnusedContainers)
        {
            Console.WriteLine($"Container {con.SerialNumber} {++Counter}");
        }

        if (choices.Count > 0)
        {
            Console.WriteLine($"Your choices: {string.Join(", ", choices.Select(x => (1+x).ToString()).ToArray())}");
        }
        Console.WriteLine("Choose unused container by inputing its index(input -1 to submit): ");
        string ChoiceStr = Console.ReadLine();
        int Choice;
        if (int.TryParse(ChoiceStr, out Choice) && Choice < UnusedContainers.Count)
        {
            if (Choice != -1)
            {
                choices.Add(Choice-1);
                AddContainers(ShipNumber, choices);
            }
            else
            {
                List<Container> containerToAdd = new();
                foreach (var index in choices)
                {
                    containerToAdd.Add(UnusedContainers[index]);
                }
                Ships[ShipNumber].AddContainers(containerToAdd);
                foreach (var index in choices)
                {
                    UnusedContainers.Remove(UnusedContainers[index]);
                }
            }
        }
        else
        {
            WrongAnswer();
        }
    }
    private void AddContainer(int ShipNumber)
    {
        Console.WriteLine("Unused Containers: ");
        int Counter = 0;
        foreach (var con in UnusedContainers)
        {
            Console.WriteLine($"Container {con.SerialNumber} {++Counter}");
        }
        Console.WriteLine("Choose unused container by inputing its index");
        string ChoiceStr = Console.ReadLine();
        int Choice;
        if (int.TryParse(ChoiceStr, out Choice))
        {
            Ships[ShipNumber].AddContainer(UnusedContainers[Choice-1]);
        }
        else
        {
            WrongAnswer();
        }
    }
    private void EditUnusedContainer()
    {
        Console.WriteLine("Unused Containers: ");
        int counter = 0;
        foreach (var con in UnusedContainers)
        {
            Console.WriteLine($"{con.SerialNumber} : {++counter}");
        }
        Console.WriteLine("Choose unused container to edit by inputing its index: ");
        string ChoiceStr = Console.ReadLine();
        int Choice;
        if (int.TryParse(ChoiceStr, out Choice))
        {
            EditContainer(null, Choice-1);
        }
        else
        {
            WrongAnswer();
        }
    }
    private void EditContainer(int? ShipNumber, int? conIndex)
    {
        Console.WriteLine("Containers: ");
        int counter = 0;
        string ChocieStr = "-1";
            
        if (ShipNumber.HasValue)
        {
            foreach (var con in Ships[ShipNumber.Value].Containers)
            {
                Console.WriteLine($"{con.SerialNumber} : {++counter}");
            }


            Console.WriteLine("Choose container to edit by inputing its index: ");


            ChocieStr = Console.ReadLine();
        }

        int Choice;
        
        if (int.TryParse(ChocieStr, out Choice))
        {
            Container con = null;
            if (ShipNumber.HasValue)
            {
                con = Ships[ShipNumber.Value].Containers[Choice - 1];
            }
            else if (conIndex.HasValue)
            {
                con = UnusedContainers[conIndex.Value];
            }
            Console.WriteLine(con);
            Console.WriteLine("Choose option: \n" +
                              "1 - Height \n" +
                              "2 - Depth\n" +
                              "3 - Weight\n" +
                              "4 - Maximum Cargo Weight\n" +
                              "5 - Load Cargo\n" +
                              "6 - Unload Cargo");
            switch (con.ContainerType)
            {
                case "C":
                    Console.WriteLine("7 - Product\n" +
                                      "8 - Back\n");
                    break;
                case "G":
                    Console.WriteLine("7 - Back\n");
                    break;
                case "L":
                    Console.WriteLine("7 - Product\n" +
                                      "8 - Back\n");
                    break;
            }

            ChocieStr = Console.ReadLine();
            if (int.TryParse(ChocieStr, out Choice))
            {
                switch (Choice)
                {
                    case 1:
                        Console.WriteLine("Input new height: ");
                        var Choice2Str = Console.ReadLine();
                        double Choice2;
                        if (double.TryParse(Choice2Str, out Choice2))
                        {
                            con.ContainerHeight = Choice2;
                        }
                        else
                        {
                            WrongAnswer();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Input new depth: ");
                        var Choice2aStr = Console.ReadLine();
                        double Choice2a;
                        if (double.TryParse(Choice2aStr, out Choice2a))
                        {
                            con.ContainerDepth = Choice2a;
                        }
                        else
                        {
                            WrongAnswer();
                        }
                        break;
                    case 3:
                        Console.WriteLine("Input new weight: ");
                        var Choice2aaStr = Console.ReadLine();
                        double Choice2aa;
                        if (double.TryParse(Choice2aaStr, out Choice2aa))
                        {
                            con.ContainerWeight = Choice2aa;
                        }
                        else
                        {
                            WrongAnswer();
                        }
                        break;
                    case 4:
                        Console.WriteLine("Input new maximum cargo weight: ");
                        var Choice2abStr = Console.ReadLine();
                        double Choice2ab;
                        if (double.TryParse(Choice2abStr, out Choice2ab))
                        {
                            con.CargoMax = Choice2ab;
                        }
                        else
                        {
                            WrongAnswer();
                        }
                        break;
                    case 5:
                        Console.WriteLine("Input how much cargo to load: ");
                        var cargoLoadChoiceStr = Console.ReadLine();
                        double cargoLoadChoice;
                        if (double.TryParse(cargoLoadChoiceStr, out cargoLoadChoice))
                        {
                            con.Load(cargoLoadChoice);
                        }

                        break;
                    case 6:
                        con.Unload();
                        break;
                    case 7:
                        if (con.ContainerType == "C")
                        {
                            Console.WriteLine("Possible Products: ");
                            counter = 0;
                            foreach (var value in Enum.GetValues(typeof(PossibleProducts)))
                            {
                                Console.WriteLine($"{value} : {++counter}");
                            }
                            Console.WriteLine("Choose new product's index to set new product: ");
                            ChocieStr = Console.ReadLine();
                            if (int.TryParse(ChocieStr, out Choice))
                            {
                                CoolingContainer con2 = (CoolingContainer)con;
                                counter = 0;
                                foreach (var value in Enum.GetValues(typeof(PossibleProducts)))
                                {
                                    counter++;
                                    if (counter == Choice)
                                    {
                                        con2.Product = (PossibleProducts)value;
                                    }
                                }

                                con2.SetTemp();
                            }
                            else
                            {
                                WrongAnswer();
                            }
                        }
                        else if (con.ContainerType == "G")
                        {
                            if (ShipNumber.HasValue)
                            {
                                EditShip(ShipNumber.Value);
                            }
                            else
                            {
                                EditUnusedContainer();
                            }
                        }
                        else if (con.ContainerType == "L")
                        {
                            Console.WriteLine("Possible Products: ");
                            counter = 0;
                            foreach (var value in Enum.GetValues(typeof(PossibleLiquidProducts)))
                            {
                                Console.WriteLine($"{value} : {++counter}");
                            }
                            Console.WriteLine("Choose new product's index to set new product: ");
                            ChocieStr = Console.ReadLine();
                            if (int.TryParse(ChocieStr, out Choice))
                            {
                                LiquidContainer con2 = (LiquidContainer)con;
                                counter = 0;
                                foreach (var value in Enum.GetValues(typeof(PossibleLiquidProducts)))
                                {
                                    counter++;
                                    if (counter == Choice)
                                    {
                                        con2.Product = (PossibleLiquidProducts)value;
                                        con2.setDangerous();
                                    }
                                }
                            }
                        }
                        break;
                    case 8:
                        if (con.ContainerType == "L" || con.ContainerType == "C")
                        {
                            if (ShipNumber.HasValue)
                            {
                                EditShip(ShipNumber.Value);
                            }
                            else
                            {
                                EditUnusedContainer();
                            }
                        }
                        else
                            WrongAnswer();
                        break;
                    default:
                        WrongAnswer();
                        break;
                }
            }
            else
        
            {
                WrongAnswer();
            }
        }
        else
        {
            WrongAnswer();
        }
    }

    private void RemoveContainer(int ShipNumber)
    {
        Console.WriteLine("Containers: ");
        int counter = 0;
        foreach (var con in Ships[ShipNumber].Containers)
        {
            Console.WriteLine($"{con.SerialNumber} : {++counter}");
        }

        Console.WriteLine("Choose container to remove by inputing its index: ");
        string ChocieStr = Console.ReadLine();
        int Choice;
        if (int.TryParse(ChocieStr, out Choice))
        {
            Ships[ShipNumber].Containers.Remove(Ships[ShipNumber].Containers[Choice-1]);
        }
        else
        {
            WrongAnswer();
        }
    }

    private void CreateContainer()
    {
        int Choice;
        
        Console.WriteLine("Choose option: \n" +
                          "1 - Create Radnom Container \n" +
                          "2 - Create Personalized Container\n" +
                          "3 - Back\n");
        string ChoiceStr = Console.ReadLine();
        if (int.TryParse(ChoiceStr, out Choice))
        {
            switch (Choice)
            {
                case 1:
                    GenerateRandomContainer();
                    break;
                case 2:
                    CreatePersonalizedContainer();
                    break;
                case 3:
                    UseUserInterface();
                    break;
                default:
                    WrongAnswer();
                    break;
            }
        }
    }

    private void GenerateRandomContainer()
    {
        Random rnd = new();
        int type = rnd.Next(0, 3);
        switch (type)
        {
            case 0:
                PossibleProducts product = PossibleProducts.Banana;
                int maxPossible = 0;
                foreach (var value in Enum.GetValues(typeof(PossibleProducts)))
                {
                    maxPossible++;
                }

                int ChosenProduct = rnd.Next(0, maxPossible);
                int counter = 0;
                foreach (var value in Enum.GetValues(typeof(PossibleProducts)))
                {
                    if (counter == ChosenProduct)
                    {
                        product = (PossibleProducts)value;
                    }
                    counter++;
                }
                UnusedContainers.Add(new CoolingContainer(rnd.NextDouble()*500, rnd.NextDouble()*20,
                    rnd.NextDouble()*20, rnd.NextDouble()*500, rnd.NextDouble()*1000,product));
                break;
            case 1:
                UnusedContainers.Add(new GasContainer(rnd.NextDouble()*500, rnd.NextDouble()*20,
                    rnd.NextDouble()*20, rnd.NextDouble()*500, rnd.NextDouble()*1000));
                break;
            case 2:
                PossibleLiquidProducts LiquidProduct = PossibleLiquidProducts.Chlorine;
                int maxPossibleLiquid = 0;
                foreach (var value in Enum.GetValues(typeof(PossibleLiquidProducts)))
                {
                    maxPossibleLiquid++;
                }

                int ChosenProductLiquid = rnd.Next(0, maxPossibleLiquid);
                int counterLiquid = 0;
                foreach (var value in Enum.GetValues(typeof(PossibleLiquidProducts)))
                {
                    if (counterLiquid == ChosenProductLiquid)
                    {
                        LiquidProduct = (PossibleLiquidProducts)value;
                    }
                    counterLiquid++;
                }
                UnusedContainers.Add(new LiquidContainer(rnd.NextDouble()*500, rnd.NextDouble()*20,
                    rnd.NextDouble()*20, rnd.NextDouble()*500, rnd.NextDouble()*1000, LiquidProduct));
                break;
        }
    }

    private void CreatePersonalizedContainer()
    {
        Console.WriteLine("Avaliable Types:\n" +
                          "Cooling Container : 1\n" +
                          "Gas Container : 2 \n" +
                          "Liquid Container : 3\n" +
                          "Go back : 4" +
                          "Choose type by inputing its index: ");
        var choiceStr = Console.ReadLine();
        int choice;
        if (int.TryParse(choiceStr, out choice))
        {
            Console.WriteLine("Input Cargo Weight: ");
            var cargoWeightStr = Console.ReadLine();
            double cargoWeight;
            Console.WriteLine("Input Container Depth: ");
            var containerDepthStr = Console.ReadLine();
            double containerDept;
            Console.WriteLine("Input Container Height: ");
            var containerHeightStr = Console.ReadLine();
            double containerHeight;
            Console.WriteLine("Input Container Weight: ");
            var containerWeightStr = Console.ReadLine();
            double containerWeight;
            Console.WriteLine("Input Maximum Cargo Weight: ");
            var maxCargoWeightStr = Console.ReadLine();
            double maxCargoWeight;
            if (double.TryParse(cargoWeightStr, out cargoWeight) && double.TryParse(containerDepthStr, out containerDept) &&
                double.TryParse(containerHeightStr, out containerHeight) &&
                double.TryParse(containerWeightStr, out containerWeight) && 
                double.TryParse(maxCargoWeightStr, out maxCargoWeight))
            {
                switch (choice)
                {
                    case 1:
                        CreatePersonalizedCC(cargoWeight, containerDept, containerHeight, containerWeight, maxCargoWeight);
                        break;
                    case 2:
                        CreatePersonalizedGC(cargoWeight, containerDept, containerHeight, containerWeight, maxCargoWeight);
                        break;
                    case 3:
                        CreatePersonalizedLG(cargoWeight, containerDept, containerHeight, containerWeight, maxCargoWeight);
                        break;
                    case 4:
                        CreateContainer();
                        break;
                    default:
                        WrongAnswer();
                        break;
                }
            }
            else
            {
                WrongAnswer();
            }
        }
        else
        {
            WrongAnswer();
        }
    }

    private void CreatePersonalizedCC(double cargoWeight, double containerDept, double containerHeight, double containerWeight, double cargoMaxWeight)
    {
        Console.WriteLine("Possible Products: ");
        int counter = 0;
        foreach (var value in Enum.GetValues(typeof(PossibleProducts)))
        {
            Console.WriteLine($"{value} : {++counter}");
        }
        Console.WriteLine("Choose product's index to set product: ");
        int Choice;
        string choiceStr = Console.ReadLine();
        if (int.TryParse(choiceStr, out Choice))
        {
            counter = 0;
            foreach (PossibleProducts value in Enum.GetValues(typeof(PossibleProducts)))
            {
                counter++;
                if (counter == Choice)
                {
                    UnusedContainers.Add(new CoolingContainer(cargoWeight, containerDept, 
                        containerHeight, containerWeight, cargoMaxWeight, value));
                }
            }
        }
        else
        {
            WrongAnswer();
        }
    }

    private void CreatePersonalizedGC(double cargoWeight, double containerDept, double containerHeight, double containerWeight, double cargoMaxWeight)
    {
        UnusedContainers.Add(new GasContainer(cargoWeight, containerDept, containerHeight, containerWeight, cargoMaxWeight));
    }

    private void CreatePersonalizedLG(double cargoWeight, double containerDept, double containerHeight, double containerWeight, double cargoMaxWeight)
    {
        Console.WriteLine("Possible Products: ");
        int counter = 0;
        foreach (var value in Enum.GetValues(typeof(PossibleLiquidProducts)))
        {
            Console.WriteLine($"{value} : {++counter}");
        }
        Console.WriteLine("Choose product's index to set product: ");
        int Choice;
        string choiceStr = Console.ReadLine();
        if (int.TryParse(choiceStr, out Choice))
        {
            counter = 0;
            foreach (PossibleLiquidProducts value in Enum.GetValues(typeof(PossibleLiquidProducts)))
            {
                counter++;
                if (counter == Choice)
                {
                    UnusedContainers.Add(new LiquidContainer(cargoWeight, containerDept, 
                        containerHeight, containerWeight, cargoMaxWeight,value));
                }
            }
        }
        else
        {
            WrongAnswer();
        }
    }
    private void ShowShips()
    {
        Console.WriteLine("Ships:");
        foreach (var ship in Ships)
        {
            Console.WriteLine(ship);
        }
        Console.WriteLine("Press Enter to go back to Main Menu");
        Console.ReadLine();
    }
}