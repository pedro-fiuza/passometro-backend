using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.HospitalService
{
    public class HospitalService : IHospitalService
    {
        private readonly DataContext context;
        public HospitalService(DataContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResponse<Hospital>> Add(Hospital hospital)
        {
            var hospitalExists = await HospitalExistsByName(hospital.Name);

            if (hospitalExists is not null)
            {
                return new ServiceResponse<Hospital>
                {
                    Success = false,
                    Message = "O hospital ja existe"
                };
            }

            context.Hospital.Add(hospital);
            await context.SaveChangesAsync();

            return new ServiceResponse<Hospital> { Data = hospital, Message = "Hospital cadastrado com sucesso!" };
        }

        public async Task<ServiceResponse<Hospital>> Update(Hospital hospital)
        {
            var result = await HospitalExistsById(hospital.Id);

            if (result is null)
                return new ServiceResponse<Hospital>
                {
                    Success = false,
                    Message = "O hospital selecionado nao existe.",
                };

            result.Name = hospital.Name;
            result.Address = hospital.Address;
            result.City = hospital.City;
            result.Uf = hospital.Uf;

            context.Hospital.Update(result);
            await context.SaveChangesAsync();

            return new ServiceResponse<Hospital>
            {
                Data = result,
                Message = "Hospital atualizado com sucesso.",
            };
        }

        private async Task<Hospital> HospitalExistsByName(string name)
        {
            return await context.Hospital.FirstOrDefaultAsync(hospital => hospital.Name.ToLower().Equals(name.ToLower()));
        }

        private async Task<Hospital> HospitalExistsById(int id)
        {
            return await context.Hospital.FirstOrDefaultAsync(hospital => hospital.Id == id);
        }

        public async Task<ServiceResponse<PagedResponse<Hospital>>> SearchHospitals(string searchText, int page)
        {
            var hospitalsFindeds = await FindHospitalsBySearchText(searchText);

            return await PaginateHospitals(hospitalsFindeds, page);
        }

        private async Task<ServiceResponse<PagedResponse<Hospital>>> PaginateHospitals(List<Hospital> hospitals, int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((hospitals).Count / pageResults);

            var paginatedHospitals = hospitals.Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToList();

            return new ServiceResponse<PagedResponse<Hospital>>
            {
                Data = new PagedResponse<Hospital>
                {
                    PagedData = paginatedHospitals,
                    CurrentPage = page,
                    Pages = (int)pageCount,
                }
            };
        }

        public async Task<ServiceResponse<Hospital>> GetHospitalById(int id)
        {
            var result = await HospitalExistsById(id);

            if (result is null)
                return new ServiceResponse<Hospital>
                {
                    Data = null,
                    Success = false,
                    Message = "O hospital nao existe."
                };

            return new ServiceResponse<Hospital> { Data = result, Message = "Hospital encontrado com sucesso." };
        }

        private async Task<List<Hospital>> FindHospitalsBySearchText(string searchText)
        {
            return await context.Hospital
                .Where(r => r.Name.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();
        }

        public async Task<ServiceResponse<PagedResponse<Hospital>>> GetAllHospitalsPaginated(int page)
        {
            var hospitals = await context.Hospital.ToListAsync();

            return await PaginateHospitals(hospitals, page);
        }
    }
}
