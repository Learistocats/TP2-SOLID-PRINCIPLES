namespace HotelReservation.Services;

using HotelReservation.Models;
using HotelReservation.Infrastructure;

// couche applicative : orchestre le workflow en coordinant le service domaine,
// le store et le logging. contient aucune regle metier en soit.
public class ReservationService
{
    private readonly ReservationDomainService _domain;
    private readonly InMemoryReservationStore _store;
    private int _counter = 0;

    public ReservationService(ReservationDomainService domain, InMemoryReservationStore store)
    {
        _domain = domain;
        _store  = store;
    }

    public string CreateReservation(string guestName, string roomId, DateTime checkIn,
        DateTime checkOut, int guestCount, string roomType, string email)
    {
        Console.WriteLine($"[LOG] Creating reservation for {guestName}...");

        var room = _domain.GetRoom(roomId);
        _domain.ValidateCapacity(room, guestCount);
        _domain.CheckAvailability(roomId, checkIn, checkOut, _store.GetAll());

        var total = _domain.CalculatePrice(room, checkIn, checkOut);

        _counter++;
        var reservation = new Reservation
        {
            Id       = $"R-{_counter:D3}",
            GuestName = guestName,
            RoomId    = roomId,
            CheckIn   = checkIn,
            CheckOut  = checkOut,
            GuestCount = guestCount,
            RoomType  = roomType,
            Status    = "Confirmed",
            Email     = email,
            TotalPrice = total
        };
        _store.Add(reservation);

        Console.WriteLine($"[LOG] Reservation {reservation.Id} created.");
        return reservation.Id;
    }

    public Reservation? GetReservation(string id) => _store.GetById(id);

    public List<Reservation> GetAllReservations() => _store.GetAll();

    public static List<Room> GetRooms() => ReservationDomainService.GetAllRooms();
}
