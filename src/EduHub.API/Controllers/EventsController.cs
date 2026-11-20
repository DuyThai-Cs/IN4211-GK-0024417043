using EduHub.Application.Events;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

[ApiController]
[Route("api")]
public class EventsController : ControllerBase
{
    private readonly EventService _service;

    public EventsController(EventService service)
    {
        _service = service;
    }

    /// <summary>Lấy danh sách sự kiện của CLB.</summary>
    [HttpGet("clubs/{clubId:guid}/events")]
    [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
    public IActionResult GetByClub(Guid clubId) => Ok(_service.GetByClub(clubId));

    /// <summary>Xem một sự kiện theo Id.</summary>
    [HttpGet("events/{id:guid}")]
    [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var ev = _service.GetById(id);
        return ev is null ? NotFound() : Ok(ev);
    }

    /// <summary>Tạo sự kiện mới cho CLB.</summary>
    [HttpPost("clubs/{clubId:guid}/events")]
    [ProducesResponseType(typeof(Event), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(Guid clubId, [FromBody] CreateEventRequest request)
    {
        try
        {
            var ev = _service.CreateEvent(clubId, request.Title, request.Date, request.Capacity, request.Channel);
            return CreatedAtAction(nameof(GetById), new { id = ev.Id }, ev);
        }
        catch (ArgumentException ex)   // tiêu đề rỗng, sức chứa <= 0, kênh không hỗ trợ
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Xoá một sự kiện.</summary>
    [HttpDelete("events/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _service.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}

public record CreateEventRequest(string Title, DateTime Date, int Capacity, string Channel = "email");