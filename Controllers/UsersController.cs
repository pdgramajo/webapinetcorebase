using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using webapinetcorebase.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace webapinetcorebase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleMgr, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleMgr;
            _configuration = configuration;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IQueryable<ApplicationUserBasicData>>> Get()
        {

            var userList = await (from user in _userManager.Users
								  select new
								  {
									  UserId = user.Id,
									  Username = user.UserName,
									  user.Email,
									  user.PhoneNumber,
									  RoleNames = (from userRole in user.Roles //[AspNetUserRoles]
												   join role in _roleManager.Roles //[AspNetRoles]//
												   on userRole.RoleId
												   equals role.Id
												   select role.Name).ToList()
								  }).ToListAsync();

            var finalList = userList.Select( u => new ApplicationUserBasicData{
                Id = u.UserId,
                UserName = u.Username,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Roles = string.Join(",", u.RoleNames),
            });

            return Ok(finalList);

            // return Ok(_userManager.Users.Select(user => new ApplicationUserBasicData
            // {
            //     Id = user.Id,
            //     UserName = user.UserName,
            //     Email = user.Email,
            //     PhoneNumber = user.PhoneNumber
            // }).ToList());



        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserBasicData>> Get(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

           // var id2 = user.Select(d => d.Id);

            // ApplicationUserBasicData s = user.Select(item => new ApplicationUserBasicData{
            //     Id = item.Id,
            //     UserName = item.UserName,
            //     Email = item.Email,
            //     PhoneNumber = item.PhoneNumber
            // });
            return Ok(user);

        }
    }
}