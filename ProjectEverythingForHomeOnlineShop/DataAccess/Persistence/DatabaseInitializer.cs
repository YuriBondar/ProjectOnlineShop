using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence.Identity;

namespace ProjectEverythingForHomeOnlineShop.DataAccess.Persistence
{
    public class DatabaseInitializer
    {

        private readonly OnlineShopMySQLDatabaseContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(
            OnlineShopMySQLDatabaseContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<DatabaseInitializer> logger)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// initialaze database and applay nee migrations by starting app
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                /// applay migrations
                await _dbContext.Database.MigrateAsync();

                /// create new roles if they do not exist
                
                await EnsureRolesAsync();

                /// create first mainAdmin login with 'admin' role if it dos not already exist
                
                await EnsureAdminUserAsync();

                _logger.LogInformation("Database initialization is succesful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during database initialization.");
            }
        }

        /// <summary>
        /// create new roles from <array roles> if they do not already exist
        /// </summary>

        private async Task EnsureRolesAsync()
        {
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                    _logger.LogInformation($"Role {role} was created.");
                }
            }
        }

        /// <summary>
        /// create first mainAdmin login with 'admin' role if it dos not already exist
        /// </summary>

        private async Task EnsureAdminUserAsync()
        {
            string adminEmail = "mainAdmin@example.com";
            string adminPassword = "Admin123!";

            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = "MainAdmin",
                    Email = adminEmail
                };

                var result = await _userManager.CreateAsync(newAdmin, adminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newAdmin, "Admin");
                    _logger.LogInformation("Admistrator is created successful.");
                }
                else
                {
                    _logger.LogError("Admistrator is not created: {Errors}", result.Errors);
                }
            }
        }

    }
}
