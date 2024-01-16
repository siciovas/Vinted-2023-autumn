namespace BackendAssignment;

public class CalculationProperties
{
    private readonly decimal _price;
    private readonly decimal? _discount;
    private readonly DateTime? _previousDate;

    /// <summary>
    /// Initialization of the new instance of the <see cref="CalculationProperties"/> class
    /// </summary>
    /// <param name="price"></param>
    /// <param name="discount"></param>
    /// <param name="previousDate"></param>
    public CalculationProperties(decimal price, decimal? discount, DateTime? previousDate)
    {
        _price = price;
        _discount = discount;
        _previousDate = previousDate;
    }

    public decimal Price => _price;
    public decimal? Discount => _discount;
    public DateTime? PreviousDate => _previousDate;
}