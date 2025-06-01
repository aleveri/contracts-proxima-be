using ContractsApi.Application.Interfaces;
using ContractsApi.Domain.Entities;

namespace ContractsApi.Application.UseCases
{
    public class TarifaService(ITarifaRepository repository)
    {
        public Task<List<Tarifa>> GetAllTarifasAsync()
        {
            return repository.GetAllAsync();
        }
    }
}
