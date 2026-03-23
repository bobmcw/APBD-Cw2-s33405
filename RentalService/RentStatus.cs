namespace ConsoleApp1.RentalService;

public class RentStatus
{
    public enum State
    {
        Available,
        Rented,
        Returned,
    }
    
    private DateTime _start;
    private DateTime _declaredReturnDate;
    private DateTime _returnDate;
    public User.User? User { get; }
    public Device.Device? Device { get; }
    private State _state = State.Available;

    public RentStatus(User.User user, Device.Device device,DateTime returnDate)
    {
        if (_state == State.Available)
        {
            Device = device;
            _state = State.Rented;
            _start = DateTime.Now;
            User = user;
            _declaredReturnDate = returnDate;
        }
        else
        {
            throw new Exception("device is not available");
        }
    }

    public int DeviceId()
    {
        if (Device != null)
        {
            return Device.Id;
        }

        throw new Exception("this rent status does not have a device");
    }

    public int UserId()
    {
        if (User != null) return User.Id;
        return -1;
    }

    public void returnDevice()
    {
        _state = State.Returned;
        _returnDate = DateTime.Now;
    }

    public override string ToString()
    {
        return Device + _state.ToString();
    }
}