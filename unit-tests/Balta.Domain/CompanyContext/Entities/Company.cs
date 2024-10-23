using Balta.Domain.AccountContext.Entities;
using Balta.Domain.SharedContext.Entities;
using Balta.Domain.StoreContext.Entities;

namespace Balta.Domain.CompanyContext.Entities;

public sealed class Company(Guid id) : Entity(id)
{
    public string Name { get; set; }
    public List<Position> Positions { get; set; }

    public List<License> Licenses { get; set; }
    
}

public class License
{
    public Position Position { get; set; }
    public Plan Plan { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public DateTime EndedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}

public class Position
{
    public Student Student { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}

public class Job
{
    public Company Company { get; set; }
    public string Title { get; set; }
    public string Tags { get; set; } // Tags Relacionadas
    public string Exams { get; set; } // Exames relacionados ou obrigatórios
}

public class JobApplication
{
    public Job Job { get; set; }
    public Student Type { get; set; }
}