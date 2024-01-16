namespace BackendAssignment;

public class Shipment
{
    private readonly DateTime _date;
    private readonly Size _size;
    private readonly Provider _provider;

    /// <summary>
    /// Initialization of the new instance of the <see cref="Shipment"/> class
    /// </summary>
    /// <param name="date">The date of the shipment</param>
    /// <param name="size">The package size of the shipment</param>
    /// <param name="provider">The provider of the shipment</param>
    public Shipment(DateTime date, Size size, Provider provider)
    {
        _date = date;
        _size = size;
        _provider = provider;
    }

    public DateTime Date => _date;
    public Size Size => _size;
    public Provider Provider => _provider;
}