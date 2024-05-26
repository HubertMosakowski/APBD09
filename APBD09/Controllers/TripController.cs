using Microsoft.AspNetCore.Mvc;
using APBD09.Models;
using APBD09.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http.HttpResults;

namespace APBD09.Controllers;

[ApiController]
[System.Web.Http.Route("api/trips")]
public class TripController: Controller
{
    private readonly ITripRepository _tripRepository;
    private readonly IConfiguration _configuration;

    public TripController(ITripRepository tripRepository, IConfiguration configuration)
    {
        _tripRepository = tripRepository;
        _configuration = configuration;
        _tripRepository.setConfig(_configuration);
    }

    [HttpGet("/api/trips")]
    public async Task<ActionResult> GetTrips()
    {
        return Ok(await _tripRepository.getTrip());
    }

    [HttpPost("/api/trips.{idTrip}/clients")]
    public async Task<ActionResult> PostClientToTrip()
    {
        await _tripRepository.postClientToTrip();
        return Created();
    }

    [HttpDelete("/api/clients/{idClient}")]
    public async Task<ActionResult> DeleteClient()
    {
        await _tripRepository.deleteClient();
        return Ok();
    }
}