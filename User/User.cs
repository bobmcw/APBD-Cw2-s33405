namespace ConsoleApp1.User;

public abstract class User(string firstName, string lastName)
{
    public string _firstName { get; } = firstName;
    public string _lastName { get; } = lastName;
    
}