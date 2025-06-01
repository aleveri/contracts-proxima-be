using ContractsApi.Application.Interfaces;
using ContractsApi.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ContractsApi.Infrastructure.Repositories
{
    public class TarifaRepository(IConfiguration configuration) : ITarifaRepository
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Missing connection string.");

        public async Task<List<Tarifa>> GetAllAsync()
        {
            var tarifas = new List<Tarifa>();

            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                var cmd = new NpgsqlCommand("SELECT id, nombre, precio FROM tarifas", conn);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    tarifas.Add(new Tarifa
                    {
                        Id = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        Precio = reader.GetDecimal(2)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las tarifas.", ex);
            }

            return tarifas;
        }
    }
}
