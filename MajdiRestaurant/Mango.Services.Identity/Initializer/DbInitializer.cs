using IdentityModel;
using Mango.Services.Identity.DbContext;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Services.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userMaager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager  )
        {
            _db = db;
            _userMaager = userManager;  
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if(_roleManager.FindByNameAsync(StaticDetails.Admin).Result==null)
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Customer)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "Master",
                Email = "MrMajdiAtrous@atrous.com",
                EmailConfirmed = true,
                PhoneNumber = "0611111111",
                FirstName ="Majdi",
                LastName="Atrous"
            };
            _userMaager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userMaager.AddToRoleAsync(adminUser, StaticDetails.Admin).GetAwaiter().GetResult();
            var result = _userMaager.AddClaimsAsync(adminUser, new Claim[] {
            new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " +adminUser.LastName),
            new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
            new Claim(JwtClaimTypes.Role, StaticDetails.Admin)
            }).Result;

            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "Customer1",
                Email = "Customer1@atrous.com",
                EmailConfirmed = true,
                PhoneNumber = "0611111112",
                FirstName = "Customer",
                LastName = "One"
            };
            _userMaager.CreateAsync(customerUser, "Admin123*").GetAwaiter().GetResult();
            _userMaager.AddToRoleAsync(customerUser, StaticDetails.Customer).GetAwaiter().GetResult();
            var resultCustomer = _userMaager.AddClaimsAsync(customerUser, new Claim[] {
            new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " +customerUser.LastName),
            new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
            new Claim(JwtClaimTypes.Role, StaticDetails.Customer)
            }).Result;




        }
    }
}
