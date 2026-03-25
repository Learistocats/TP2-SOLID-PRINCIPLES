namespace HotelReservation.Repositories;

using HotelReservation.Models;

// lecture seule, consommateurs : InMemoryRoomRepository, CheckInService, etc.
public interface IReservationReader
{
    Reservation? GetById(string id);
    List<Reservation> GetAll();
    List<Reservation> GetByDateRange(DateTime from, DateTime to);
    List<Reservation> GetByGuest(string guestName);
}
