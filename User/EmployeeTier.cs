namespace ConsoleApp1.User;

public class EmployeeTier : IUserType
{
    public int max_rentals()
    {
        return 5;
    }

    public string get_type()
    {
        return "Employee";
    }
}