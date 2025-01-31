namespace DemoElasticSearch.Api.Models;

public class Cliente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime Age { get; set; }
    public string Description { get; set; }
}