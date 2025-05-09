using Domain.Base.Repository;
using Domain.Patient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Patient.Repository
{
    public interface IPatientRepository : IBaseRepository<PatientEntity>
    {
    }
}
