using DevExpress.Xpo;
using MasJWT.Classes;  // XPO-Entity
using MasJWT.DTOs;      // DTO-Klassen
using MasJWT;           // Klasse UserLogin
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace messenger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _configuration;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;

            // Datenbankverbindung konfigurieren
            //const string connectionString = @"Data Source=localhost\SQLEXPRESSMAS;Database=MasMessenger;Integrated Security=True;";
            const string connectionString = @"Data Source=localhost\SQLEXPRESSMAS; Initial Catalog=MasMessengerJWT; User Id=sa; Password=abcd1234!";
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
        }

        // GET: /Users
        [HttpGet(Name = "GetUsers")]
        public IEnumerable<UserDto> Get()
        {
            var usersDto = new List<UserDto>();
            using (var uow = new UnitOfWork())
            {
                var users = uow.Query<MasJWT.Classes.User>().ToList();
                foreach (var user in users)
                {
                    var dto = new UserDto
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
                    usersDto.Add(dto);
                }
            }
            _logger.LogInformation("Alle Benutzer werden abgerufen.");
            return usersDto;
        }

        // GET: /Users/{oid}
        [HttpGet("{oid}", Name = "GetUserById")]
        public ActionResult<UserDto> GetById(int oid)
        {
            using (var uow = new UnitOfWork())
            {
                var user = uow.GetObjectByKey<MasJWT.Classes.User>(oid);
                if (user == null)
                {
                    return NotFound();
                }
                var dto = new UserDto
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
                return Ok(dto);
            }
        }

        // POST: /Users
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto createUser)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(createUser.Name) || string.IsNullOrEmpty(createUser.Email))
            {
                return BadRequest("Name und Email sind Pflichtfelder.");
            }

            using (var uow = new UnitOfWork())
            {
                try
                {
                    var userEntity = new MasJWT.Classes.User(uow)
                    {
                        Name = createUser.Name,
                        Surname = createUser.Surname,
                        Email = createUser.Email,
                        Image = createUser.Image,
                        Birthday = createUser.Birthday,
                        Job = createUser.Job,
                        Nationality = createUser.Nationality,
                        Gender = createUser.Gender,
                        Role = createUser.Role
                    };

                    if (!string.IsNullOrEmpty(createUser.Password))
                    {
                        using (var hmac = new HMACSHA512())
                        {
                            userEntity.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(createUser.Password));
                            userEntity.PasswordSalt = hmac.Key;
                        }
                    }
                    uow.CommitChanges();

                    var userDto = new UserDto
                    {
                        Oid = userEntity.Oid,
                        Name = userEntity.Name,
                        Surname = userEntity.Surname,
                        Email = userEntity.Email,
                        Image = userEntity.Image,
                        Birthday = userEntity.Birthday,
                        Job = userEntity.Job,
                        Nationality = userEntity.Nationality,
                        Gender = userEntity.Gender,
                        Role = userEntity.Role
                    };

                    return CreatedAtRoute("GetUserById", new { oid = userEntity.Oid }, userDto);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fehler beim Erstellen des Benutzers.");
                    return StatusCode(500, "Es ist ein Fehler aufgetreten.");
                }
            }
        }

        // PUT: /Users/{oid}
        [HttpPut("{oid}", Name = "UpdateUser")]
        public IActionResult Update(int oid, [FromBody] CreateUserDto updateUser)
        {
            using (var uow = new UnitOfWork())
            {
                var userEntity = uow.GetObjectByKey<MasJWT.Classes.User>(oid);
                if (userEntity == null)
                {
                    return NotFound();
                }

                if (!string.IsNullOrEmpty(updateUser.Name)) userEntity.Name = updateUser.Name;
                if (!string.IsNullOrEmpty(updateUser.Surname)) userEntity.Surname = updateUser.Surname;
                if (!string.IsNullOrEmpty(updateUser.Email)) userEntity.Email = updateUser.Email;
                if (!string.IsNullOrEmpty(updateUser.Job)) userEntity.Job = updateUser.Job;
                if (!string.IsNullOrEmpty(updateUser.Gender)) userEntity.Gender = updateUser.Gender;
                if (!string.IsNullOrEmpty(updateUser.Nationality)) userEntity.Nationality = updateUser.Nationality;
                if (!string.IsNullOrEmpty(updateUser.Role)) userEntity.Role = updateUser.Role;
                if (updateUser.Birthday.HasValue) userEntity.Birthday = updateUser.Birthday.Value;
                if (updateUser.Image != null) userEntity.Image = updateUser.Image;

                if (!string.IsNullOrEmpty(updateUser.Password))
                {
                    using (var hmac = new HMACSHA512())
                    {
                        userEntity.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(updateUser.Password));
                        userEntity.PasswordSalt = hmac.Key;
                    }
                }
                uow.CommitChanges();
                _logger.LogInformation($"Benutzer mit Oid {oid} wurde aktualisiert.");
                return NoContent();
            }
        }

        // DELETE: /Users/{oid}
        [HttpDelete("{oid}", Name = "DeleteUser")]
        public IActionResult Delete(int oid)
        {
            using (var uow = new UnitOfWork())
            {
                var userEntity = uow.GetObjectByKey<MasJWT.Classes.User>(oid);
                if (userEntity == null)
                {
                    return NotFound();
                }
                userEntity.Delete();
                uow.CommitChanges();
                _logger.LogInformation($"Benutzer mit Oid {oid} wurde gelöscht.");
                return NoContent();
            }
        }

        // POST: /Users/login
        [HttpPost("login", Name = "Login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] MasJWT.UserLogin login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Email und Passwort sind Pflichtfelder.");
            }

            using (var uow = new UnitOfWork())
            {
                var user = await uow.Query<MasJWT.Classes.User>()
                                    .FirstOrDefaultAsync(u => u.Email == login.Email);

                if (user == null)
                {
                    _logger.LogWarning($"Fehlgeschlagener Login-Versuch für Email: {login.Email}");
                    return Unauthorized("Ungültige Anmeldedaten.");
                }

                using (var hmac = new HMACSHA512(user.PasswordSalt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
                    if (!CryptographicOperations.FixedTimeEquals(computedHash, user.PasswordHash))
                    {
                        _logger.LogWarning($"Fehlgeschlagener Login-Versuch für Email: {login.Email}");
                        return Unauthorized("Ungültige Anmeldedaten.");
                    }
                }

                // JWT-Token generieren
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Oid.ToString()),
                        new Claim(ClaimTypes.Name, user.Email)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var response = new LoginResponseDto
                {
                    Oid = user.Oid,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Role = user.Role,
                    Token = tokenString
                };

                _logger.LogInformation($"Benutzer {user.Email} hat sich erfolgreich angemeldet.");
                return Ok(response);
            }
        }
    }
}
