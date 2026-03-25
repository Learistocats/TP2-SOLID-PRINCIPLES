namespace HotelReservation.Services;

using HotelReservation.Models;

// interface définie dans le domaine métier, HousekeepingService ne connaît que ça
// l'infrastructure décide comment la notification est envoyée
public interface ICleaningNotifier
{
    void NotifyTask(CleaningTask task);
}
