using ApiPostgreSQL.Data.Repositories;
using ApiPostgreSQL.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiPostgreSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : Controller
    {
        public readonly ICarRepository _carRepo;
        public AutoController(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            return Ok(await _carRepo.GetAllCars());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarDetail(int id)
        {
            return Ok(await _carRepo.GetCarDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] Auto car)
        {
            if (car == null)
                return BadRequest();

            if(ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _carRepo.InsertCar(car);

            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCar([FromBody] Auto car)
        {
            if (car == null)
                return BadRequest();

            if (ModelState.IsValid)
                return BadRequest(ModelState);

            await _carRepo.UpdateCar(car);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carRepo.DeleteCar(new Auto { Id = id});

            return NoContent();
        }
    }
}
