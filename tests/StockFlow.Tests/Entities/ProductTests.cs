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
    [Fact]
public void AddStock_WithValidQuantity_ShouldIncreaseQuantity()
{
    var product = new Product(
        "Teclado",
        "Teclado mecânico",
        250m,
        10,
        5);

    product.AddStock(5);

    Assert.Equal(15, product.Quantity);
}

[Fact]
public void AddStock_WithZeroQuantity_ShouldThrowArgumentOutOfRangeException()
{
    var product = new Product(
        "Teclado",
        "Teclado mecânico",
        250m,
        10,
        5);

    var action = () => product.AddStock(0);

    Assert.Throws<ArgumentOutOfRangeException>(action);
}

[Fact]
public void RemoveStock_WithValidQuantity_ShouldDecreaseQuantity()
{
    var product = new Product(
        "Teclado",
        "Teclado mecânico",
        250m,
        10,
        5);

    product.RemoveStock(4);

    Assert.Equal(6, product.Quantity);
}

[Fact]
public void RemoveStock_WithQuantityGreaterThanStock_ShouldThrowInvalidOperationException()
{
    var product = new Product(
        "Teclado",
        "Teclado mecânico",
        250m,
        10,
        5);

    var action = () => product.RemoveStock(11);

    Assert.Throws<InvalidOperationException>(action);
}

[Fact]
public void ChangePrice_WithValidPrice_ShouldUpdatePrice()
{
    var product = new Product(
        "Teclado",
        "Teclado mecânico",
        250m,
        10,
        5);

    product.ChangePrice(199.90m);

    Assert.Equal(199.90m, product.Price);
}

[Fact]
public void IsBelowMinimumStock_WhenQuantityIsLower_ShouldReturnTrue()
{
    var product = new Product(
        "Teclado",
        "Teclado mecânico",
        250m,
        4,
        5);

    var result = product.IsBelowMinimumStock();

    Assert.True(result);
}
}