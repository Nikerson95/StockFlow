using StockFlow.Domain.Entities;

namespace StockFlow.Tests.Entities;

public class CategoryTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateCategory()
    {
        var category = new Category(
            "Periféricos",
            "Teclados, mouses e acessórios");

        Assert.NotEqual(Guid.Empty, category.Id);
        Assert.Equal("Periféricos", category.Name);
        Assert.Equal(
            "Teclados, mouses e acessórios",
            category.Description);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        var action = () => new Category(
            "",
            "Descrição da categoria");

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateCategory()
    {
        var category = new Category(
            "Informática",
            "Produtos de informática");

        category.Update(
            "Periféricos",
            "Teclados, mouses e acessórios");

        Assert.Equal("Periféricos", category.Name);
        Assert.Equal(
            "Teclados, mouses e acessórios",
            category.Description);
    }

    [Fact]
    public void Update_WithEmptyName_ShouldThrowArgumentException()
    {
        var category = new Category(
            "Informática",
            "Produtos de informática");

        var action = () => category.Update(
            "",
            "Descrição inválida");

        Assert.Throws<ArgumentException>(action);
    }
}