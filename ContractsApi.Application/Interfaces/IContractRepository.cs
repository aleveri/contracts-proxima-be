using ContractsApi.Domain.Entities;

namespace ContractsApi.Application.Interfaces;

public interface IContractRepository
{
    Task<IEnumerable<Contract>> GetAllAsync();
    Task<Contract> GetByIdAsync(int id);
    Task CreateAsync(Contract contract);
    Task UpdateAsync(Contract contract);
}