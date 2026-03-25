namespace HotelReservation.Interfaces;

// uniquement pour les reservations qui supportent vraiment l'annulation
// en etendant IReservation, le compilateur empeche d'appeler Cancel sur une NonRefundable
public interface ICancellableReservation : IReservation
{
    void Cancel();
}
