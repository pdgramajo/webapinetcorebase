using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
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
            var users = await _userManager.Users.Select(user => new ApplicationUserBasicData
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = string.Join(",", user.Roles.Join(_roleManager.Roles,
                                            userRole => userRole.RoleId,
                                            role => role.Id,
                                            (userRole, role) => role.Name
                                            ).ToList())
            }).ToListAsync();

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserBasicData>> Get(string id)
        {
            var userExist = _userManager.Users.Any(x => x.Id == id);
            if (!userExist)
            {
                return NotFound();
            }

            var userData = await _userManager.Users.Where(user => user.Id == id).Select(user => new ApplicationUserBasicData
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = string.Join(',', user.Roles.Join(_roleManager.Roles,
                                            userRole => userRole.RoleId,
                                            role => role.Id,
                                            (userRole, role) => role.Name
                                            ).ToList())
            }).FirstOrDefaultAsync();

            return Ok(userData);
        }

        [HttpPost("{id}/Role")]
        public async Task<ActionResult> AddRole(string id, [FromBody] RoleId role)
        {

            if (string.IsNullOrEmpty(role.Id))
            {
                return BadRequest("the roleId is required");
            }
            try
            {
                var rol = await _roleManager.FindByIdAsync(role.Id);
                var user = await _userManager.FindByIdAsync(id);
                var result = await _userManager.AddToRoleAsync(user, rol.Name);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

    }
}