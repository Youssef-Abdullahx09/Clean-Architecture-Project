namespace Domain.Entities;

public class Gathering
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Type { get; set; }
    public Guid Creator { get; set; }
    public string ScheduledAt { get; set; }
}
