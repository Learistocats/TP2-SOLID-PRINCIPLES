namespace HotelReservation.Models;

// Reservation sert maintenant un seul acteur : le RECEPTIONNISTE.
// la facturation a ete deplacee dans BillingCalculator (acteur : comptable).
// le planning menage a ete deplacee dans HousekeepingScheduler (acteur : gouvernante).
public class Reservation
{
    public string Id { get; set; } = string.Empty;
    public string GuestName { get; set; } = string.Empty;
    public string RoomId { get; set; } = string.Empty;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int GuestCount { get; set; }
    public string RoomType { get; set; } = string.Empty;
    public string Status { get; set; } = "Confirmed"; // Confirmed, CheckedIn, CheckedOut, Cancelled
    public string CancellationPolicy { get; set; } = "Flexible";
    public string Email { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }

    // acteur : RECEPTIONNISTE — gestion du cycle de vie de l'annulation
    public void Cancel()
    {
        if (Status == "CheckedIn")
            throw new InvalidOperationException("Cannot cancel after check-in");
        Status = "Cancelled";
    }
}
