namespace HotelReservation.Services;

using HotelReservation.Models;
using HotelReservation.Interfaces;

// plus de switch ici — on recoit la politique par injection.
// ajouter une nouvelle politique = nouvelle classe, zero modif ici.
public class CancellationService
{
    private readonly ICancellationPolicy _policy;

    public CancellationService(ICancellationPolicy policy)
    {
        _policy = policy;
    }

    public decimal CalculateRefund(Reservation reservation, DateTime now)
    {
        return _policy.CalculateRefund(reservation, now);
    }

    public void CancelReservation(Reservation reservation, DateTime now)
    {
        var refund = CalculateRefund(reservation, now);
        reservation.Cancel();
        Console.WriteLine(
            $"[OK] Reservation {reservation.Id} cancelled " +
            $"({reservation.CancellationPolicy} policy: " +
            $"{(refund == reservation.TotalPrice ? "full" : "partial")} refund of {refund:F2} EUR)");
    }
}
