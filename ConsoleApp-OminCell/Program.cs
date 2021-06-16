﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_OminCell
{
    class Program
    {
        static void Main(string[] args)
        {
            Cabinet cabinet = new Cabinet();
            IntialiseCabinet(ref cabinet);
            Console.Write("Provide your User ID, please (0-5): ");

            int userId = -1;

            while (!(int.TryParse(Console.ReadLine(), out userId) && (userId >= 0 && userId <= 5)))
            {
                Console.WriteLine("Invalid input!");
                Console.Write("Provide your User ID, please (0-5): ");
            }

            int actionId = -2;

            while (actionId != -1)
            {

                Console.WriteLine("Please select one of the following actions:");
                Console.WriteLine("-1: Shut Down the cabinet");
                Console.WriteLine("0: Add Medication to a bin");
                Console.WriteLine("1: Remove Medication from a bin");
                Console.WriteLine("2: Reorder Report");
                Console.Write("Selected Action: ");

                while (!(int.TryParse(Console.ReadLine(), out actionId) && (actionId >= -1 && actionId <= 2)))
                {
                    Console.WriteLine("Invalid input!");
                    Console.WriteLine("Please select one of the following actions:");
                    Console.WriteLine("-1: Shut Down the cabinet");
                    Console.WriteLine("0: Add Medication to a bin");
                    Console.WriteLine("1: Remove Medication from a bin");
                    Console.WriteLine("2: Reorder Report");
                    Console.Write("Selected Action: ");
                }
                Console.WriteLine();
                switch (actionId)
                {
                    case 0:
                        AddMedication(ref cabinet, userId);
                        break;
                    case 1:
                        RemoveMedication(ref cabinet, userId);
                        break;
                    case 2:
                        ReorderReport(cabinet);
                        break;
                    default:
                        break;
                }

                Console.WriteLine();
            }
            Console.WriteLine("Shutting Down, press any key to exit");
            Console.Read();
        }

        private static void AddMedication(ref Cabinet cabinet, int userId)
        {
            int medicationId = -1;
            Console.Write("Provide the Medication ID, please (0-10000): ");
            while (!(int.TryParse(Console.ReadLine(), out medicationId) && (medicationId >= 0 && medicationId <= 10000)))
            {
                Console.WriteLine("Invalid input!");
                Console.Write("Provide the Medication ID, please (0-10000): ");
            }
            Console.Write("Provide the Medication Name, please: ");
            string medicationName = Console.ReadLine();
            Console.WriteLine("Please select one of the following bins:");
            Console.WriteLine("0-1: Large bin (15 units capacity)");
            Console.WriteLine("2-6: Medium bin (10 units capacity)");
            Console.WriteLine("7-9: Small bin (5 units capacity)");
            Console.Write("Selected bin: ");
            int binId = -1;
            while (!(int.TryParse(Console.ReadLine(), out binId) && (binId >= 0 && binId <= 9)))
            {
                Console.WriteLine("Invalid input!");
                Console.WriteLine("Please select one of the following bins:");
                Console.WriteLine("0-1: Large bin (15 units capacity)");
                Console.WriteLine("2-6: Medium bin (10 units capacity)");
                Console.WriteLine("7-9: Small bin (5 units capacity)");
                Console.Write("Selected bin: ");
            }
            int quantity = -1;
            Console.Write("Provide the Medication quantity, please: ");
            while (!(int.TryParse(Console.ReadLine(), out quantity) && (quantity >= 1 && quantity <= 10000)))
            {
                Console.WriteLine("Invalid input!");
                Console.Write("Provide the Medication quantity, please (1-10000): ");
            }
            Bin selectedBin = cabinet.Bins.Where(x => x.ID == binId).First();
            string actionResult = selectedBin.AddMedication(new Medication { ID = medicationId, Name = medicationName }, quantity);
            Console.WriteLine(actionResult);
            cabinet.AddLog(userId, actionResult);
            if (!actionResult.StartsWith("Warning")) cabinet.UpdateReorderReport();
        }
        private static void RemoveMedication(ref Cabinet cabinet, int userId)
        {
            int medicationId = -1;
            Console.Write("Provide the ID of the Medication to remove, please (0-10000): ");
            while (!(int.TryParse(Console.ReadLine(), out medicationId) && (medicationId >= 0 && medicationId <= 10000)))
            {
                Console.WriteLine("Invalid input!");
                Console.Write("Provide the Medication ID, please (0-10000): ");
            }
            
            Console.WriteLine("Please select one of the following bins:");
            Console.WriteLine("0-1: Large bin");
            Console.WriteLine("2-6: Medium bin");
            Console.WriteLine("7-9: Small bin");
            Console.Write("Selected bin: ");
            int binId = -1;
            while (!(int.TryParse(Console.ReadLine(), out binId) && (binId >= 0 && binId <= 9)))
            {
                Console.WriteLine("Invalid input!");
                Console.WriteLine("Please select one of the following bins:");
                Console.WriteLine("0-1: Large bin (15 units capacity)");
                Console.WriteLine("2-6: Medium bin (10 units capacity)");
                Console.WriteLine("7-9: Small bin (5 units capacity)");
                Console.Write("Selected bin: ");
            }
            int quantity = -1;
            Console.Write("Provide the Medication quantity to get removed, please: ");
            while (!(int.TryParse(Console.ReadLine(), out quantity) && (quantity >= 1 && quantity <= 10000)))
            {
                Console.WriteLine("Invalid input!");
                Console.Write("Provide the Medication quantity, please (1-10000): ");
            }
            Bin selectedBin = cabinet.Bins.Where(x => x.ID == binId).First();
            string actionResult = selectedBin.RemoveMedicationByID(medicationId, quantity);
            Console.WriteLine(actionResult);
            cabinet.AddLog(userId, actionResult);
            if (!actionResult.StartsWith("Warning")) cabinet.UpdateReorderReport();
        }

        static void IntialiseCabinet(ref Cabinet cabinet)
        {
            cabinet.UserIds = new List<int>();
            cabinet.Log = new List<string>();
            cabinet.UserIds = new List<int>();
            cabinet.Bins = new List<Bin>();
            cabinet.ReorderReport = new Dictionary<Medication, string>();
            cabinet.IsEmpty = true;

            // add 5 users
            for (int i = 0; i < 5; i++)
            {
                cabinet.UserIds.Add(i);
            }

            // load the cabinets with 10 bins
            for (int i = 0; i < 2; i++)
            {
                cabinet.Bins.Add(CreateBin(i, BinType.large, 15));
            }
            for (int i = 0; i < 5; i++)
            {
                cabinet.Bins.Add(CreateBin(i+2, BinType.medium, 10));
            }
            for (int i = 0; i < 3; i++)
            {
                cabinet.Bins.Add(CreateBin(i+6, BinType.small, 5));
            }
        }

        static Bin CreateBin(int id, BinType type, int units)
        {
            Bin bin = new Bin()
            {
                ID = id,
                Type = type,
                Units = units,
                Inventory = new Dictionary<Medication, int>()
            };
            return bin;
        }

        static void ReorderReport(Cabinet cabinet)
        {
            if (cabinet.IsEmpty)
            {
                Console.WriteLine("All the bins are empty!");
                return;
            }

            if (cabinet.ReorderReport.Count == 0)
                Console.WriteLine("All stocks are sufficient");

            foreach (var item in cabinet.ReorderReport)
            {
                Console.WriteLine(item.Value);
            }
        }
    }

    enum BinType
    {
        large,
        medium,
        small
    }

    class Medication
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    interface IBin
    {
        int ID { get; set; }
        BinType Type { get; set; }
        int Units { get; set; }
        Dictionary<Medication, int> Inventory { get; set; }

        string AddMedication(Medication medication, int quantity);        
        string RemoveMedication(Medication medication, int quantity);
        string AddMedicationByID(int medicationId, int quantity);
        string RemoveMedicationByID(int medicationId, int quantity);
    }

    class Bin : IBin
    {
        public int ID { get; set; }
        public BinType Type { get; set; }
        public int Units { get; set; }
        public Dictionary<Medication, int> Inventory { get; set; }

        public string AddMedication(Medication medication, int quantity)
        {
            if (Inventory.Where(x => x.Key.ID == medication.ID).Any())
            {
                Medication medicationFound = Inventory.Where(x => x.Key.ID == medication.ID).First().Key;

                Inventory.TryGetValue(medicationFound, out int quantVal);
                
                if (quantVal + quantity <= Units)
                {
                    Inventory[medicationFound] = quantVal + quantity;
                }
                else return $"Warning!: Add Medication {medication.Name} (id:{medication.ID}) Failed, quantity is bigger than {Units - quantVal} units available in this bin having id: {ID}!";
                
            }
            else
            {
                if (quantity <= Units)
                {
                    Inventory.Add(medication, quantity);
                }
                else return $"Warning!: Add Medication {medication.Name} (id:{medication.ID}) Failed, quantity is bigger than {Units} units available in this bin having id: {ID}!";

            }
            return $"Medication {medication.Name} (id:{medication.ID}), Quantity: {quantity} Added to this bin having id: {ID}!";
        }

        public string RemoveMedication(Medication medication, int quantity)
        {
            if (Inventory.Where(x => x.Key.ID == medication.ID).Any())
            {
                Medication medicationFound = Inventory.Where(x => x.Key.ID == medication.ID).First().Key;

                Inventory.TryGetValue(medicationFound, out int quantVal);

                if (quantVal >= quantity)
                {
                    Inventory[medicationFound] = quantVal - quantity;
                }
                else return $"Warning!: Remove Medication {medication.Name} (id:{medication.ID}) Failed, quantity requested is bigger than {quantVal} units available in bin having id: {ID}!";
            }
            else
            {
                return $"Warning!: Remove  Medication {medication.Name} (id:{medication.ID}) Failed, no such medication available in this bin having id: {ID}!";
            }
            return $"Medication {medication.Name} (id:{medication.ID}), Quantity: {quantity} Removed from this bin having id: {ID}!";
        }

        public string AddMedicationByID(int medicationId, int quantity)
        {
            Medication medication = Inventory.Where(x => x.Key.ID == medicationId)?.First().Key;
            if (medication != null) return AddMedication(medication, quantity);
            return $"Warning!: Add Medication {medication.Name} (id:{medication.ID}) Failed, no such medication available in this bin having id: {ID}!";
        }

        public string RemoveMedicationByID(int medicationId, int quantity)
        {
            Medication medication = Inventory.Where(x => x.Key.ID == medicationId)?.First().Key;
            if (medication != null) return RemoveMedication(medication, quantity);
            return $"Warning!: Remove  Medication {medication.Name} (id:{medication.ID}) Failed, no such medication available in this bin having id: {ID}!";
        }
    }

    class Cabinet
    {
        public List<int> UserIds { get; set; }
        public List<string> Log { get; set; }
        public List<Bin> Bins { get; set; }
        public Dictionary<Medication, string> ReorderReport { get; set; }

        public bool IsEmpty { get; set; }

        public void AddLog(int userID, string message)
        {
            Log.Add($"UserID: {userID}, Action: {message}");
        }

        public void UpdateReorderReport()
        {
            int percentageWarning = 20;
            int minStockFine = (15 + 10 + 5) * percentageWarning / 100;
            Dictionary<Medication, int> inventory = new Dictionary<Medication, int>();
            foreach (var bin in Bins)
            {
                foreach (var item in bin.Inventory)
                {
                    if (inventory.TryGetValue(item.Key, out int quantVal))
                    {
                        inventory.TryGetValue(item.Key, out int quant);
                        inventory[item.Key] = quant + quantVal;
                    }
                    else
                    {
                        inventory.Add(item.Key, item.Value);
                    }
                }
            }

            foreach (var item in inventory)
            {
                if(item.Value < minStockFine)
                {
                    ReorderReport[item.Key] = $"Medication {item.Key.Name} (id:{item.Key.ID}) stock became {minStockFine - item.Value} units lower than minimum allowed";
                }
                else
                {
                    ReorderReport.Remove(item.Key);
                }
            }

            if (inventory.Count == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
            }
        }
    }
}
