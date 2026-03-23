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
                   break;
               case '3':
                   break;
               default:
                   Console.Error.WriteLine("Invalid option");
                   break;
           }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}