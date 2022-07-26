using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiPostgreSQL.Model;

namespace ApiPostgreSQL.Data.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Auto>> GetAllCars();
        Task<Auto> GetCarDetails(int id);
        Task<bool> InsertCar(Auto car);
        Task<bool> UpdateCar(Auto car);
        Task<bool> DeleteCar(Auto car);
    }
}
