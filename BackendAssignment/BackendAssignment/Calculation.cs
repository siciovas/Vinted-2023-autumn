using System.Globalization;

namespace BackendAssignment;

public class Calculation
{
    private DateTime? _previousDate;
    private decimal _monthlyDiscount = Constants.MonthlyDiscount;
    private int _lpShipmentTimes = Constants.LpShipmentCounter;

    private record PackageInfo(Provider Provider, Size Size, decimal Price);
 
    private readonly List<PackageInfo> _packagesInfo = new()
    {
        new PackageInfo(Provider.LP, Size.S, 1.50m),
        new PackageInfo(Provider.LP, Size.M, 4.90m),
        new PackageInfo(Provider.LP, Size.L, 6.90m),
        new PackageInfo(Provider.MR, Size.S, 2),
        new PackageInfo(Provider.MR, Size.M, 3),
        new PackageInfo(Provider.MR, Size.L, 4),
    };

    /// <summary>
    /// Reads the data from the input data file. If the line in the file is valid, the record is sent for calculations and after calculations date, size, provider,
    /// price with the discount, and a shipment discount (or '-' if there is none) are printed
    /// If the line format is wrong or carrier/sizes are unrecognized, it prints whole line and appends 'Ignored' word
    /// </summary>
    /// <param name="fileName">Name of the data input file</param>
    public void Execute(string fileName = Constants.FileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("The file does not exist!");
            return;
        }

        var lines = File.ReadAllLines(fileName);

        if (lines.Length == 0)
        {
            Console.WriteLine("The file is empty!");
            return;
        }

        foreach (var line in lines)
        {
            var parts = line.Split(' ');

            if (parts.Length != 3)
            {
                Console.WriteLine(Constants.IgnoredLine, line);
                continue;
            }

            if (DateTime.TryParseExact(parts[0], Constants.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)
                && IsValidSize(parts[1], out var size)
                && IsValidProvider(parts[2], out var provider))
            {
                var shipment = new Shipment(date, size, provider);
                var result = Calculate(shipment, _previousDate);
                _previousDate = result.PreviousDate;
                var discount = result.Discount != null ? result.Discount.ToString() : "-";
                Console.WriteLine($"{shipment.Date.ToString(Constants.DateFormat)} {shipment.Size} {shipment.Provider} {result.Price:F2} {discount:F2}");
            }

            else
            {
                Console.WriteLine(Constants.IgnoredLine, line);
            }
        }
    }

    /// <summary>
    /// All rules calculation
    /// </summary>
    /// <param name="shipment">Shipment object</param>
    /// <param name="previousDate">Previous shipment date</param>
    /// <returns>If no rules were applied for the shipment, it returns the current shipment price, null for the discount and shipment date</returns>
    private CalculationProperties Calculate(Shipment shipment, DateTime? previousDate)
    {
        if (shipment.Date.Year != previousDate.GetValueOrDefault().Year || shipment.Date.Month != previousDate.GetValueOrDefault().Month)
        {
            ResetNewMonthValues();
        }

        var currentShipmentPrice = GetCurrentShipmentPrice(shipment);

        if (shipment.Size == Size.S)
        {
            return CalculateFirstRuleResult(currentShipmentPrice, shipment);
        }

        if (shipment.Size == Size.L && shipment.Provider == Provider.LP)
        {
            _lpShipmentTimes++;

            if (_lpShipmentTimes == Constants.FreeDelivery)
            {
                return CalculateSecondRuleResult(currentShipmentPrice, shipment);
            }

        }

        return new CalculationProperties(currentShipmentPrice, null, shipment.Date);
    }

    /// <summary>
    /// Calculates that all S shipments should always match the lowest S package price among the providers
    /// </summary>
    /// <param name="currentShipmentPrice">The current shipment price</param>
    /// <param name="shipment">Shipment object</param>
    /// <returns>Returns calculated price with the discount, discount price or null, if no discount was given and shipment date</returns>
    public CalculationProperties CalculateFirstRuleResult(decimal currentShipmentPrice, Shipment shipment) 
    {
        var lowestShipmentPrice = GetLowestShipmentPrice();
        var primaryDiscount = currentShipmentPrice - lowestShipmentPrice;
        var finalDiscount = _monthlyDiscount < primaryDiscount ? _monthlyDiscount : primaryDiscount;
        var priceWithDiscount = currentShipmentPrice - finalDiscount;
        _monthlyDiscount -= finalDiscount;

        return new CalculationProperties(priceWithDiscount, finalDiscount != 0 ? finalDiscount : null, shipment.Date);
    }

    /// <summary>
    /// Calculates that the third L shipment via LP should be free, but only once a calendar month
    /// </summary>
    /// <param name="currentShipmentPrice">The current shipment price</param>
    /// <param name="shipment">Shipment object</param>
    /// <returns>Returns calculated price with the discount, discount price and shipment date</returns>
    public CalculationProperties CalculateSecondRuleResult(decimal currentShipmentPrice, Shipment shipment)
    {
        var finalDiscount = _monthlyDiscount < currentShipmentPrice ? _monthlyDiscount : currentShipmentPrice;
        var priceWithDiscount = currentShipmentPrice - finalDiscount;
        _monthlyDiscount -= finalDiscount;

        return new CalculationProperties(priceWithDiscount, currentShipmentPrice, shipment.Date);
    }

    /// <summary>
    /// Gets the lowest price for S size
    /// </summary>
    /// <returns>Returns decimal value</returns>
    public decimal GetLowestShipmentPrice()
    {
        return _packagesInfo.Where(packageInfo => packageInfo.Size == Size.S).Min(packageInfo => packageInfo.Price);
    }

    /// <summary>
    /// Gets the current shipment price
    /// </summary>
    /// <param name="shipment">Shipment object</param>
    /// <returns>Returns decimal value</returns>
    public decimal GetCurrentShipmentPrice(Shipment shipment)
    {
        return _packagesInfo.Single(packageInfo => packageInfo.Provider == shipment.Provider && packageInfo.Size == shipment.Size).Price;
    }

    /// <summary>
    /// Resets monthly values
    /// </summary>
    public void ResetNewMonthValues()
    {
        _monthlyDiscount = Constants.MonthlyDiscount;
        _lpShipmentTimes = Constants.LpShipmentCounter;
    }

    /// <summary>
    /// Checks if the size is valid
    /// </summary>
    /// <param name="input">Size value in the input data</param>
    /// <param name="size">Size enum</param>
    /// <returns>Returns boolean value</returns>
    public static bool IsValidSize(string input, out Size size)
    {
        return Enum.TryParse(input, out size);
    }

    /// <summary>
    /// Checks if the provider is valid
    /// </summary>
    /// <param name="input">Provider value in the input data</param>
    /// <param name="provider">Provider enum</param>
    /// <returns>Returns boolean value</returns>
    public static bool IsValidProvider(string input, out Provider provider)
    {
        return Enum.TryParse(input, out provider);
    }
}