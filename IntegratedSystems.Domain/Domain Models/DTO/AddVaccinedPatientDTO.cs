using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models.DTO
{
    public class AddVaccinedPatientDTO
    {
        public Guid PatientId { get; set; }
        public Guid CenterId { get; set; }
        public string Manufacturer { get; set; }
        public DateTime DateTaken { get; set; }
        public List<Patient> Patients { get; set; }

        /*public Patient? Patient { get; set; }*/
        /*public VaccinationCenter VaccinationCenter { get; set; }*/


    }
}
