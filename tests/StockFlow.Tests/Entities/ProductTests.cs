using StockFlow.Domain.Entities;

namespace StockFlow.Tests.Entities;

public class ProductTests
{
    private static readonly Guid CategoryId = Guid.NewGuid();

    [Fact]
    public void Constructor_WithValidData_ShouldCreateProduct()
    {
        var product = new Product(
            "Teclado mecânico",
            "Teclado ABNT2 com iluminação",
            249.90m,
            12,
            5,
            CategoryId);

        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.Equal("Teclado mecânico", product.Name);
        Assert.Equal("Teclado ABNT2 com iluminação", product.Description);
        Assert.Equal(249.90m, product.Price);
        Assert.Equal(12, product.Quantity);
        Assert.Equal(5, product.MinimumStock);
        Assert.Equal(CategoryId, product.CategoryId);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        var action = () => new Product(
            "",
            "Produto inválido",
            100m,
            10,
            2,
            CategoryId);

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
            2,
            CategoryId);

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
            2,
            CategoryId);

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
            -1,
            CategoryId);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Constructor_WithEmptyCategoryId_ShouldThrowArgumentException()
    {
        var action = () => new Product(
            "Mouse",
            "Mouse sem fio",
            50m,
            10,
            2,
            Guid.Empty);

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void AddStock_WithValidQuantity_ShouldIncreaseQuantity()
    {
        var product = CreateProduct(quantity: 10);

        product.AddStock(5);

        Assert.Equal(15, product.Quantity);
    }

    [Fact]
    public void AddStock_WithZeroQuantity_ShouldThrowArgumentOutOfRangeException()
    {
        var product = CreateProduct(quantity: 10);

        var action = () => product.AddStock(0);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void RemoveStock_WithValidQuantity_ShouldDecreaseQuantity()
    {
        var product = CreateProduct(quantity: 10);

        product.RemoveStock(4);

        Assert.Equal(6, product.Quantity);
    }

    [Fact]
    public void RemoveStock_WithQuantityGreaterThanStock_ShouldThrowInvalidOperationException()
    {
        var product = CreateProduct(quantity: 10);

        var action = () => product.RemoveStock(11);

        Assert.Throws<InvalidOperationException>(action);
    }

    [Fact]
    public void ChangePrice_WithValidPrice_ShouldUpdatePrice()
    {
        var product = CreateProduct();

        product.ChangePrice(199.90m);

        Assert.Equal(199.90m, product.Price);
    }

    [Fact]
    public void IsBelowMinimumStock_WhenQuantityIsLower_ShouldReturnTrue()
    {
        var product = CreateProduct(
            quantity: 4,
            minimumStock: 5);

        var result = product.IsBelowMinimumStock();

        Assert.True(result);
    }

    private static Product CreateProduct(
        int quantity = 10,
        int minimumStock = 5)
    {
        return new Product(
            "Teclado",
            "Teclado mecânico",
            250m,
            quantity,
            minimumStock,
            CategoryId);
    }
}