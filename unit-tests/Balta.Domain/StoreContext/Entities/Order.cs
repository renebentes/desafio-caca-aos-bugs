using Balta.Domain.AccountContext.Entities;
using Balta.Domain.CompanyContext.Entities;

namespace Balta.Domain.StoreContext.Entities;

public class Order
{
    public string Number { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public int Status { get; set; }

    public string BillingAddress { get; set; }

    public string PaymentInfo { get; set; } // Gateway, ExternalRef, Installments, etc
    
    public List<OrderLine> Lines { get; set; }
    
    // Criar interface para tornar o campo único
    public Customer Customer { get; set; }
}

public class OrderLine
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}

// new OrderLine("Premium", 1, 1017, 1017) // Compra PF (Compra PF limitada a um produto de cada)
// new OrderLine("Premium", 10, 1017, 10000) // Compra Corp