namespace ContractsApi.Domain.Entities;

public class Tarifa
{
    public int Id { get; set; }
    public required string Descripcion { get; set; }
    public decimal Precio { get; set; }
}