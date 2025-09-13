using Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IFoodServices _foodServices;

        public FoodController(IFoodServices foodServices)
        {
            _foodServices = foodServices;
        }

        [HttpGet]
        public async Task<IEnumerable<FoodDTO>> GetAll()
        {
            return await _foodServices.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> Add(String name,double price , String category)
        {

            FoodDTO food = FoodDTO.CreateFoodDTO(name, price, category);
            if (food == null)
                return BadRequest("Food data is required.");

            await _foodServices.Add(name, price, category);
            return Ok("Food item added successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _foodServices.Delete(id);
            return Ok($"Food item with ID {id} deleted.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, String name ,double price ,String category)
        {
            FoodDTO food = FoodDTO.CreateFoodDTO(name, price, category);
            if (food == null)
                return BadRequest("Food data is required.");

            await _foodServices.Update(id, name,price, category);
            return Ok($"Food item with ID {id} updated.");
        }
    }
}
