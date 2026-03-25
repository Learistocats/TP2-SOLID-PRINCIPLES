namespace HotelReservation.Services;

// interface définie côté consommateur, dans le namespace métier.
// l'infrastructure implémente ce contrat, pas l'inverse.
public interface ILogger
{
    void Log(string message);
}
