namespace ConsoleApp1.RentalService;

using User;
using Device;
public class RentalService
{
    private List<Device> _inventory;
    private List<RentStatus> _currentRents;
    private List<RentStatus> _history;

    void rentDevice(int id, User user, DateTime returnDate)
    {
        var res = _inventory.Where(i => i.Id == id);
        var items = res.ToList();
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
}