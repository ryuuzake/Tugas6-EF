using Microsoft.EntityFrameworkCore;

namespace Tugas6.Model
{
    public class ApplicationDbContext : DbContext
    {
        private static string ConnectionURL = @"Data Source=DESKTOP-L4VGE4A\SQLEXPRESS01;Initial Catalog=todos;Integrated Security=True";

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionURL);
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
    }
}
