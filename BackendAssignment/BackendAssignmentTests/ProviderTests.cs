using BackendAssignment;

namespace BackendAssignmentTests;

public class ProviderTests
{
    [Fact]
    public void IsValidProvider_ProviderIsLP_ReturnsTrue()
    {
        // Act
        var isValidProvider = Calculation.IsValidProvider("LP", out var actualProvider);

        // Assert
        Assert.True(isValidProvider);
        Assert.Equal(Provider.LP, actualProvider);
    }

    [Fact]
    public void IsValidProvider_ProviderIsMR_ReturnsTrue()
    {
        // Act
        var isValidProvider = Calculation.IsValidProvider("MR", out var actualProvider);

        // Assert
        Assert.True(isValidProvider);
        Assert.Equal(Provider.MR, actualProvider);
    }

    [Fact]
    public void IsValidProvider_ProviderIsEmpty_ReturnsFalse()
    {
        // Act
        var isValidProvider = Calculation.IsValidProvider("", out _);

        // Assert
        Assert.False(isValidProvider);
    }

    [Fact]
    public void IsValidProvider_ProviderIsNotCorrect_ReturnsFalse()
    {
        // Act
        var isValidProvider = Calculation.IsValidProvider("WrongProviderTest", out _);

        // Assert
        Assert.False(isValidProvider);
    }
}