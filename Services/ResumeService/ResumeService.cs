using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.ResumeService
{
    public class ResumeService : IResumeService
    {
        private readonly DataContext context;

        public ResumeService(DataContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResponse<Resume>> Add(Resume resume, int? patientId = null)
        {
            var patient = await context.Patient.Include(p => p.Resumes)
                                                .FirstOrDefaultAsync(f => f.Id == patientId);

            patient.Resumes.Add(resume);
            await context.SaveChangesAsync();

            return new ServiceResponse<Resume> { Data = resume, Message = "Prontuario criado com sucesso." };
        }

        public async Task<ServiceResponse<PagedResponse<Resume>>> GetAllResumesPaginated(int page)
        {
            var resumes = await context.Resume.ToListAsync();

            return await PaginateResumes(resumes, page);
        }

        public async Task<ServiceResponse<Resume>> GetResumeById(int id)
        {
            var result = await context.Resume.FirstOrDefaultAsync(r => r.Id == id);

            if (result is null)
                return new ServiceResponse<Resume>
                {
                    Data = null,
                    Success = false,
                    Message = "O prontuario nao existe."
                };

            return new ServiceResponse<Resume> { Data = result, Message = "Prontuario encontrado com sucesso." };
        }

        private async Task<ServiceResponse<PagedResponse<Resume>>> PaginateResumes(List<Resume> resumes, int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling((resumes).Count / pageResults);

            var paginatedResumes = resumes.Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToList();

            return new ServiceResponse<PagedResponse<Resume>>
            {
                Data = new PagedResponse<Resume>
                {
                    PagedData = paginatedResumes,
                    CurrentPage = page,
                    Pages = (int)pageCount,
                }
            };
        }

        public async Task<ServiceResponse<PagedResponse<Resume>>> SearchResumes(DateTime date, int page)
        {
            var result = await context.Resume
                .AsNoTracking()
                .Where(x => x.AdmissionDate.Value.Date == date.Date)
                .ToListAsync();

            return await PaginateResumes(result, page);
        }

        public async Task<ServiceResponse<PagedResponse<Resume>>> GetResumesFromPatientsPaginated(int id, int page)
        {
            var resumeSearch = await context.Patient
                                      .AsNoTracking()
                                      .Include(r => r.Resumes)
                                      .FirstOrDefaultAsync(p => p.Id == id);

            return await PaginateResumes(resumeSearch.Resumes?.ToList(), page);
        }

        public async Task<ServiceResponse<Resume>> Update(Resume resume)
        {
            var result = await context.Resume.FirstOrDefaultAsync(rsm => rsm.Id == resume.Id);

            if (result is null)
                return new ServiceResponse<Resume>
                {
                    Success = false,
                    Message = "O prontuario selecionado nao existe."
                };

            result.Bed = resume.Bed;
            result.AdmissionDate = resume.AdmissionDate;
            result.Surgeries = resume.Surgeries;
            result.MainDiagnosis = resume.MainDiagnosis;
            result.Complications = resume.Complications;
            result.ProposalOfTheDay = resume.ProposalOfTheDay;

            context.Resume.Update(result);
            await context.SaveChangesAsync();

            return new ServiceResponse<Resume>
            {
                Data = result,
                Message = "Prontuario atualizado com sucesso."
            };
        }
    }
}
