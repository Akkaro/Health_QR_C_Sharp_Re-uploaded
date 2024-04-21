using Health_QR.DAL;
using Health_QR.Models;
using IronBarCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Diagnostics;
using System.Security.Policy;

namespace Health_QR.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {

        private PatientDAL _dalPatient;
        private RolesDAL _dalRoles;
        private Users _dalUser;

        public PatientController(ILogger<HomeController> logger)
        {
            _dalPatient = new PatientDAL();
            _dalRoles = new RolesDAL();
            _dalUser = new Users();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {

            patient.AddedBy = _dalUser.GetUserIdByName(User.Identity.Name).Id;
            var uniq = int.Parse(DateTime.Now.ToString("yymmssfff"));
            patient.UniquePatientId = uniq;
            patient.Personal_Information.UniquePatientId = uniq;
            patient.Personal_Information.Contact_Information.UniquePatientId = uniq;
            patient.Active_Problems_Diagnoses.UniquePatientId = uniq;
            patient.Active_Problems_Diagnoses.Physical_Examination.UniquePatientId = uniq;
            patient.Active_Problems_Diagnoses.Vital_Signs.UniquePatientId = uniq;

            patient.QR_File = AddQRCode(patient.UniquePatientId);
            _dalPatient.AddPatient(patient);
            _dalPatient.AddPatient_Personal_Information(patient.Personal_Information);
            _dalPatient.AddPatient_Contact_Information(patient.Personal_Information.Contact_Information);
            _dalPatient.AddPatient_Active_Problems_Diagnoses(patient.Active_Problems_Diagnoses);
            _dalPatient.AddPatient_Physical_Examination(patient.Active_Problems_Diagnoses.Physical_Examination);
            _dalPatient.AddPatient_Vital_Signs(patient.Active_Problems_Diagnoses.Vital_Signs);
            
            return RedirectToAction("Create");
        }

        public IActionResult List()
        {
            var patientList = _dalPatient.ListPatients();
            foreach (var patient in patientList)
            {
                patient.Personal_Information = _dalPatient.GetPatient_Personal_Information(patient.UniquePatientId);
                patient.Personal_Information.Contact_Information = _dalPatient.GetPatient_Contact_Information(patient.UniquePatientId);
                patient.Active_Problems_Diagnoses = _dalPatient.GetPatient_Active_Problems_Diagnoses(patient.UniquePatientId);
                patient.Active_Problems_Diagnoses.Physical_Examination = _dalPatient.GetPatient_Physical_Examination(patient.UniquePatientId);
                patient.Active_Problems_Diagnoses.Vital_Signs = _dalPatient.GetPatient_Vital_Signs(patient.UniquePatientId);
            }
            return View(patientList);
        }
        public IActionResult SearchByAddedBy()
        {
              
            var patientList = _dalPatient.Search(_dalUser.GetUserIdByName(User.Identity.Name).Id);
            foreach (var patient in patientList)
            {
                patient.Personal_Information = _dalPatient.GetPatient_Personal_Information(patient.UniquePatientId);
                patient.Personal_Information.Contact_Information = _dalPatient.GetPatient_Contact_Information(patient.UniquePatientId);
                patient.Active_Problems_Diagnoses = _dalPatient.GetPatient_Active_Problems_Diagnoses(patient.UniquePatientId);
                patient.Active_Problems_Diagnoses.Physical_Examination = _dalPatient.GetPatient_Physical_Examination(patient.UniquePatientId);
                patient.Active_Problems_Diagnoses.Vital_Signs = _dalPatient.GetPatient_Vital_Signs(patient.UniquePatientId);
            }
            return View(patientList);
        }

        public IActionResult Details(int id)
        {
            Patient patient = new Patient();
            if (id > 0)
            {
                patient = _dalPatient.GetPatient(id);
            }
            return View("Details", patient);
        }
        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var ret =  _dalPatient.GetPatient(id);
            return View(ret);
            }

        [HttpPost]
        [Route("/Patient/PostDelete/{id}")]
        public IActionResult PostDelete(string id)
        {
            _dalPatient.DeleteById(int.Parse(id));
            return RedirectToAction("List");
        }
        public string AddQRCode(int id)
        {
            string idstring = id.ToString();
            string fileName = idstring + ".png";
            fileName = "wwwroot/Images/" + fileName;
            string fileNameStaff = "https://61d6-109-97-98-41.eu.ngrok.io/Patient/Details/"+idstring;
            var image = QRCodeWriter.CreateQrCode(fileNameStaff, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng(fileName);

            return "/Images/"+idstring+".png";
        }
        public IActionResult Personal_Information_Create()
        {
            return View();
        }
        public IActionResult Contact_InformationCreate()
        {
            return View();
        }
        public IActionResult ActiveProblemsCreate()
        {
            return View();
        }
        public IActionResult Physical_ExaminationCreate()
        {
            return View();
        }
        public IActionResult Vital_SignsCreate()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Update(int id) => View(_dalPatient.GetPatient(id));

        [HttpPost]
        public IActionResult Update(Patient patient)
        {
            _dalPatient.UpdatePatient(patient);

            return RedirectToAction("Details", new { id = patient.UniquePatientId});
        }
    }
}
