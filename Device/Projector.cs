namespace ConsoleApp1.Device;

public class Projector(int resolutionX, int resolutionY, string make) : IDevice
{
    public int ResolutionX { get; } = resolutionX;
    public int ResolutionY { get; } = resolutionY;
    public string Make { get; } = make;

}