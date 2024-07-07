using Microsoft.EntityFrameworkCore;
using WebApi.Models.Domain;
namespace WebApi.Data
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions options) : base(options)
        {
          
        }
          public DbSet<Contact> Contacts { get; set; }
    }
}