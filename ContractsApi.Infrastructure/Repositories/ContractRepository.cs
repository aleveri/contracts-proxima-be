using ContractsApi.Application.Interfaces;
using ContractsApi.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ContractsApi.Infrastructure.Repositories;

public class ContractRepository(IConfiguration configuration) : IContractRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    public async Task<IEnumerable<Contract>> GetAllAsync()
    {
        try
        {
            var contracts = new List<Contract>();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = new NpgsqlCommand("SELECT * FROM contracts", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                contracts.Add(new Contract
                {
                    Id = reader.GetInt32(0),
                    Dni = reader.GetString(1),
                    Nombre = reader.GetString(2),
                    Apellido = reader.GetString(3),
                    TarifaId = reader.GetInt32(4),
                    FechaContratacion = reader.GetDateTime(5)
                });
            }
            return contracts.OrderBy(x => x.Nombre);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener los contratos.", ex);
        }
    }

    public async Task<Contract> GetByIdAsync(int id)
    {
        try
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = new NpgsqlCommand("SELECT * FROM contracts WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Contract
                {
                    Id = reader.GetInt32(0),
                    Dni = reader.GetString(1),
                    Nombre = reader.GetString(2),
                    Apellido = reader.GetString(3),
                    TarifaId = reader.GetInt32(4),
                    FechaContratacion = reader.GetDateTime(5)
                };
            }

            throw new InvalidOperationException($"Contrato con ID {id} no encontrado.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener el contrato con ID {id}.", ex);
        }
    }

    public async Task CreateAsync(Contract contract)
    {
        try
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = new NpgsqlCommand("INSERT INTO contracts (dni, nombre, apellido, tarifa_id, fecha_contratacion) VALUES (@dni, @nombre, @apellido, @tarifa_id, @fecha)", conn);
            cmd.Parameters.AddWithValue("dni", contract.Dni);
            cmd.Parameters.AddWithValue("nombre", contract.Nombre);
            cmd.Parameters.AddWithValue("apellido", contract.Apellido);
            cmd.Parameters.AddWithValue("tarifa_id", contract.TarifaId);
            cmd.Parameters.AddWithValue("fecha", contract.FechaContratacion);
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error al crear el contrato.", ex);
        }
    }

    public async Task UpdateAsync(Contract contract)
    {
        try
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = new NpgsqlCommand("UPDATE contracts SET dni = @dni, nombre = @nombre, apellido = @apellido, tarifa_id = @tarifa_id, fecha_contratacion = @fecha WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", contract.Id);
            cmd.Parameters.AddWithValue("dni", contract.Dni);
            cmd.Parameters.AddWithValue("nombre", contract.Nombre);
            cmd.Parameters.AddWithValue("apellido", contract.Apellido);
            cmd.Parameters.AddWithValue("tarifa_id", contract.TarifaId);
            cmd.Parameters.AddWithValue("fecha", contract.FechaContratacion);
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al actualizar el contrato con ID {contract.Id}.", ex);
        }
    }
}