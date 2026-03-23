using System.Reflection;
using ConsoleApp1.User;

namespace ConsoleApp1;

public class Tui
{
    private RentalService.RentalService _service = new RentalService.RentalService();
    private List<User.User> _users = new List<User.User>();

    private void _addUserForm()
    {
        Console.Clear();
        var tierList = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IUserType).IsAssignableFrom(t) && !t.IsInterface)
            .Select(t => (IUserType) Activator.CreateInstance(t)!).ToList();
        Console.WriteLine("enter first name");
        var fname = Console.ReadLine();
        Console.Clear();
        
        Console.WriteLine("enter last name");
        var lname = Console.ReadLine();
        Console.Clear();
        
        Console.WriteLine("select tier");
        int count = 1;
        foreach (var tier in tierList)
        {
           Console.WriteLine(count++ + " " + tier.get_type()); 
        }

        var key = (int) Char.GetNumericValue(Console.ReadKey().KeyChar);
        var usr = new User.User(fname, lname, tierList[key - 1]);
        _users.Add(usr);
        Console.WriteLine("added user: " + usr);

    }

    private void _addDeviceForm()
    {
        var typeList = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(Device.Device).IsAssignableFrom(t) && !t.IsAbstract)
            .Select(t => (Device.Device)Activator.CreateInstance(t)!).ToList();
        Console.WriteLine("select device type:");
        int count = 1;
        foreach (var type in typeList)
        {
            Console.WriteLine(count++ + " " + type.get_type()); 
        }

        var key = (int) Char.GetNumericValue(Console.ReadKey().KeyChar);
        var ctor = typeList[key - 1].GetType().GetConstructors().FirstOrDefault()!;
        ParameterInfo[] parameters = ctor.GetParameters();
        object[] args = new object[parameters.Length];
        
        for (int i = 0; i < parameters.Length; i++)
        {
            Console.Write($"Enter value for {parameters[i].Name} ({parameters[i].ParameterType.Name}): ");
            string input = Console.ReadLine() ?? throw new InvalidOperationException();

            args[i] = Convert.ChangeType(input, parameters[i].ParameterType) ?? throw new InvalidOperationException();
        }

        var dev = (Device.Device) ctor.Invoke(args);
        _service.AddDeviceToInventory(dev);
        Console.WriteLine("added device: " + dev);
        
    }

    private void _rentForm()
    {
        if (_users.Count == 0)
        {
            Console.WriteLine("no users");
            return;
        }
        Console.WriteLine("which user to make a rent for");
        int count = 1;
        foreach (var tmpusr in _users)
        {
           Console.WriteLine(count++ + ". " + tmpusr); 
        }
        var key = (int) Char.GetNumericValue(Console.ReadKey().KeyChar);
        var usr = _users[key - 1];
        
        Console.Clear();
        Console.WriteLine("which device to rent?");
        count = 1;
        foreach (var device in _service.Inventory)
        {
            Console.WriteLine(count++ + ". " + device); 
        }
        key = (int) Char.GetNumericValue(Console.ReadKey().KeyChar);
        var devId = _service.Inventory[key - 1].Id;
        try
        {
            _service.RentDevice(devId, usr, new DateTime(2026, 10, 23));
        }
        catch (Exception _)
        {
            Console.Error.WriteLine("device is already rented");
            Console.ReadKey();
        }
    }
    
    public void Start()
    {
        while (true)
        {
           Console.WriteLine("Select option"); 
           Console.WriteLine("1. add new user"); 
           Console.WriteLine("2. add new device"); 
           Console.WriteLine("3. make a new rent");
           var key = Console.ReadKey();
           switch (key.KeyChar)
           {
               case '1':
                   _addUserForm();
                   break;
               case '2':
                   _addDeviceForm();
                   break;
               case '3':
                   _rentForm();
                   break;
               default:
                   Console.Error.WriteLine("Invalid option");
                   break;
           }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}