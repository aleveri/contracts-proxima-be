using ContractsApi.Application.Interfaces;
using ContractsApi.Domain.Entities;

namespace ContractsApi.Application.UseCases;

public class ContractService
{
    private readonly IContractRepository _repository;

    public ContractService(IContractRepository repository) => _repository = repository;

    public Task<IEnumerable<Contract>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Contract> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task CreateAsync(Contract contract) => _repository.CreateAsync(contract);
    public Task UpdateAsync(Contract contract) => _repository.UpdateAsync(contract);
}