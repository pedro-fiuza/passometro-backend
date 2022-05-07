using Domain.Entities;
using Domain.Response;

namespace Services.PatientService
{
    public interface IPatientService
    {
        Task<ServiceResponse<Patient>> Add(Patient patient, int? hospitalId = null);
        Task<ServiceResponse<Patient>> Update(Patient patient);
        Task<ServiceResponse<PagedResponse<Patient>>> SearchPatients(string searchText, int page);
        Task<ServiceResponse<Patient>> GetPatientById(int id);
        Task<ServiceResponse<PagedResponse<Patient>>> GetAllPatientsPaginated(int page);
        Task<ServiceResponse<PagedResponse<Patient>>> GetPatientsFromHospitalPaginated(int id, int page);
    }
}
