namespace HotelReservation.Interfaces;

// implemente seulement IReservation; Cancel() n'existe pas sur ce type.
// le compilateur interdit l'appel, plus besoin de lever une exception au runtime.
public class NonRefundableReservation : IReservation
{
    public string Id { get; set; } = string.Empty;
    public string GuestName { get; set; } = string.Empty;
    public string Status { get; set; } = "Confirmed";
    public decimal TotalPrice { get; set; }

    public decimal CalculateRefund() => 0m;
}
