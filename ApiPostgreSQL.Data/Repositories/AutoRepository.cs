using ApiPostgreSQL.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPostgreSQL.Data.Repositories
{
    public class AutoRepository : ICarRepository
    {
        private PostgreSQLConfig _config;
        public AutoRepository(PostgreSQLConfig config)
        {
            _config = config;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_config.ConnectionString);
        }

        public async Task<bool> DeleteCar(Auto car)
        {
            var db = dbConnection();
            var sql = @"
                        DELETE
                        FROM public.""Cars""
                            WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new { Id = car.Id });
            return result > 0;
        }

        public async Task<IEnumerable<Auto>> GetAllCars()
        {
            var db = dbConnection();
            var sql = @"
                        SELECT id, modelo, marca, color, año, puertas
                        FROM public.""Cars""
                        ";

            return await db.QueryAsync<Auto>(sql, new { });
        }

        public async Task<Auto> GetCarDetails(int id)
        {
            var db = dbConnection();
            var sql = @"
                        SELECT id, modelo, marca, color, año, puertas
                        FROM public.""Cars""
                        WHERE id = @Id";

            return await db.QueryFirstOrDefaultAsync<Auto>(sql, new { Id = id });
        }

        public async Task<bool> InsertCar(Auto car)
        {
            var db = dbConnection();
            var sql = @"
                        INSERT INTO public.""Cars""(modelo, marca, color, año, puertas)
                        VALUES(@Marca, @Modelo, @Color, @Año, @Puertas);";

            var result = await db.ExecuteAsync(sql, new { car.Marca, car.Modelo, car.Color, car.Año, car.Puertas});
            return result > 0;
        }

        public async Task<bool> UpdateCar(Auto car)
        {
            var db = dbConnection();
            var sql = @"
                        UPDATE public.""Cars""
                        SET marca = @Marca,
                            modelo = @Modelo,
                            color = @Color,
                            año = @Año,
                            puertas = @Puertas,
                        WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new { car.Marca, car.Modelo, car.Color, car.Año, car.Puertas, car.Id });
            return result > 0;
        }
    }
}
