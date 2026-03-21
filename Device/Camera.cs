namespace ConsoleApp1.Device;

public class Camera(int cost, int megaPixels, string make) : IDevice
{
    public int MegaPixels { get; } = megaPixels;
    public int Cost { get; } = cost;
    public string Make { get; } = make;


}