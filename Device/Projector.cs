namespace ConsoleApp1.Device;

public class Projector(int resolutionX, int resolutionY, string make) : Device
{
    public int ResolutionX { get; } = resolutionX;
    public int ResolutionY { get; } = resolutionY;
    public string Make { get; } = make;

    public override string ToString()
    {
        return base.ToString() + " " + Make + " " + ResolutionX + "x" + ResolutionY;
    }
}