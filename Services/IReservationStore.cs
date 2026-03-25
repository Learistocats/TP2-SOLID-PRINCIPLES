namespace HotelReservation.Services;

using HotelReservation.Models;

// interface de stockage définie côté consommateur, dans le namespace métier.
// permet de changer l'implémentation sans toucher à BookingService.
public interface IReservationStore
{
    void Add(Reservation reservation);
    Reservation? GetById(string id);
    List<Reservation> GetAll();
}
