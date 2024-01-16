using BackendAssignment;

namespace BackendAssignmentTests;

public class SizeTests
{
    [Fact]
    public void IsValidSize_SizeIsS_ReturnsTrue()
    {
        // Act
        var isValidSize = Calculation.IsValidSize("S", out var actualSize);

        // Assert
        Assert.True(isValidSize);
        Assert.Equal(Size.S, actualSize);
    }

    [Fact]
    public void IsValidSize_SizeIsM_ReturnsTrue()
    {
        // Act
        var isValidSize = Calculation.IsValidSize("M", out var actualSize);

        // Assert
        Assert.True(isValidSize);
        Assert.Equal(Size.M, actualSize);
    }

    [Fact]
    public void IsValidSize_SizeIsL_ReturnsTrue()
    {
        // Act
        var isValidSize = Calculation.IsValidSize("L", out var actualSize);

        // Assert
        Assert.True(isValidSize);
        Assert.Equal(Size.L, actualSize);
    }

    [Fact]
    public void IsValidSize_SizeIsEmpty_ReturnsFalse()
    {
        // Act
        var isValidSize = Calculation.IsValidSize("", out _);

        // Assert
        Assert.False(isValidSize);
    }

    [Fact]
    public void IsValidSize_SizeIsNotCorrect_ReturnsFalse()
    {
        // Act
        var isValidSize = Calculation.IsValidSize("WrongSizeTest", out _);

        // Assert
        Assert.False(isValidSize);
    }
}