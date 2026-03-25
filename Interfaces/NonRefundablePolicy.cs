namespace HotelReservation.Interfaces;

using HotelReservation.Models;

// aucun remboursement quoi qu'il arrive
public class NonRefundablePolicy : ICancellationPolicy
{
    public decimal CalculateRefund(Reservation reservation, DateTime now) => 0m;
}
