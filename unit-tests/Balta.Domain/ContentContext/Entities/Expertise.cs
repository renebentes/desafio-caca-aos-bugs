using Balta.Domain.ContentContext.Entities;

namespace Balta.Domain.AccountContext.Entities;

public class Expertise
{
    public Tag Tag { get; set; }
    public int Level { get; set; }
    public List<Exam> Exams { get; set; }
    public List<Project> Projects { get; set; }
}