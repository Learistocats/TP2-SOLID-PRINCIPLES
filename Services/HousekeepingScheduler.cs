namespace HotelReservation.Services;

using HotelReservation.Models;

// acteur : GOUVERNANTE — logique de plannification des changements de linge.
// extrait de Reservation pour que seul la gouvernante puisse demander des modifs ici.
public class HousekeepingScheduler
{
    public List<DateTime> GetLinenChangeDays(Reservation reservation)
    {
        var days    = new List<DateTime>();
        var current = reservation.CheckIn.AddDays(3);
        while (current < reservation.CheckOut)
        {
            days.Add(current);
            current = current.AddDays(3);
        }
        return days;
    }
}
