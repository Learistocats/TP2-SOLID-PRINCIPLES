namespace HotelReservation.Services;

using HotelReservation.Models;

// dépend uniquement de ICleaningNotifier, défini dans ce même namespace
// aucune référence à l'infrastructure ici
public class HousekeepingService
{
    private readonly ICleaningNotifier _notifier;

    public HousekeepingService(ICleaningNotifier notifier)
    {
        _notifier = notifier;
    }

    public List<CleaningTask> GenerateLinenChangeSchedule(Reservation reservation)
    {
        var tasks   = new List<CleaningTask>();
        var current = reservation.CheckIn.AddDays(3);
        while (current < reservation.CheckOut)
        {
            tasks.Add(new CleaningTask
            {
                RoomId           = reservation.RoomId,
                Date             = current,
                Type             = "LinenChange",
                HousekeeperEmail = "housekeeping@masdesoliviers.fr",
                Time             = new TimeSpan(10, 0, 0)
            });
            current = current.AddDays(3);
        }
        return tasks;
    }

    public void NotifyHousekeeper(CleaningTask task)
    {
        _notifier.NotifyTask(task);
    }
}
