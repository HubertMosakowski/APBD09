using APBD09.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APBD09.Repositories;

public class TripRepository: ITripRepository
{
    private IConfiguration _configuration;
    private readonly MasterContext _context;
    
    public TripRepository(MasterContext context)
    {
        _context = context;
    }
    
    public void setConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<Trip>> getTrip(int page = 1, int pageSize = 10)
    {
        var totalTrips = await _context.Trips.CountAsync();
        var totalPages = (int)System.Math.Ceiling(totalTrips / (double)pageSize);
        List<Trip> tripsList = new();

        var trips = await _context.Trips
            .Include(t => t.Name)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(tripsList);

        var result = new
        {
            pageNum = page,
            pageSize = pageSize,
            allPages = totalPages,
            trips = trips.Select(t => new
            {
                t.Name,
                t.Description,
                DateFrom = t.DateFrom.ToString("yyyy-MM-dd"),
                DateTo = t.DateTo.ToString("yyyy-MM-dd"),
                t.MaxPeople,
                Countries = t.CountryTrips.Select(ct => new { ct.Country.Name }),
                Clients = t.ClientTrips.Select(ct => new { ct.Client.FirstName, ct.Client.LastName })
            })
        };

        return Ok(result);
    }

    public async Task deleteClient()
    {
        
    }

    public async Task postClientToTrip()
    {
        
    }
}