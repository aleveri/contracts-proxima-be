namespace ContractsApi.Domain.Entities;

public class Contract
{
    public int Id { get; set; }
    public required string Dni { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public int TarifaId { get; set; }
    public DateTime FechaContratacion { get; set; }
}