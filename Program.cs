// See https://aka.ms/new-console-template for more information

using ConsoleApp1;
using ConsoleApp1.Device;
using ConsoleApp1.RentalService;
using ConsoleApp1.User;

Console.WriteLine("Hello, World!");

var serv = new RentalService();
serv.AddDeviceToInventory(new Projector(3440, 1440, "Dell"));
serv.AddDeviceToInventory(new Laptop("Intel", "Linux", "Lenovo"));
serv.AddDeviceToInventory(new Camera(1000, 140, "Sony"));


var users = new List<User>();

users.Add(new User("Bob", "Kowalski", new EmployeeTier()));
users.Add(new User("Michal", "Studencki", new StudentTier()));

var tui = new Tui(serv, users);

tui.Start();