using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Services.ResumeService;

namespace passometro_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService resumeService;

        public ResumeController(IResumeService resumeService)
        {
            this.resumeService = resumeService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<PagedResponse<Resume>>>> Get(int page)
        {
            var response = await resumeService.GetAllResumesPaginated(page);

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Resume>>> Post(CreateResumeDto resume)
        {
            var response = await resumeService.Add(new Resume
            {
                Bed = resume.Bed,
                AdmissionDate = resume.AdmissionDate,
                Complications = resume.Complications,
                Surgeries = resume.Surgeries,
                MainDiagnosis = resume.MainDiagnosis,
                ProposalOfTheDay = resume.ProposalOfTheDay
            }, resume.PatientId);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Resume>>> Put(Resume resume)
        {
            var response = await resumeService.Update(resume);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpGet("get-resume-by-id")]
        public async Task<ActionResult<ServiceResponse<Resume>>> GetResumeById(int resumeId)
        {
            var response = await resumeService.GetResumeById(resumeId);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-resumes-from-patient")]
        public async Task<ActionResult<ServiceResponse<List<PagedResponse<Resume>>>>> GetResumesFromPatient(int patientId, int page = 1)
        {
            var response = await resumeService.GetResumesFromPatientsPaginated(patientId, page);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<List<PagedResponse<Resume>>>>> SearchResumes([FromQuery] DateTime date, int page = 1)
        {
            var response = await resumeService.SearchResumes(date, page);

            return Ok(response);
        }
    }
}
