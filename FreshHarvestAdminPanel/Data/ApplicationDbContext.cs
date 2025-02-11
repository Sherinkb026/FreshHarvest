using FreshHarvestAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace FreshHarvestAdminPanel.Data
    
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
        public DbSet<CategoryModel> categories { get; set; }
    }
}
