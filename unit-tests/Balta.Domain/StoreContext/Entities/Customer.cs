using Balta.Domain.AccountContext.Entities;
using Balta.Domain.CompanyContext.Entities;

namespace Balta.Domain.StoreContext.Entities;

public class Customer
{
    public Student? Account { get; set; }
    public Company? Company { get; set; }
}