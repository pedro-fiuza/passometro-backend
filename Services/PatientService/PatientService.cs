using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly DataContext context;

        public PatientService(DataContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResponse<Patient>> Add(Patient patient, int? hospitalId = null)
        {
            var result = await context.Hospital.Include(p => p.Patients)
                                             .FirstOrDefaultAsync(f => f.Id == hospitalId);

            result.Patients.Add(patient);
            await context.SaveChangesAsync();

            return new ServiceResponse<Patient> { Data = patient, Message = "Paciente criado com sucesso." };
        }

        public async Task<ServiceResponse<PagedResponse<Patient>>> GetAllPatientsPaginated(int page)
        {
            var patients = await context.Patient.ToListAsync();

            return await PaginatePatients(patients, page);
        }

        public async Task<ServiceResponse<Patient>> GetPatientById(int id)
        {
            var result = await context.Patient.FirstOrDefaultAsync(patient => patient.Id == id);

            if (result is null)
                return new ServiceResponse<Patient>
                {
                    Data = null,
                    Success = false,
                    Message = "O paciente nao existe."
                };

            return new ServiceResponse<Patient> { Data = result, Message = "Paciente encontrado com sucesso." };
        }

        public async Task<ServiceResponse<PagedResponse<Patient>>> GetPatientsFromHospitalPaginated(int id, int page)
        {
            var hospitalSearch = await context.Hospital
                                      .AsNoTracking()
                                      .Include(r => r.Patients)
                                      .FirstOrDefaultAsync(h => h.Id == id);

            return await PaginatePatients(hospitalSearch.Patients?.ToList(), page);
        }

        public async Task<ServiceResponse<PagedResponse<Patient>>> SearchPatients(string searchText, int page)
        {
            var patientsFinded = await FindPatientsBySearchText(searchText);

            return await PaginatePatients(patientsFinded, page);
        }

        public async Task<ServiceResponse<Patient>> Update(Patient patient)
        {
            var result = await context.Patient.FirstOrDefaultAsync(patient => patient.Id == patient.Id);

            if (result is null)
                return new ServiceResponse<Patient>
                {
                    Success = false,
                    Message = "O paciente selecionado nao existe"
                };

            result.Name = patient.Name;
            result.Age = patient.Age;
            result.MotherName = patient.MotherName;
            result.BirthDate = patient.BirthDate;

            context.Patient.Update(result);
            await context.SaveChangesAsync();

            return new ServiceResponse<Patient>
            {
                Data = result,
                Message = "Paciente atualizado com sucesso",
            };
        }

        private async Task<ServiceResponse<PagedResponse<Patient>>> PaginatePatients(List<Patient> patients, int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((patients).Count / pageResults);

            var paginatedPatients = patients.Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToList();

            return new ServiceResponse<PagedResponse<Patient>>
            {
                Data = new PagedResponse<Patient>
                {
                    PagedData = paginatedPatients,
                    CurrentPage = page,
                    Pages = (int)pageCount,
                }
            };
        }

        private async Task<List<Patient>> FindPatientsBySearchText(string searchText)
        {
            return await context.Patient
                .Where(r => r.Name.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();
        }
    }
}
