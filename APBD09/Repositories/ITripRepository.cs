using APBD09.Models;

namespace APBD09.Repositories;

public interface ITripRepository
{
    public Task<List<Trip>> getTrip(int page, int pageSize);

    public Task postClientToTrip();

    public Task deleteClient();

    public void setConfig(IConfiguration configuration);
}