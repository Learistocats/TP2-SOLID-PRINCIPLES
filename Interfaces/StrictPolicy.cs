namespace HotelReservation.Interfaces;

using HotelReservation.Models;

// remboursement total a 14j+, 50% entre 7 et 14j, sinon r
public class StrictPolicy : ICancellationPolicy
{
    public decimal CalculateRefund(Reservation reservation, DateTime now)
    {
        var daysBeforeCheckIn = (reservation.CheckIn - now).Days;
        if (daysBeforeCheckIn >= 14) return reservation.TotalPrice;
        if (daysBeforeCheckIn >= 7)  return reservation.TotalPrice * 0.5m;
        return 0m;
    }
}
