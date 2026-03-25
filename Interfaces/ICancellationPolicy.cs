namespace HotelReservation.Interfaces;

using HotelReservation.Models;

// contrat que toutes les politiques d'annulation doivent respecter.
// pour ajouter une nouvelle politique, on cree une classe qui implemente ca — sans toucher au reste.
public interface ICancellationPolicy
{
    decimal CalculateRefund(Reservation reservation, DateTime now);
}
