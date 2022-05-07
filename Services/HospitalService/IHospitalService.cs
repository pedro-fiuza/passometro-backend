using Domain.Entities;
using Domain.Response;

namespace Services.HospitalService
{
    public interface IHospitalService
    {
        Task<ServiceResponse<Hospital>> Add(Hospital hospital);
        Task<ServiceResponse<Hospital>> Update(Hospital hospital);
        Task<ServiceResponse<PagedResponse<Hospital>>> SearchHospitals(string searchText, int page);
        Task<ServiceResponse<Hospital>> GetHospitalById(int id);
        Task<ServiceResponse<PagedResponse<Hospital>>> GetAllHospitalsPaginated(int page);
    }
}
