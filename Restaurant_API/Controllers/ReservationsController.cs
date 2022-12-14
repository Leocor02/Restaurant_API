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
    public class ReservationsController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public ReservationsController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }
        
    [HttpGet("GetUserReservationsList/{id}")]
    public ActionResult<IEnumerable<ReservationDTO>> GetUserReservationsList(int id)
    {
            var query = from reservation in _context.Reservations
                        where reservation.Iduser == id
                    select new
                    {
                        Idreservation = reservation.Idreservation,
                        Date = reservation.Date,
                        DinersQuantity = reservation.DinersQuantity,
                        Iduser = reservation.Iduser,
                        Idtable = reservation.Idtable
                    };

        List<ReservationDTO> ReservationsList = new List<ReservationDTO>();

        foreach (var reservation in query)
        {
            ReservationsList.Add(
                new ReservationDTO
                {
                    Idreservation = reservation.Idreservation,
                    Date = reservation.Date,
                    DinersQuantity = reservation.DinersQuantity,
                    Iduser = reservation.Iduser,
                    Idtable = reservation.Idtable
                }
                );
        }

        if (ReservationsList == null)
        {
            return NotFound();
        }

        return ReservationsList;

    }
    [HttpGet("GetAllReservationsList")]
        public ActionResult<IEnumerable<ReservationDTO>> GetAllReservationsList()
        {
            var query = from reservation in _context.Reservations
                        select new
                        {
                            Idreservation = reservation.Idreservation,
                            Date = reservation.Date,
                            DinersQuantity = reservation.DinersQuantity,
                            Iduser = reservation.Iduser,
                            Idtable = reservation.Idtable
                        };

            List<ReservationDTO> ReservationsList = new List<ReservationDTO>();

            foreach (var reservation in query)
            {
                ReservationsList.Add(
                    new ReservationDTO
                    {
                        Idreservation = reservation.Idreservation,
                        Date = reservation.Date,
                        DinersQuantity = reservation.DinersQuantity,
                        Iduser = reservation.Iduser,
                        Idtable = reservation.Idtable
                    }
                    );
            }

            if (ReservationsList == null)
            {
                return NotFound();
            }

            return ReservationsList;

        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Idreservation)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Idreservation }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Idreservation == id);
        }
    }
}
