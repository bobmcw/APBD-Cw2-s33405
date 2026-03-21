namespace ConsoleApp1.Device;

public class Laptop(string cpu, string os, string make) : IDevice
{
    public string Cpu { get; } = cpu;
    public string Os { get; } = os;
    public string Make { get; } = make;
}