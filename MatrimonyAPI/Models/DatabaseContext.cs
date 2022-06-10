using MatrimonyAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatrimonyAPI.Models
{
    public partial class DatabaseContext : DbContext
    {
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Contact>? Contacts{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("tbl_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
