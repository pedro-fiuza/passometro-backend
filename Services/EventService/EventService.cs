using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.EventService
{
    public class EventService : IEventService
    {
        private readonly DataContext context;
        public EventService(DataContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResponse<Event>> Add(Event eventEntity, int? resumeId = null)
        {
            var resume = await context.Resume.Include(p => p.Events)
                                                .FirstOrDefaultAsync(f => f.Id == resumeId);

            resume.Events.Add(eventEntity);
            await context.SaveChangesAsync();

            return new ServiceResponse<Event> { Data = eventEntity, Message = "Evento criado com sucesso." };
        }

        public async Task<ServiceResponse<PagedResponse<Event>>> GetAllEventsPaginated(int page)
        {
            var events = await context.Event.ToListAsync();

            return await PaginateEvents(events, page);
        }

        public async Task<ServiceResponse<Event>> GetEventById(int id)
        {
            var result = await context.Event.FirstOrDefaultAsync(r => r.Id == id);

            if (result is null)
                return new ServiceResponse<Event>
                {
                    Data = null,
                    Success = false,
                    Message = "O evento nao existe."
                };

            return new ServiceResponse<Event> { Data = result, Message = "Evento encontrado com sucesso." };
        }

        private async Task<ServiceResponse<PagedResponse<Event>>> PaginateEvents(List<Event> eventEntity, int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling((eventEntity).Count / pageResults);

            var paginatedEvents = eventEntity.Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToList();

            return new ServiceResponse<PagedResponse<Event>>
            {
                Data = new PagedResponse<Event>
                {
                    PagedData = paginatedEvents,
                    CurrentPage = page,
                    Pages = (int)pageCount,
                }
            };
        }

        public async Task<ServiceResponse<PagedResponse<Event>>> GetEventsFromResumePaginated(int id, int page)
        {
            var eventSearch = await context.Resume
                                      .AsNoTracking()
                                      .Include(r => r.Events)
                                      .FirstOrDefaultAsync(p => p.Id == id);

            return await PaginateEvents(eventSearch.Events?.ToList(), page);
        }

        public async Task<ServiceResponse<PagedResponse<Event>>> SearchEvents(DateTime date, int page)
        {
            var result = await context.Event
                .AsNoTracking()
                .Where(x => x.EventDate.Date == date.Date)
                .ToListAsync();

            return await PaginateEvents(result, page);
        }

        public async Task<ServiceResponse<Event>> Update(Event eventEntity)
        {
            var result = await context.Event.FirstOrDefaultAsync(ev => ev.Id == eventEntity.Id);

            if (result is null)
                return new ServiceResponse<Event>
                {
                    Success = false,
                    Message = "O evento selecionado nao existe."
                };


            result.Description = eventEntity.Description;
            result.Title = eventEntity.Title;
            result.EventDate = eventEntity.EventDate;

            context.Event.Update(result);
            await context.SaveChangesAsync();

            return new ServiceResponse<Event>
            {
                Data = result,
                Message = "Evento atualizado com sucesso."
            };
        }
    }
}
