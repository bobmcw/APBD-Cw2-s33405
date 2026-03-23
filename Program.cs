// See https://aka.ms/new-console-template for more information

using ConsoleApp1.Device;
using ConsoleApp1.RentalService;
using ConsoleApp1.User;

Console.WriteLine("Hello, World!");

var usr = new User("Bob", "Smith", new StudentTier());
var service = new RentalService();
service.AddDeviceToInventory(new Projector(1920, 1080, "Epson"));
Console.WriteLine(service.Inventory);
service.RentDevice(1, usr, new DateTime(2026, 6, 12));
Console.WriteLine(service.CurrentRents);
service.ReturnDevice(1);
Console.WriteLine(service.History);

