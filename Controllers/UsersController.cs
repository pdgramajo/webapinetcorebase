using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using webapinetcorebase.Models;

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
        public ActionResult<IQueryable<ApplicationUserBasicData>> Get()
        {
            return Ok(_userManager.Users.Select(item => new ApplicationUserBasicData{
                Id = item.Id,
                UserName = item.UserName,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber
            }).ToList());
        }
    }
}