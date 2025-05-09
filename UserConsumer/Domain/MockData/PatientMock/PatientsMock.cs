using Domain.Doctor.Model;
using Domain.Patient.Model;
using Domain.Schedule.Model.Enum;
using Domain.Schedule.Model;

namespace Domain.MockData.PatientMock
{
    public static class PatientsMock
    {
        public static List<PatientEntity> GetMockPatients(List<DoctorEntity> doctors)
        {
            var patient1 = new PatientEntity
            {
                CPF = "123.456.789-00",
                FirstName = "João",
                LastName = "Pereira",
                Email = "joao.pereira@example.com",
                PhoneNumber = "(11) 90000-0001",
                BirthDate = new DateTime(1990, 2, 15),
                Schedules = new List<ScheduleEntity>()
            };

            var patient2 = new PatientEntity
            {
                CPF = "987.654.321-00",
                FirstName = "Ana",
                LastName = "Souza",
                Email = "ana.souza@example.com",
                PhoneNumber = "(21) 90000-0002",
                BirthDate = new DateTime(1992, 7, 30),
                Schedules = new List<ScheduleEntity>()
            };

            // Assume que os doctors foram criados usando o mock de doctors
            var doctor1Schedule = doctors[0].SchedulesDoctor[1]; // alocado = true
            var doctor2Schedule = doctors[1].SchedulesDoctor[1]; // alocado = true

            patient1.Schedules.Add(new ScheduleEntity
            {
                PatientId = patient1.Id,
                ScheduleDoctorId = doctor1Schedule.Id,
                //DateHour = doctor1Schedule.DateHour,
                Status = ScheduleStatusEnum.Approved
            });

            patient2.Schedules.Add(new ScheduleEntity
            {
                PatientId = patient2.Id,
                ScheduleDoctorId = doctor2Schedule.Id,
                //DateHour = doctor2Schedule.DateHour,
                Status = ScheduleStatusEnum.Canceled,
                CancellationJustification = "Paciente não pôde comparecer"
            });

            return new List<PatientEntity> { patient1, patient2 };
        }
    }
}
