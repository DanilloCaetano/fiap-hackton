using Domain.User.Model;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Context
{
    public class TechChallengeContext : DbContext
    {
        public TechChallengeContext(DbContextOptions options) : base(options)
        {
        }

        public TechChallengeContext()
        {
        }
        public virtual DbSet<UserEntity> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var mockDoctors = DoctorsMock.GetMockDoctors();
            //modelBuilder.Entity<DoctorEntity>().HasData(mockDoctors);
            //modelBuilder.Entity<PatientEntity>().HasData(PatientsMock.GetMockPatients(mockDoctors));
        }
    }
}
