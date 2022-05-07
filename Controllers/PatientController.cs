using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Services.PatientService;

namespace passometro_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;

        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<PagedResponse<Patient>>>> Get(int page)
        {
            var response = await patientService.GetAllPatientsPaginated(page);

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Patient>>> Post(CreatePatientDto patient)
        {
            var response = await patientService.Add(new Patient
            {
                Name = patient.Name,
                Age = patient.Age,
                BirthDate = patient.BirthDate,
                MotherName = patient.MotherName,
            }, patient.HospitalId);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Patient>>> Put(Patient patient)
        {
            var response = await patientService.Update(patient);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpGet("get-patient-by-id")]
        public async Task<ActionResult<ServiceResponse<Patient>>> GetPatientById(int patientId)
        {
            var response = await patientService.GetPatientById(patientId);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-patients-from-hospital")]
        public async Task<ActionResult<ServiceResponse<List<PagedResponse<Patient>>>>> GetPatientsFromHospital(int hospitalId, int page = 1)
        {
            var response = await patientService.GetPatientsFromHospitalPaginated(hospitalId, page);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<List<PagedResponse<Patient>>>>> SearchPatients(string searchText, int page = 1)
        {
            var response = await patientService.SearchPatients(searchText, page);

            return Ok(response);
        }
    }
}
