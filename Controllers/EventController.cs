using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Services.EventService;

namespace passometro_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<PagedResponse<Event>>>> Get(int page)
        {
            var response = await eventService.GetAllEventsPaginated(page);

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Event>>> Post(CreateEventDto eventEntity)
        {
            var response = await eventService.Add(new Event
            {
                EventDate = eventEntity.EventDate,
                Description = eventEntity.Description,
                Title = eventEntity.Title,
            }, eventEntity.ResumeId);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Event>>> Put(Event eventEntity)
        {
            var response = await eventService.Update(eventEntity);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpGet("get-event-by-id")]
        public async Task<ActionResult<ServiceResponse<Event>>> GetEventById(int eventId)
        {
            var response = await eventService.GetEventById(eventId);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-events-from-resume")]
        public async Task<ActionResult<ServiceResponse<PagedResponse<Resume>>>> GetEventsFromResume(int resumeId, int page = 1)
        {
            var response = await eventService.GetEventsFromResumePaginated(resumeId, page);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<PagedResponse<Resume>>>> SearchEvents([FromQuery] DateTime date, int page = 1)
        {
            var response = await eventService.SearchEvents(date, page);

            return Ok(response);
        }
    }
}
