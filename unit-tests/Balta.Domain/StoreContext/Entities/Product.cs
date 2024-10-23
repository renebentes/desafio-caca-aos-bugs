namespace Balta.Domain.StoreContext.Entities;

public class Product
{
    public string Name { get; set; }
    public int DurationInDays { get; set; } // Zero para compra única
    public string Role { get; set; } // Perfil a ser atribuído ao aluno
    public decimal Price { get; set; }
    
    // var product = new Product("Premium", 365, "premium", 1017m);
    // var product = new Product("Pro", 365, "pro", 1499m);
    // var product = new Product("Fullstack .NET", 0, "3054", 699m);
}

public class Plan : Product;

public class Course : Product;