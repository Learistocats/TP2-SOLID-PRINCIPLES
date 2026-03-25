namespace HotelReservation.Infrastructure;

using HotelReservation.Services;

// implémentation infrastructure du contrat ILogger défini dans le domaine métier.
public class FileLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}
