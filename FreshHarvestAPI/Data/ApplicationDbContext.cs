using FreshHarvestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FreshHarvestAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<ProductModel> Products { get; set; }
    }
}
