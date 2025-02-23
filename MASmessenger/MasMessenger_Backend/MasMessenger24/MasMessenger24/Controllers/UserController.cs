using DevExpress.Xpo;
using MasMessenger24.Classes;  // XPO-Datenbankklasse
using MasMessenger24;          // Normale User-Klasse
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace messenger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;

            // Datenbankverbindung konfigurieren
            //const string connectionString = @"Data Source=localhost\SQLEXPRESSMAS;Database=MasMessenger;Integrated Security=True;";
            const string connectionString = @"Data Source=localhost\SQLEXPRESSMAS; Initial Catalog=MasMessenger; User Id=sa; Password=abcd1234!";
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
        }

        // GET: /Users
        [HttpGet(Name = "GetUsers")]
        public IEnumerable<MasMessenger24.User> Get()
        {
            var usersmodel = new List<MasMessenger24.User>();
            using (var uow = new UnitOfWork())
            {
                // XPO-Abfrage auf die XPO-User-Klasse
                var users = uow.Query<MasMessenger24.Classes.User>().ToList();

                foreach (var user in users)
                {
                    var usermodel = new MasMessenger24.User()
                    {
                        Oid = user.Oid,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        Image = user.Image,
                        Birthday = user.Birthday,
                        Job = user.Job,
                        Nationality = user.Nationality,
                        Gender = user.Gender,
                        Role = user.Role
                    };
                    usersmodel.Add(usermodel);
                }
            }

            _logger.LogInformation("Retrieving all users.");
            return usersmodel;
        }

        // POST: /Users
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<MasMessenger24.User>> Create(MasMessenger24.User user)
        {
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Name und Email sind Pflichtfelder.");
            }

            using (var uow = new UnitOfWork())
            {
                try
                {
                    var userEntity = new MasMessenger24.Classes.User(uow)
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        Image = user.Image,
                        Birthday = user.Birthday,
                        Job = user.Job,
                        Nationality = user.Nationality,
                        Gender = user.Gender,
                        Role = user.Role
                    };

                    // Passwort-Hash generieren, falls vorhanden
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using (var hmac = new HMACSHA512())
                        {
                            userEntity.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                            userEntity.PasswordSalt = hmac.Key; // Salt speichern
                        }
                    }

                    uow.CommitChanges();
                    return CreatedAtRoute("GetUsers", new { id = userEntity.Oid }, user);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fehler beim Erstellen des Benutzers.");
                    return StatusCode(500, "Ein Fehler ist aufgetreten.");
                }
            }
        }

        // PUT: /Users/{oid}
        [HttpPut("{oid}", Name = "UpdateUser")]
        public IActionResult Update(int oid, [FromBody] MasMessenger24.User user)
        {
            using (var uow = new UnitOfWork())
            {
                var userEntity = uow.GetObjectByKey<MasMessenger24.Classes.User>(oid);
                if (userEntity == null)
                {
                    return NotFound();
                }

                // Felder aktualisieren
                if (!string.IsNullOrEmpty(user.Name)) userEntity.Name = user.Name;
                if (!string.IsNullOrEmpty(user.Surname)) userEntity.Surname = user.Surname;
                if (!string.IsNullOrEmpty(user.Email)) userEntity.Email = user.Email;
                if (!string.IsNullOrEmpty(user.Job)) userEntity.Job = user.Job;
                if (!string.IsNullOrEmpty(user.Gender)) userEntity.Gender = user.Gender;
                if (!string.IsNullOrEmpty(user.Nationality)) userEntity.Nationality = user.Nationality;
                if (!string.IsNullOrEmpty(user.Role)) userEntity.Role = user.Role;
                if (user.Birthday.HasValue) userEntity.Birthday = user.Birthday.Value;
                if (user.Image != null) userEntity.Image = user.Image;

                // Passwort aktualisieren
                if (!string.IsNullOrEmpty(user.Password))
                {
                    using (var hmac = new HMACSHA512())
                    {
                        userEntity.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                        userEntity.PasswordSalt = hmac.Key;
                    }
                }

                uow.CommitChanges();
                _logger.LogInformation($"User with Oid {oid} updated.");
                return NoContent();
            }
        }

        // DELETE: /Users/{oid}
        [HttpDelete("{oid}", Name = "DeleteUser")]
        public IActionResult Delete(int oid)
        {
            using (var uow = new UnitOfWork())
            {
                var userEntity = uow.GetObjectByKey<MasMessenger24.Classes.User>(oid);
                if (userEntity == null)
                {
                    return NotFound();
                }

                userEntity.Delete();
                uow.CommitChanges();
                _logger.LogInformation($"User with ID {oid} deleted.");
                return NoContent();
            }
        }
        [HttpPost("login", Name = "Login")]
        public async Task<ActionResult<MasMessenger24.User>> Login([FromBody] UserLogin login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Email and password are required.");
            }

            using (var uow = new UnitOfWork())
            {
                var user = await uow.Query<MasMessenger24.Classes.User>()
                                    .FirstOrDefaultAsync(u => u.Email == login.Email);

                if (user == null)
                {
                    _logger.LogWarning($"Failed login attempt for email: {login.Email}");
                    return Unauthorized("Invalid credentials.");
                }

                using (var hmac = new HMACSHA512(user.PasswordSalt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

                    if (!CryptographicOperations.FixedTimeEquals(computedHash, user.PasswordHash))
                    {
                        _logger.LogWarning($"Failed login attempt for email: {login.Email}");
                        return Unauthorized("Invalid credentials.");
                    }
                }

                _logger.LogInformation($"User {user.Email} successfully logged in.");
                return Ok(new
                {
                    Oid = user.Oid,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Image = user.Image,
                    Birthday = user.Birthday,
                    Job = user.Job,
                    Gender = user.Gender,
                    Nationality = user.Nationality,
                    Role = user.Role,
                });
            }
        }

    }

}
