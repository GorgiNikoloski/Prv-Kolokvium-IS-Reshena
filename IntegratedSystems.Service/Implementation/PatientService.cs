using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;


namespace IntegratedSystems.Service.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;

        public PatientService(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }



        public Patient CreateNewPatient(Patient patient)
        {
            return _patientRepository.Insert(patient);
        }

        public Patient DeletePatient(Guid? id)
        {
            var patient_to_delete = _patientRepository.Get(id);
            return _patientRepository.Delete(patient_to_delete);
        }

        public Patient GetPatientById(Guid? id)
        {
            return _patientRepository.Get(id);
        }

        public List<Patient> GetPatients()
        {
            return _patientRepository.GetAll().ToList();
        }

        public Patient UpdatePatient(Patient patient)
        {
            return _patientRepository.Update(patient);
        }
    }
}
