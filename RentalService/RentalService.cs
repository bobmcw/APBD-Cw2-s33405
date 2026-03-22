namespace ConsoleApp1.RentalService;

using User;
using Device;
public class RentalService
{
    private List<Device> _inventory;
    private List<RentStatus> _currentRents;
    private List<RentStatus> _history;

    void RentDevice(int id, User user, DateTime returnDate)
    {
        var items = _inventory.Where(i => i.Id == id).ToList();
        if (items.Count == 1)
        {
            var dev = items.First();
            _currentRents.Add(new RentStatus(user, dev, returnDate));
        }
        else
        {
            throw new Exception("element with such ID not found");
        }
    }

    void ReturnDevice(int id)
    {
        var items = _currentRents.Where(i => i.DeviceId() == id).ToList();
        if (items.Count == 1)
        {
            items[0].returnDevice();
            _history.Add(items[0]);
            _currentRents.Remove(items[0]);
        }
    }
}