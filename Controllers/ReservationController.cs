using Microsoft.AspNetCore.Mvc;
using PusherServer;
using resevation_be.DTOs;
using resevation_be.models;
using resevation_be.Services;

namespace resevation_be.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ReservationController : ControllerBase
{
    IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync([FromHeader] string authorization)
    {
        return await _reservationService.GetUserReservationsAsync(authorization);
    }
    [HttpPost]
    public async Task<bool> Reseve([FromBody] ReservationDto reservation, [FromHeader] string authorization)
    {
        return await _reservationService.ReseveAsync(reservation, authorization);
    }

}
