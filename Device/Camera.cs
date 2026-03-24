namespace ConsoleApp1.Device;

public class Camera(int cost, int megaPixels, string make) : Device
{
    public int MegaPixels { get; } = megaPixels;
    public int Cost { get; } = cost;
    public string Make { get; } = make;

    public override string ToString()
    {
        return base.ToString() + " " + Cost + " " + Make + " " + MegaPixels;
    }

    public override string get_type()
    {
        return "Camera";
    }

    public Camera() : this(0, 0, "") {}
}