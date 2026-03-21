namespace ConsoleApp1.User;

public class User
{
    private static int _idGenerator = 0;
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    User(string firstName, string lastName)
    {
        Id = ++_idGenerator;
        FirstName = firstName;
        LastName = lastName;
    }
    
}