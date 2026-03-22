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
    private User.User? _user;
    private Device.Device? _device;
    private State _state = State.Available;

    public RentStatus(User.User user, Device.Device device,DateTime returnDate)
    {
        if (_state == State.Available)
        {
            _device = device;
            _state = State.Rented;
            _start = DateTime.Now;
            _user = user;
            _declaredReturnDate = returnDate;
        }
        else
        {
            throw new Exception("device is not available");
        }
    }
    
    public void returnDevice()
    {
        _state = State.Returned;
        _returnDate = DateTime.Now;
    }
}