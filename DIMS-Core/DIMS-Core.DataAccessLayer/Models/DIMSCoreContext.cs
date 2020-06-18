using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.DataAccessLayer.Models
{
    public partial class DIMSCoreContext : DbContext
    {
        public DIMSCoreContext()
        {
        }

        public DIMSCoreContext(DbContextOptions<DIMSCoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Samples> Samples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samples>(entity =>
            {
                entity.HasKey(e => e.SampleId);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}