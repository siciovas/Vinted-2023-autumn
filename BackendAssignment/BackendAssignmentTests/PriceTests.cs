using AutoFixture;
using BackendAssignment;

namespace BackendAssignmentTests;

public class PriceTests
{
    private readonly Fixture _fixture = new();
    private readonly Calculation _calculations = new();

    [Fact]
    public void GetLowestShipmentPrice_ShipmentPriceIsLowest_ReturnsCorrectLowestShipmentPrice()
    {
        // Arrange
        const decimal expectedValue = 1.50m;

        // Act
        var actualPrice = _calculations.GetLowestShipmentPrice();

        // Assert
        Assert.Equal(expectedValue, actualPrice);
    }

    [Fact]
    public void GetCurrentShipmentPrice_CurrentShipmentPriceIsCorrect_ReturnsCorrectCurrentShipmentPrice()
    {
        // Arrange
        const decimal expectedPrice = 1.50m;
        var shipment = new Shipment(_fixture.Create<DateTime>(), Size.S, Provider.LP);

        // Act
        var actualPrice = _calculations.GetCurrentShipmentPrice(shipment);

        // Assert
        Assert.Equal(expectedPrice, actualPrice);
    }
}