using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConsoleApp_OminCell
{
    class Program
    {
        static void Main(string[] args)
        {
            Cabinet cabinet = new Cabinet();
            cabinet.IntialiseCabinet();
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
                Console.WriteLine("2: Stock Reorder Report");
                Console.WriteLine("3: Print Log");
                //Console.WriteLine("4: UnitTest Simulation");
                Console.Write("Selected Action: ");

                while (!(int.TryParse(Console.ReadLine(), out actionId) && (actionId >= -1 && actionId <= 3)))
                {
                    Console.WriteLine("Invalid input!");
                    Console.WriteLine("Please select one of the following actions:");
                    Console.WriteLine("-1: Shut Down the cabinet");
                    Console.WriteLine("0: Add Medication to a bin");
                    Console.WriteLine("1: Remove Medication from a bin");
                    Console.WriteLine("2: Stock Reorder Report");
                    Console.WriteLine("3: Print Log");
                    //Console.WriteLine("4: UnitTest Simulation");
                    Console.Write("Selected Action: ");
                }
                Console.WriteLine();
                switch (actionId)
                {
                    case 0:
                        AddMedicationUI(ref cabinet, userId);
                        break;
                    case 1:
                        RemoveMedicationUI(ref cabinet, userId);
                        break;
                    case 2:
                        foreach (var item in cabinet.StockReorderReport())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 3:
                        foreach (var item in cabinet.GetLog())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    //case 4:
                    //    CabinetUnitTest cabinetUnitTest = new CabinetUnitTest();
                    //    cabinetUnitTest.Initialize();
                    //    cabinetUnitTest.AddMedication_ShouldNoWarning_WhenMedicationQuantityLowerThanUnits();
                    //    cabinetUnitTest.Initialize();
                    //    cabinetUnitTest.RemoveMedication_ShouldNoWarning_WhenMedicationQuantityToRemoveIsGreaterThanUnitsOnStock();
                    //    cabinetUnitTest.Initialize();
                    //    cabinetUnitTest.StockReorderReport_ShouldReturnMessage_WhenMedicationQuantityLowerThanMinimum();
                    //    cabinetUnitTest.Initialize();
                    //    cabinetUnitTest.RemoveMedication_ShouldGiveWarning_WhenMedicationNotExistInCurrentBin();
                    //    break;
                    default:
                        break;
                }

                Console.WriteLine();
            }
            Console.WriteLine("Shutting Down, press Enter to exit");
            Console.Read();
        }

        private static void AddMedicationUI(ref Cabinet cabinet, int userId)
        {
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

            Console.WriteLine();
            int medicationId = -1;
            Console.Write("Provide the Medication ID, please (0-10000): ");
            while (!(int.TryParse(Console.ReadLine(), out medicationId) && (medicationId >= 0 && medicationId <= 10000)))
            {
                Console.WriteLine("Invalid input!");
                Console.Write("Provide the Medication ID, please (0-10000): ");
            }
            Console.Write("Provide the Medication Name, please: ");
            string medicationName = Console.ReadLine();

            int quantity = -1;
            Console.Write("Provide the Medication quantity, please: ");
            while (!(int.TryParse(Console.ReadLine(), out quantity) && (quantity >= 1 && quantity <= 10000)))
            {
                Console.WriteLine("Invalid input!");
                Console.Write("Provide the Medication quantity, please (1-10000): ");
            }
            
            string actionResult = cabinet.AddMedicationToBin(userId, binId, medicationId, medicationName, quantity);
            
            Console.WriteLine();
            Console.WriteLine(actionResult);
            
        }
        private static void RemoveMedicationUI(ref Cabinet cabinet, int userId)
        {
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

            int medicationId = -1;
            Console.WriteLine();
            Console.Write("Provide the ID of the Medication to remove, please (0-10000): ");
            while (!(int.TryParse(Console.ReadLine(), out medicationId) && (medicationId >= 0 && medicationId <= 10000)))
            {
                Console.WriteLine("Invalid input!");
                Console.Write("Provide the Medication ID, please (0-10000): ");
            }

            Console.Write("Provide the Medication quantity to get removed, please: ");

            int quantity;
            while (!(int.TryParse(Console.ReadLine(), out quantity) && (quantity >= 1 && quantity <= 10000)))
            {
                Console.WriteLine("Invalid input!");
                Console.Write("Provide the Medication quantity, please (1-10000): ");
            }

            string actionResult = cabinet.RemoveMedicationFromBin(userId, binId, medicationId, quantity);

            Console.WriteLine();
            Console.WriteLine(actionResult);            
        }
    }

    public enum BinType
    {
        large,
        medium,
        small
    }

    public class Medication : IMedication
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public interface IBinFactory
    {
        IBin CreateLargeBin(int id);
        IBin CreateMediumBin(int id);
        IBin CreateSmallBin(int id);
    }

    public interface IBin
    {
        int ID { get; set; }
        BinType Type { get; set; }
        int Units { get; set; }
        Dictionary<Medication, int> Inventory { get; set; }

        string AddMedication(Medication medication, int quantity);
        string RemoveMedication(Medication medication, int quantity);
        string RemoveMedicationByID(int medicationId, int quantity);
    }

    public interface IMedication
    {
        int ID { get; set; }
        string Name { get; set; }
    }

    public interface ICabinet
    {
        List<int> UserIds { get; set; }
        List<string> Log { get; set; }
        List<IBin> Bins { get; set; }
        Dictionary<Medication, string> ReorderReport { get; set; }
        bool IsEmpty { get; set; }

        string AddMedicationToBin(int userId, int binId, int medicationId, string medicationName, int quantity);

        string RemoveMedicationFromBin(int userId, int binId, int medicationId, int quantity);

        List<string> StockReorderReport();

        List<string> GetLog();
    }

    public class BinFactory : IBinFactory
    {
        public static IBin CreateLargeBin(int id)
        {
            return CreateBin(id, BinType.large, 15);
        }

        public static IBin CreateMediumBin(int id)
        {
            return CreateBin(id, BinType.medium, 10);
        }

        public static IBin CreateSmallBin(int id)
        {
            return CreateBin(id, BinType.small, 5);
        }

        IBin IBinFactory.CreateLargeBin(int id)
        {
            return BinFactory.CreateLargeBin(id);
        }

        IBin IBinFactory.CreateMediumBin(int id)
        {
            return BinFactory.CreateMediumBin(id);
        }

        IBin IBinFactory.CreateSmallBin(int id)
        {
            return BinFactory.CreateSmallBin(id);
        }

        static IBin CreateBin(int id, BinType type, int units)
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
    }


    public class Bin : IBin
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
            return $"Medication {medication.Name} (id:{medication.ID}), Quantity: {quantity} Added to this bin having id: {ID}.";
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
            return $"Medication {medication.Name} (id:{medication.ID}), Quantity: {quantity} Removed from this bin having id: {ID}.";
        }

        public string RemoveMedicationByID(int medicationId, int quantity)
        {
            Medication medication = Inventory.Where(x => x.Key.ID == medicationId)?.FirstOrDefault().Key;
            if (medication != null) return RemoveMedication(medication, quantity);
            return $"Warning!: Remove  Medication {((medication?.Name) ?? "Unknown")} (id:{medication?.ID ?? medicationId}) Failed, no such medication available in this bin having id: {ID}!";
        }
    }

    public class Cabinet : ICabinet
    {
        public List<int> UserIds { get; set; }
        public List<string> Log { get; set; }
        public List<IBin> Bins { get; set; }
        public Dictionary<Medication, string> ReorderReport { get; set; }

        public bool IsEmpty { get; set; }



        public void IntialiseCabinet()
        {
            BinFactory bf = new BinFactory();
            UserIds = new List<int>();
            Log = new List<string>();
            UserIds = new List<int>();
            Bins = new List<IBin>();
            ReorderReport = new Dictionary<Medication, string>();
            IsEmpty = true;

            // add 5 users
            for (int i = 0; i < 5; i++)
            {
                UserIds.Add(i);
            }

            // load the cabinets with 10 bins
            for (int i = 0; i < 2; i++)
            {
                Bins.Add(BinFactory.CreateLargeBin(i));
            }
            for (int i = 0; i < 5; i++)
            {
                Bins.Add(BinFactory.CreateMediumBin(i + 2));
            }
            for (int i = 0; i < 3; i++)
            {
                Bins.Add(BinFactory.CreateSmallBin(i + 6));
            }
        }

        public string AddMedicationToBin(int userId, int binId, int medicationId, string medicationName, int quantity)
        {
            Bin selectedBin = (Bin)Bins.Where(x => x.ID == binId).First();
            string actionResult = selectedBin.AddMedication(new Medication { ID = medicationId, Name = medicationName }, quantity);

            AddLog(userId, $"ActionType: Add, Action: {actionResult}");
            
            if (!actionResult.StartsWith("Warning")) UpdateReorderReport();
            
            return actionResult;
        }

        public string RemoveMedicationFromBin(int userId, int binId, int medicationId, int quantity)
        {
            Bin selectedBin = (Bin)Bins.Where(x => x.ID == binId).First();
            string actionResult = selectedBin.RemoveMedicationByID(medicationId, quantity);
            
            AddLog(userId, $"ActionType: Remove, Action: {actionResult}");
            
            if (!actionResult.StartsWith("Warning")) UpdateReorderReport();

            return actionResult;
        }

        private void AddLog(int userID, string message)
        {
            Log.Add($"UserID: {userID}, {message}");
        }

        private void UpdateReorderReport()
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
                if (item.Value > 0 && item.Value < minStockFine)
                {
                    ReorderReport[item.Key] = $"Medication {item.Key.Name} (id:{item.Key.ID}) stock became {minStockFine - item.Value} units lower than minimum allowed";
                }
                else
                {
                    ReorderReport.Remove(item.Key);
                }
            }

            if (inventory.Count() == 0 || inventory.Where(x => x.Value > 0).Count() == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
            }
        }

        public List<string> StockReorderReport()
        {
            if (IsEmpty)
            {
                return new List<string>() { "All the bins are empty!" };
            }

            if (ReorderReport.Count == 0)
                return new List<string>() { "All stocks are sufficient" };

            
            return ReorderReport.Select(x => x.Value).ToList();
        }

        public List<string> GetLog()
        {
            if (Log.Count() == 0)
            {
                return new List<string>() { "The log is empty!" };
            }

            return Log;
        }
    }

    [TestFixture]
    public class CabinetUnitTest
    {
        int UserId { get; set; }
        Cabinet Cabinet { get; set; }

        [SetUp]
        public void Initialize()
        {
            this.UserId = 0;
            Cabinet = new Cabinet();
            Cabinet.IntialiseCabinet();
        }

        [Test, Order(1)]
        public void AddMedication_ShouldNoWarning_WhenMedicationQuantityLowerThanUnits()
        {
            // Arrange
            int binId = 0;
            int medicationId = 0;
            string medicationName = "Paracetamol";
            int quantity = 8;

            // Act
            var actualOutput = Cabinet.AddMedicationToBin(UserId, binId, medicationId, medicationName, quantity);

            // Assert
            Assert.AreEqual("Medication Paracetamol (id:0), Quantity: 8 Added to this bin having id: 0.", actualOutput);
        }

        [Test, Order(2)]
        public void RemoveMedication_ShouldNoWarning_WhenMedicationQuantityToRemoveIsGreaterThanUnitsOnStock()
        {
            // Arrange
            int binId = 0;
            int medicationId = 0;
            string medicationName = "Paracetamol";
            int quantity = 8;
            Cabinet.AddMedicationToBin(UserId, binId, medicationId, medicationName, quantity);
            quantity = 3;
            // Act
            var actualOutput = Cabinet.RemoveMedicationFromBin(UserId, binId, medicationId, quantity);

            // Assert
            Assert.AreEqual("Medication Paracetamol (id:0), Quantity: 3 Removed from this bin having id: 0.", actualOutput);
        }

        [Test, Order(3)]
        public void StockReorderReport_ShouldReturnMessage_WhenMedicationQuantityLowerThanMinimum()
        {
            // Arrange
            int binId = 0;
            int medicationId = 0;
            string medicationName = "Paracetamol";
            int quantity = 5;
            Cabinet.AddMedicationToBin(UserId, binId, medicationId, medicationName, quantity);

            // Act
            var actualOutput = Cabinet.StockReorderReport();

            // Assert
            Assert.AreEqual(1, actualOutput.Count);
            Assert.AreEqual("Medication Paracetamol (id:0) stock became 1 units lower than minimum allowed", actualOutput[0]);
        }

        [Test, Order(4)]
        public void RemoveMedication_ShouldGiveWarning_WhenMedicationNotExistInCurrentBin()
        {
            // Arrange
            int binId = 0;
            int medicationId = 0;
            int quantity = 3;

            // Act
            var actualOutput = Cabinet.RemoveMedicationFromBin(UserId, binId, medicationId, quantity);

            // Assert
            Assert.AreEqual("Warning!: Remove  Medication Unknown (id:0) Failed, no such medication available in this bin having id: 0!", actualOutput);
        }

        // Note:
        // A lot more things to write unit test for like warnings when we add more than possible or remove more than available, or try to remove from empty bins for example, reorder list related all scennarios,
        // testing cases to prove the user activity logging is correct and so on, however not sure how was this was imagined by the requester to do in a single file editor given by [RemoteInterview.io] 
        // as I'm not even able to trigger testing here, only call the like any other class methods by pressing 4 on action input user ui.
        // That single editor limitation I used here too not splitting classes in their own class files for example.
    }
}