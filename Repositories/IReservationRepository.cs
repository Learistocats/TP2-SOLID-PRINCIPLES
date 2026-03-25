namespace HotelReservation.Repositories;

using HotelReservation.Models;

// interface complète qui regroupe les trois contrats
// InMemoryReservationRepository implémente celle-ci pour tout couvrir d'un coup
public interface IReservationRepository : IReservationReader, IReservationWriter, IReservationStats
{
}
