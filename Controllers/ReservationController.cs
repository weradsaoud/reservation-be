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
        bool res = await _reservationService.ReseveAsync(reservation, authorization);
        var options = new PusherOptions
        {
            Cluster = "eu",
            Encrypted = true
        };

        var pusher = new Pusher(
          "1745003",
          "39eebe5a77c510fba350",
          "25b16c57fc79871f6539",
          options);

        var result = await pusher.TriggerAsync(
          "my-channel",
          "my-event",
          new { message = "reservation was added successfully!" });
        return res;
    }

}
