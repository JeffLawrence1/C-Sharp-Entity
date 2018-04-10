using Microsoft.EntityFrameworkCore;

namespace userDashboard_project.Models
{
    public class YourContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}