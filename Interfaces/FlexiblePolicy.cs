namespace HotelReservation.Interfaces;

using HotelReservation.Models;

// remboursement total si annulation au moins 1 jour avant, sinon rien
public class FlexiblePolicy : ICancellationPolicy
{
    public decimal CalculateRefund(Reservation reservation, DateTime now)
    {
        var daysBeforeCheckIn = (reservation.CheckIn - now).Days;
        return daysBeforeCheckIn >= 1 ? reservation.TotalPrice : 0m;
    }
}
