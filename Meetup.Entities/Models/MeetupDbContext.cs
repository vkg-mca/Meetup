using Microsoft.EntityFrameworkCore;

namespace Meetup.Entities.Models
{
    public partial class MeetupDbContext : DbContext
    {
        public MeetupDbContext()
        {
        }

        public MeetupDbContext(DbContextOptions<MeetupDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Meetup> Meetup { get; set; }
        public virtual DbSet<MeetupDetail> MeetupDetail { get; set; }
        public virtual DbSet<MeetupFeedback> MeetupFeedback { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Data Source=PARAM\\SQLEXPRESS;Initial Catalog=MeetupDb;Integrated Security=SSPI;TrustServerCertificate=True;Trusted_Connection=True");
	     optionsBuilder.UseSqlServer("Data Source=PARAM\\SQLEXPRESS;Initial Catalog=MeetupDb;Integrated Security=SSPI;TrustServerCertificate=True;");
	 }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meetup>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Topic)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MeetupDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Topic)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<MeetupFeedback>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
