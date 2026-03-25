namespace HotelReservation.Services;

using HotelReservation.Repositories;

// dépend uniquement de IReservationStats, la seule méthode utilisée ici c'est GetTotalRevenue.
public class BillingService
{
    private readonly IReservationStats _repo;

    public BillingService(IReservationStats repo)
    {
        _repo = repo;
    }

    public decimal GetRevenueForPeriod(DateTime from, DateTime to)
    {
        return _repo.GetTotalRevenue(from, to);
    }
}
