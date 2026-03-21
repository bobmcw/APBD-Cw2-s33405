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
    private State _state = State.Available;

    void rentDevice(User.User user, DateTime returnDate)
    {
        if (_state == State.Available)
        {
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
    
    void returnDevice()
    {
        _state = State.Returned;
        _returnDate = DateTime.Now;
    }
}