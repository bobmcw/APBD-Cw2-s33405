namespace ConsoleApp1.User;

public abstract class User(string firstName, string lastName)
{
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    
}