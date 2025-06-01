using ContractsApi.Domain.Entities;

namespace ContractsApi.Application.Interfaces
{
    public interface ITarifaRepository
    {
        Task<List<Tarifa>> GetAllAsync();
    }
}
