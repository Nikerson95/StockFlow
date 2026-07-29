namespace StockFlow.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    private Category()
    {
    }

    public Category(string name, string description)
    {
        ValidateName(name);

        Id = Guid.NewGuid();
        Name = name.Trim();
        Description = description.Trim();
    }

    public void Update(string name, string description)
    {
        ValidateName(name);

        Name = name.Trim();
        Description = description.Trim();
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "O nome da categoria é obrigatório.",
                nameof(name));
        }

        if (name.Length > 100)
        {
            throw new ArgumentException(
                "O nome da categoria deve ter no máximo 100 caracteres.",
                nameof(name));
        }
    }
}