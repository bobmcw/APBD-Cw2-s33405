namespace ConsoleApp1.Device;

public abstract class Device
{
    public int Id { get; }
    private static int _idGenerator = 0;
    protected Device()
    {
        this.Id = ++_idGenerator;
    }
    
}