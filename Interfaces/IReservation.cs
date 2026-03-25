namespace HotelReservation.Interfaces;

// interface de base commune a toutes les reservations
// pas de Cancel ici,pas totues les reservations sont annulables
public interface IReservation
{
    string Id { get; }
    string GuestName { get; }
    string Status { get; }
    decimal TotalPrice { get; }
    decimal CalculateRefund();
}
