using StockFlow.Domain.Entities;

namespace StockFlow.Tests.Entities;

public class ProductTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateProduct()
    {
        var product = new Product(
            "Teclado mecânico",
            "Teclado ABNT2 com iluminação",
            249.90m,
            12,
            5);

        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.Equal("Teclado mecânico", product.Name);
        Assert.Equal(249.90m, product.Price);
        Assert.Equal(12, product.Quantity);
        Assert.Equal(5, product.MinimumStock);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        var action = () => new Product(
            "",
            "Produto inválido",
            100m,
            10,
            2);

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Constructor_WithNegativePrice_ShouldThrowArgumentOutOfRangeException()
    {
        var action = () => new Product(
            "Mouse",
            "Mouse sem fio",
            -50m,
            10,
            2);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Constructor_WithNegativeQuantity_ShouldThrowArgumentOutOfRangeException()
    {
        var action = () => new Product(
            "Mouse",
            "Mouse sem fio",
            50m,
            -1,
            2);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Constructor_WithNegativeMinimumStock_ShouldThrowArgumentOutOfRangeException()
    {
        var action = () => new Product(
            "Mouse",
            "Mouse sem fio",
            50m,
            10,
            -1);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }
}