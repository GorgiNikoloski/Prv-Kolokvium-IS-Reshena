using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;
using IntegratedSystems.Domain.Domain_Models.DTO;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace IntegratedSystems.Web.Controllers
{
    public class VaccinationCentersController : Controller
    {
        
        private readonly IVaccinationCenterService _vaccinationCenterService;
        private readonly IPatientService _patientService;

        public VaccinationCentersController(IVaccinationCenterService vaccinationCenterService, IPatientService patientService)
        {
            _vaccinationCenterService = vaccinationCenterService;
            _patientService = patientService;
        }



        // za dodavanje na vakcina
        public IActionResult AddVaccinedPatient(Guid id)
        {
            AddVaccinedPatientDTO tmp = new AddVaccinedPatientDTO
            {
                CenterId = id,
                Patients = _patientService.GetPatients().ToList()
            };
            return View(tmp);
        }

        [HttpPost]
        public IActionResult AddVaccinedPatientConfirm(AddVaccinedPatientDTO model)
        {
            Vaccine vaccine = new Vaccine
            {
                Id = Guid.NewGuid(),
                PatientId = model.PatientId,
                Manufacturer = model.Manufacturer,
                DateTaken = model.DateTaken,
                VaccinationCenter = model.CenterId,
                Certificate = Guid.NewGuid()
            };

            if(_vaccinationCenterService.AddVaccine(vaccine) == null)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));

        }



        // GET: VaccinationCenters
        public IActionResult Index()
        {
            return View(_vaccinationCenterService.GetVaccinationCenters());
        }

        // GET: VaccinationCenters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenterById(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }
            ViewBag.CenterID = id;
            return View(vaccinationCenter.Vaccines.ToList());       // ova morat da go smena toList I vo repository morat da se menit(specificiarno e)
        }

        // GET: VaccinationCenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccinationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (ModelState.IsValid)
            {
                _vaccinationCenterService.CreateNewVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenterById(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }
            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (id != vaccinationCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _vaccinationCenterService.UpdateVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenterById(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _vaccinationCenterService.DeleteVaccinationCenter(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationCenterExists(Guid id)
        {
            return _vaccinationCenterService.GetVaccinationCenterById(id) != null;
        }
    }
}
