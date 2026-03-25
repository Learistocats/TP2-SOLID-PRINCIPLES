namespace HotelReservation.Infrastructure;

using HotelReservation.Models;
using HotelReservation.Services;

// adaptateur infrastructure qui implémente ICleaningNotifier via EmailSender
// pour passer aux SMS, on crée un autre adaptateur sans toucher au domaine
public class EmailCleaningNotifier : ICleaningNotifier
{
    private readonly EmailSender _emailSender = new();

    public void NotifyTask(CleaningTask task)
    {
        _emailSender.Send(
            task.HousekeeperEmail,
            "New cleaning task",
            $"Room {task.RoomId} needs {task.Type} on {task.Date:dd/MM/yyyy}");
    }
}
