using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
    public class UsersController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public Tools.Crypto MyCrypto { get; set; }

        public UsersController(RestaurantContext context)
        {
            _context = context;

            MyCrypto = new Tools.Crypto();
        }

        // GET: api/Users/ValidateUserCredentials?email=1%40gmail.com&password=1234
        [HttpGet("ValidateUserCredentials")]
        public async Task<ActionResult<User>> ValidateUserCredentials(string email, string password)
        {
            string ApiLevelEncriptedPassword = MyCrypto.EncriptarEnUnSentido(password);



            var user = await _context.Users.SingleOrDefaultAsync(e => e.Email == email && e.UserPassword == ApiLevelEncriptedPassword);



            if (user == null)
            {
                return NotFound();
            }



            return user;
        }

        // GET: api/Users/GetEmployeeData?idUser=1
        [HttpGet("GetEmployeeData")]
        public ActionResult<IEnumerable<UserDTO>> GetUserInfo(int idUser)
        {
            //las consultas linq se parecen mucho a las normales que hemos hecho en T-SQL
            //una de las diferencias es que podemos usar una "tabla temporal" para almacenar
            //los resultados y luego usarla para llenar los atributos de un modelo o DTO

            var query = (from user in _context.Users
                         join userRole in _context.UserRoles on user.IduserRole equals userRole.IduserRole
                         join country in _context.Countries on user.Idcountry equals country.Idcountry
                         where user.Iduser == idUser
                         select new
                         {
                             Iduser = user.Iduser,
                             Name = user.Name,
                             Email = user.Email,
                             UserPassword = user.UserPassword,
                             BackUpEmail = user.BackUpEmail,
                             PhoneNumber = user.PhoneNumber,
                             Active = user.Active,
                             IduserRole = userRole.IduserRole,
                             Idcountry = country.Idcountry
                         }).ToList();

            List<UserDTO> list = new List<UserDTO>();

            foreach (var user in query)
            {
                UserDTO NewItem = new UserDTO();

                NewItem.Iduser = user.Iduser;
                NewItem.Name = user.Name;
                NewItem.Email = user.Email;
                NewItem.UserPassword = user.UserPassword;
                NewItem.BackUpEmail = user.BackUpEmail;
                NewItem.PhoneNumber = user.PhoneNumber;
                NewItem.Active = user.Active;
                NewItem.IduserRole = user.IduserRole;
                NewItem.Idcountry = user.Idcountry;

                list.Add(NewItem);

            }

            if (list == null)
            {
                return NotFound();
            }

            return list;

        }

        [HttpGet("GetEmployeeList")]
        public ActionResult<IEnumerable<UserDTO>> GetEmployeeList()
        {
            var query = from user in _context.Users
                        where user.IduserRole == 3
                        select new
                        {
                            Iduser = user.Iduser,
                            Name = user.Name,
                            Email = user.Email,
                            UserPassword = user.UserPassword,
                            BackUpEmail = user.BackUpEmail,
                            PhoneNumber = user.PhoneNumber,
                            Active = user.Active,
                            IduserRole = user.IduserRole,
                            Idcountry = user.Idcountry
                        };

            List<UserDTO> EmployeesList = new List<UserDTO>();

            foreach (var user in query)
            {
                EmployeesList.Add(
                    new UserDTO
                    {
                        Iduser = user.Iduser,
                        Name = user.Name,
                        Email = user.Email,
                        UserPassword = user.UserPassword,
                        BackUpEmail = user.BackUpEmail,
                        PhoneNumber = user.PhoneNumber,
                        Active = user.Active,
                        IduserRole = user.IduserRole,
                        Idcountry = user.Idcountry
                    }
                    );
            }

            if (EmployeesList == null)
            {
                return NotFound();
            }

            return EmployeesList;

        }

        // GET: api/Users/GetUserInfo?email=a@gmail.com
        [HttpGet("GetUserInfo")]
        public ActionResult<IEnumerable<UserDTO>> GetUserInfo(string email)
        {
            //las consultas linq se parece a los normales.
            var query = (from user in _context.Users
                         where user.Email == email
                         select new
                         {
                             idUser = user.Iduser,
                             name = user.Name,
                             email = user.Email,
                             userPassword = user.UserPassword,
                             backupEmail = user.BackUpEmail,
                             phoneNumber = user.PhoneNumber,
                             active = user.Active,
                             idUserRole = user.IduserRole,
                             idCountry = user.Idcountry

                         }).ToList();
            List<UserDTO> list = new List<UserDTO>();



            foreach (var item in query)
            {
                UserDTO newItem = new UserDTO();

                newItem.Iduser = item.idUser;
                newItem.Name = item.name;
                newItem.Email = item.email;
                newItem.UserPassword = item.userPassword;
                newItem.BackUpEmail = item.backupEmail;
                newItem.PhoneNumber = item.phoneNumber;
                newItem.Active = item.active;
                newItem.IduserRole = item.idUserRole;
                newItem.Idcountry = item.idCountry;

                list.Add(newItem);
            }






            if (list == null)
            {
                return NotFound();
            }



            return list;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Iduser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            string ApiLevelEncriptedPass = MyCrypto.EncriptarEnUnSentido(user.UserPassword);

            user.UserPassword = ApiLevelEncriptedPass;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Iduser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Iduser == id);
        }
    }
}
