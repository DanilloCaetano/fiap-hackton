using Domain.Doctor.Model;
using Domain.Doctor.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Doctor.Service
{
    public interface IDoctorService
    {
        Task<IList<DoctorEntity>> GetDoctorByFilter(DoctorSpecialtyEnum? doctorSpecialty, DateTime? expectedDate);
        Task AddDoctor(DoctorEntity doctorEntity);
    }
}
