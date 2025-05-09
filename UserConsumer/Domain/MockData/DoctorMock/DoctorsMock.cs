using Domain.Doctor.Model.Enum;
using Domain.Doctor.Model;

namespace Domain.MockData.DoctorMock
{
    public static class DoctorsMock
    {
        public static List<DoctorEntity> GetMockDoctors()
        {
            var doctor1 = new DoctorEntity
            {
                CRM = "123456-SP",
                FirstName = "Carlos",
                LastName = "Silva",
                Email = "carlos.silva@example.com",
                PhoneNumber = "(11) 91234-5678",
                BirthDate = new DateTime(1980, 5, 12),
                Specialty = DoctorSpecialtyEnum.Cardiologist,
                SchedulesDoctor = new List<ScheduleDoctorEntity>()
            };

            var doctor2 = new DoctorEntity
            {
                CRM = "654321-RJ",
                FirstName = "Mariana",
                LastName = "Oliveira",
                Email = "mariana.oliveira@example.com",
                PhoneNumber = "(21) 99876-5432",
                BirthDate = new DateTime(1985, 10, 23),
                Specialty = DoctorSpecialtyEnum.Dermatologist,
                SchedulesDoctor = new List<ScheduleDoctorEntity>()
            };

            // Agendas para o Dr. Carlos
            doctor1.SchedulesDoctor.Add(new ScheduleDoctorEntity
            {
                DateHour = DateTime.Today.AddHours(9),
                IsAllocated = false,
                DoctorId = doctor1.Id
            });
            doctor1.SchedulesDoctor.Add(new ScheduleDoctorEntity
            {
                DateHour = DateTime.Today.AddHours(10),
                IsAllocated = true,
                DoctorId = doctor1.Id
            });

            // Agendas para a Dra. Mariana
            doctor2.SchedulesDoctor.Add(new ScheduleDoctorEntity
            {
                DateHour = DateTime.Today.AddDays(1).AddHours(14),
                IsAllocated = false,
                DoctorId = doctor2.Id
            });
            doctor2.SchedulesDoctor.Add(new ScheduleDoctorEntity
            {
                DateHour = DateTime.Today.AddDays(1).AddHours(15),
                IsAllocated = true,
                DoctorId = doctor2.Id
            });

            return new List<DoctorEntity> { doctor1, doctor2 };
        }
    }
}
