using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccinationCenterService : IVaccinationCenterService
    {
        private readonly IRepository<VaccinationCenter> _vaccinationCenterRepository;
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Vaccine> _vaccineRepository;

        public VaccinationCenterService(IRepository<VaccinationCenter> vaccinationCenterRepository, IRepository<Patient> patientRepository, IRepository<Vaccine> vaccineRepository)
        {
            _vaccinationCenterRepository = vaccinationCenterRepository;
            _patientRepository = patientRepository;
            _vaccineRepository = vaccineRepository;
        }

        public Vaccine AddVaccine(Vaccine vaccine)
        {
            var center = _vaccinationCenterRepository.Get(vaccine.VaccinationCenter);
            if(center.Vaccines.Count == center.MaxCapacity)
            {
                return null;
            }
            var patient = _patientRepository.Get(vaccine.PatientId);
            vaccine.Center = center;
            vaccine.PatientFor = patient;
            _vaccineRepository.Insert(vaccine);
            patient.VaccinationSchedule.Add(vaccine);
            center.Vaccines.Add(vaccine);
            _vaccinationCenterRepository.Update(center);
            _patientRepository.Update(patient);
            return vaccine;
        }

        public VaccinationCenter CreateNewVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            return _vaccinationCenterRepository.Insert(vaccinationCenter);
        }

        public VaccinationCenter DeleteVaccinationCenter(Guid id)
        {
            var center_to_delete = _vaccinationCenterRepository.Get(id);
            return _vaccinationCenterRepository.Delete(center_to_delete);
        }

        public VaccinationCenter GetVaccinationCenterById(Guid? id)
        {
            return _vaccinationCenterRepository.Get(id);
        }

        public List<VaccinationCenter> GetVaccinationCenters()
        {
            return _vaccinationCenterRepository.GetAll().ToList();
        }

        public VaccinationCenter UpdateVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            return _vaccinationCenterRepository.Update(vaccinationCenter);
        }
    }
}
