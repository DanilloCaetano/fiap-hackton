using Domain.Doctor.Model;
using Domain.MockData.DoctorMock;
using Domain.MockData.PatientMock;
using Domain.Patient.Model;
using Domain.Schedule.Model;
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

        public virtual DbSet<PatientEntity> Patients { get; set; }
        public virtual DbSet<DoctorEntity> Doctors { get; set; }
        public virtual DbSet<ScheduleDoctorEntity> SchedulesDoctor { get; set; }
        public virtual DbSet<ScheduleEntity> Schedules { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientEntity>()
            .HasMany(c => c.Schedules)
            .WithOne(r => r.Patient)
            .HasForeignKey(c => c.PatientId);

            modelBuilder.Entity<DoctorEntity>()
            .HasMany(c => c.SchedulesDoctor)
            .WithOne(r => r.Doctor)
            .HasForeignKey(c => c.DoctorId);

            modelBuilder.Entity<ScheduleDoctorEntity>();

            modelBuilder.Entity<ScheduleEntity>()
            .HasOne(c => c.ScheduleDoctor)
            .WithOne(c => c.Schedule);

            modelBuilder.Entity<ScheduleEntity>()
            .HasOne(c => c.Patient)
            .WithMany(c => c.Schedules)
            .HasForeignKey(c => c.PatientId);

            //var mockDoctors = DoctorsMock.GetMockDoctors();
            //modelBuilder.Entity<DoctorEntity>().HasData(mockDoctors);
            //modelBuilder.Entity<PatientEntity>().HasData(PatientsMock.GetMockPatients(mockDoctors));
        }
    }
}
