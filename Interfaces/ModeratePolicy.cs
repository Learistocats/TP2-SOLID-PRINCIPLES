namespace HotelReservation.Interfaces;

using HotelReservation.Models;

// remboursement total a 5j+, 50% entre 2 et 5j, sinon rien
public class ModeratePolicy : ICancellationPolicy
{
    public decimal CalculateRefund(Reservation reservation, DateTime now)
    {
        var daysBeforeCheckIn = (reservation.CheckIn - now).Days;
        if (daysBeforeCheckIn >= 5) return reservation.TotalPrice;
        if (daysBeforeCheckIn >= 2) return reservation.TotalPrice * 0.5m;
        return 0m;
    }
}
