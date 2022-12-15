using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_API.Attributes;
using Restaurant_API.Models;
using Restaurant_API.Models.DTO;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class DishesController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public DishesController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/Dishes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dish>>> GetDishes()
        {
            return await _context.Dishes.ToListAsync();
        }

        // GET: api/Dishes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> GetDish(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            return dish;
        }

        // GET: api/Dishes/GetDishData?idDish=1
        [HttpGet("GetDishData")]
        public ActionResult<IEnumerable<DishDTO>> GetUserInfo(int idDish)
        {
            //las consultas linq se parecen mucho a las normales que hemos hecho en T-SQL
            //una de las diferencias es que podemos usar una "tabla temporal" para almacenar
            //los resultados y luego usarla para llenar los atributos de un modelo o DTO

            var query = (from dish in _context.Dishes
                         join country in _context.Countries on dish.Idcountry equals country.Idcountry
                         where dish.Iddish == idDish
                         select new
                         {
                             Iddish = dish.Iddish,
                             ItemPictureUrl = dish.ItemPictureUrl,
                             DishDescription = dish.DishDescription,
                             Idcountry = country.Idcountry,
                         }).ToList();

            List<DishDTO> list = new List<DishDTO>();

            foreach (var dish in query)
            {
                DishDTO NewItem = new DishDTO();

                NewItem.Iddish = dish.Iddish;
                NewItem.ItemPictureUrl = dish.ItemPictureUrl;
                NewItem.DishDescription = dish.DishDescription;
                NewItem.Idcountry = dish.Idcountry;

                list.Add(NewItem);

            }

            if (list == null)
            {
                return NotFound();
            }

            return list;

        }

        [HttpGet("GetDishesList")]
        public ActionResult<IEnumerable<DishDTO>> GetDishesList()
        {
            var query = from dish in _context.Dishes
                        select new
                        {
                            Iddish = dish.Iddish,
                            ItemPictureUrl = dish.ItemPictureUrl,
                            DishDescription = dish.DishDescription,
                            Idcountry = dish.Idcountry
                        };

            List<DishDTO> DishesList = new List<DishDTO>();

            foreach (var dish in query)
            {
                DishesList.Add(
                    new DishDTO
                    {
                        Iddish = dish.Iddish,
                        ItemPictureUrl = dish.ItemPictureUrl,
                        DishDescription = dish.DishDescription,
                        Idcountry = dish.Idcountry
                    }
                    );
            }

            if (DishesList == null)
            {
                return NotFound();
            }

            return DishesList;

        }

        // PUT: api/Dishes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDish(int id, Dish dish)
        {
            if (id != dish.Iddish)
            {
                return BadRequest();
            }

            _context.Entry(dish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Dishes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dish>> PostDish(Dish dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDish", new { id = dish.Iddish }, dish);
        }

        // DELETE: api/Dishes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }

            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.Iddish == id);
        }
    }
}
