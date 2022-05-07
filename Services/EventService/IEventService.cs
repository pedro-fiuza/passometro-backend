using Domain.Entities;
using Domain.Response;

namespace Services.EventService
{
    public interface IEventService
    {
        Task<ServiceResponse<Event>> Add(Event eventEntity, int? resumeId = null);
        Task<ServiceResponse<Event>> Update(Event eventEntity);
        Task<ServiceResponse<PagedResponse<Event>>> SearchEvents(DateTime date, int page);
        Task<ServiceResponse<Event>> GetEventById(int id);
        Task<ServiceResponse<PagedResponse<Event>>> GetAllEventsPaginated(int page);
        Task<ServiceResponse<PagedResponse<Event>>> GetEventsFromResumePaginated(int id, int page);
    }
}
