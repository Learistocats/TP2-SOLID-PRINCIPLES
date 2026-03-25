namespace HotelReservation.Services;

using HotelReservation.Models;

// ProcessCheckIn et ProcessCheckOut contienent maintenant que des etapes de haut niveau.
// la manipulation du cache et les notifs sont deleguees a des methodes privees.
public class CheckInService
{
    private readonly Dictionary<string, CacheEntry> _cache = new();
    private readonly Dictionary<string, Reservation> _dataStore;
    private const decimal LateCheckInFee = 25m;

    public CheckInService(Dictionary<string, Reservation> dataStore)
    {
        _dataStore = dataStore;
    }

    public void ProcessCheckIn(Reservation reservation)
    {
        ValidateCheckInStatus(reservation);
        RefreshCache(reservation.Id, "CheckedIn");
        ApplyLateCheckInFee(reservation);
        reservation.Status = "CheckedIn";
        NotifyRoomStatus(reservation.RoomId, "occupied");
    }

    public void ProcessCheckOut(Reservation reservation)
    {
        if (reservation.Status != "CheckedIn")
            throw new Exception($"Cannot check out: reservation is {reservation.Status}");

        reservation.Status = "CheckedOut";
        InvalidateCache(reservation.Id);
        NotifyRoomStatus(reservation.RoomId, "free");
    }

    private void ValidateCheckInStatus(Reservation reservation)
    {
        if (reservation.Status != "Confirmed")
            throw new Exception($"Cannot check in: reservation is {reservation.Status}");
    }

    private void RefreshCache(string reservationId, string status)
    {
        _cache.Remove(reservationId);
        _cache[reservationId] = new CacheEntry(DateTime.Now, status);
    }

    private void ApplyLateCheckInFee(Reservation reservation)
    {
        if (DateTime.Now.Hour >= 22)
            reservation.TotalPrice += LateCheckInFee;
    }

    private void InvalidateCache(string reservationId)
    {
        _cache.Remove(reservationId);
    }

    private void NotifyRoomStatus(string roomId, string status)
    {
        Console.WriteLine($"[SMS] Room {roomId} is now {status}");
    }
}
