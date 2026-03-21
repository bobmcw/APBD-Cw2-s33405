namespace ConsoleApp1.User;

public class StudentTier : IUserType
{
    public int max_rentals()
    {
        return 3;
    }
}