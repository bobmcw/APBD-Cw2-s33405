namespace ConsoleApp1.Device;

public abstract class Device
{
    public int Id { get; }
    private static int _idGenerator = 0;
    public Device()
    {
        this.Id = ++_idGenerator;
    }

    public override string ToString()
    {
        return Id + " " + get_type();
    }

    public abstract string get_type();
}