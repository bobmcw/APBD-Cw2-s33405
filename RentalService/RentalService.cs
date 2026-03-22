namespace ConsoleApp1.RentalService;

using User;
using Device;
public class RentalService
{
    public List<Device> Inventory { get; } = new List<Device>();
    public List<RentStatus> CurrentRents { get; } = new List<RentStatus>();
    public List<RentStatus> History { get; } = new List<RentStatus>();

    public void AddDeviceToInventory(Device dev)
    {
        Inventory.Add(dev);
    }
    public void RentDevice(int id, User user, DateTime returnDate)
    {
        var items = Inventory.Where(i => i.Id == id).ToList();
        if (items.Count == 1)
        {
            var dev = items.First();
            CurrentRents.Add(new RentStatus(user, dev, returnDate));
        }
        else
        {
            throw new Exception("element with such ID not found");
        }
    }

    public void ReturnDevice(int id)
    {
        var items = CurrentRents.Where(i => i.DeviceId() == id).ToList();
        if (items.Count == 1)
        {
            items[0].returnDevice();
            History.Add(items[0]);
            CurrentRents.Remove(items[0]);
        }
    }
}