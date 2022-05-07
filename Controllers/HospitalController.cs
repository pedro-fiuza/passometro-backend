using Domain.Entities;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Services.HospitalService;

namespace passometro_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            this.hospitalService = hospitalService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<PagedResponse<Hospital>>>> Get(int page)
        {
            var response = await hospitalService.GetAllHospitalsPaginated(page);

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Hospital>>> Post(Hospital hospital)
        {
            var response = await hospitalService.Add(hospital);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Hospital>>> Put(Hospital hospital)
        {
            var response = await hospitalService.Update(hospital);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpGet("get-hospital-by-id")]
        public async Task<ActionResult<ServiceResponse<Hospital>>> GetHospitalById(int hospitalId)
        {
            var response = await hospitalService.GetHospitalById(hospitalId);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<PagedResponse<Hospital>>>> SearchHospitals(string searchText, int page = 1)
        {
            var response = await hospitalService.SearchHospitals(searchText, page);

            return Ok(response);
        }
    }
}
