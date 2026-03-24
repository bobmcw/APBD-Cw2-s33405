using System.Reflection;
using ConsoleApp1.Exceptions;
using ConsoleApp1.User;

namespace ConsoleApp1;

public class Tui(RentalService.RentalService service, List<User.User>? users = null)
{
    private RentalService.RentalService _service = service;
    private List<User.User> _users = users ?? new List<User.User>();

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
        var usr = new User.User(fname ?? throw new InvalidOperationException(), lname ?? throw new InvalidOperationException(), tierList[key - 1]);
        _users.Add(usr);
        Console.Clear();
        Console.WriteLine("added user: " + usr);
        Console.ReadKey();

    }

    private void _addDeviceForm()
    {
        Console.Clear();
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
        Console.Clear();
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
        Console.Clear();
        Console.WriteLine("added device: " + dev);
        Console.ReadKey();

    }

    private void _rentForm()
    {
        Console.Clear();
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
        Console.Clear();
        var devId = _service.Inventory[key - 1].Id;
        try
        {
            _service.RentDevice(devId, usr, new DateTime(2026, 10, 23));
            Console.WriteLine("device rented");
            Console.ReadKey();
        }
        catch (NoSuchIdException ex)
        {
            Console.Error.WriteLine(ex.Message);
            Console.ReadKey();
        }
        catch (TooManyRentsException ex)
        {
            Console.Error.WriteLine(ex.Message);
            Console.ReadKey();
        }
    }

    private void _returnForm()
    {
       Console.Clear();
       Console.WriteLine("return for which user?");
       int i = 1;
       foreach (var usr in _users)
       {
          Console.WriteLine(i++ + ". " + usr); 
       }
       var key = (int) Char.GetNumericValue(Console.ReadKey().KeyChar);
       var user = _users[key - 1];
       var rents = _service.CurrentRents.Where(i => i.UserId() == user.Id).ToList();
       if (rents.Count == 0)
       {
           Console.Error.WriteLine("no rents for this user");
           Console.ReadKey();
           return;
       }
        Console.Clear();
        Console.WriteLine("Which item to return?");
        i = 1;
       foreach (var rent in rents)
       {
          Console.WriteLine(i++ + ". " + rent.User + " " + rent.Device); 
       }
       key = (int) Char.GetNumericValue(Console.ReadKey().KeyChar);
       var id = rents[key - 1].DeviceId();
       _service.ReturnDevice(id);
       Console.WriteLine("device returned");
       Console.ReadKey();
    }

    private void _displayInventoryStatus()
    {
        Console.Clear();
        List<int> rentedIds = new List<int>();
        foreach (var rent in _service.CurrentRents)
        {
           rentedIds.Add(rent.DeviceId()); 
        }

        foreach (var dev in _service.Inventory)
        {
            Console.Write(dev);
            if (rentedIds.Contains(dev.Id))
            {
               Console.Write(" rented\n"); 
            }
            else
            {
               Console.Write(" available\n"); 
            }
        }
        Console.ReadKey();
    }

    private void _displayAvailableDevices()
    {
        Console.Clear();
        List<int> rentedIds = new List<int>();
        foreach (var rent in _service.CurrentRents)
        {
            rentedIds.Add(rent.DeviceId()); 
        }
        
        foreach (var dev in _service.Inventory)
        {
            if (!rentedIds.Contains(dev.Id))
            {
                Console.WriteLine(dev + " available");
            }
        }
        Console.ReadKey();
    }

    private void _displayRentsForUserForm()
    {
       Console.Clear();
       Console.WriteLine("which user?");
        int count = 1;
        foreach (var tmpusr in _users)
        {
           Console.WriteLine(count++ + ". " + tmpusr); 
        }
        var key = (int) Char.GetNumericValue(Console.ReadKey().KeyChar);
        var usr = _users[key - 1];
        Console.Clear();
        var rents = _service.CurrentRents.Where(i => i.UserId() == usr.Id).ToList();
        foreach (var rent in rents)
        {
            Console.WriteLine(rent);
        }
        Console.ReadKey();
    }
    
    public void Start()
    {
        while (true)
        { 
           Console.Clear();
           Console.WriteLine("Select option"); 
           Console.WriteLine("1. add new user"); 
           Console.WriteLine("2. add new device"); 
           Console.WriteLine("3. make a new rent");
           Console.WriteLine("4. return an item");
           Console.WriteLine("5. display inventory status");
           Console.WriteLine("6. display available devices");
           Console.WriteLine("7. display current rents for user");
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
               case '4':
                   _returnForm();
                   break;
               case '5':
                   _displayInventoryStatus();
                   break;
               case '6':
                   _displayAvailableDevices();
                   break;
               case '7':
                   _displayRentsForUserForm();
                   break;
               default:
                   Console.Error.WriteLine("Invalid option");
                   break;
           }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}