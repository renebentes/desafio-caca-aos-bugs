namespace Balta.Domain.AccountContext.Entities;

public class Resume
{
    public string Goals { get; set; }
    public string Summary { get; set; }
    public List<Expertise> Expertises { get; set; }
}