namespace HotelReservation.Services;

using HotelReservation.Models;

// couche metier : contient uniquement les regles domaine et les validations.
// gere le catalogue des chambres, connait rien a la persistance ou au logging.
public class ReservationDomainService
{
    private static readonly List<Room> _rooms = new()
    {
        new Room { Id = "101", Type = "Standard", MaxGuests = 2, PricePerNight = 80m },
        new Room { Id = "102", Type = "Standard", MaxGuests = 2, PricePerNight = 80m },
        new Room { Id = "201", Type = "Suite",    MaxGuests = 2, PricePerNight = 200m },
        new Room { Id = "301", Type = "Family",   MaxGuests = 4, PricePerNight = 120m }
    };

    public Room GetRoom(string roomId)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == roomId);
        if (room == null)
            throw new Exception($"Room {roomId} not found");
        return room;
    }

    public void ValidateCapacity(Room room, int guestCount)
    {
        if (guestCount > room.MaxGuests)
            throw new Exception($"Room {room.Id} max capacity is {room.MaxGuests}");
    }

    public void CheckAvailability(string roomId, DateTime checkIn, DateTime checkOut,
        IEnumerable<Reservation> existingReservations)
    {
        var isAvailable = !existingReservations.Any(r =>
            r.RoomId == roomId &&
            r.Status != "Cancelled" &&
            r.CheckIn < checkOut &&
            r.CheckOut > checkIn);

        if (!isAvailable)
            throw new Exception($"Room {roomId} is not available for {checkIn:dd/MM} -> {checkOut:dd/MM}");
    }

    public decimal CalculatePrice(Room room, DateTime checkIn, DateTime checkOut)
    {
        var nights = (checkOut - checkIn).Days;
        return nights * room.PricePerNight;
    }

    public static List<Room> GetAllRooms() => _rooms;
}
