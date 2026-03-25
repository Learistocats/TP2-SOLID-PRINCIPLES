namespace HotelReservation.Repositories;

using HotelReservation.Models;

// écriture seule, consommateurs : BookingService, CancellationService, etc.
public interface IReservationWriter
{
    void Add(Reservation reservation);
    void Update(Reservation reservation);
    void Delete(string id);
}
