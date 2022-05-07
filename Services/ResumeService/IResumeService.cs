using Domain.Entities;
using Domain.Response;

namespace Services.ResumeService
{
    public interface IResumeService
    {
        Task<ServiceResponse<Resume>> Add(Resume resume, int? patientId = null);
        Task<ServiceResponse<Resume>> Update(Resume resume);
        Task<ServiceResponse<PagedResponse<Resume>>> SearchResumes(DateTime date, int page);
        Task<ServiceResponse<Resume>> GetResumeById(int id);
        Task<ServiceResponse<PagedResponse<Resume>>> GetAllResumesPaginated(int page);
        Task<ServiceResponse<PagedResponse<Resume>>> GetResumesFromPatientsPaginated(int id, int page);
    }
}
