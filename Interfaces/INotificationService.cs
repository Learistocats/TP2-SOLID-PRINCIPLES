namespace HotelReservation.Interfaces;

// interfaces séparées par canal, chaque consommateur ne dépend que de ce qu'il utilise vraiment

public interface IEmailNotifier
{
    void SendEmail(string to, string subject, string body);
}

public interface ISmsNotifier
{
    void SendSms(string phoneNumber, string message);
}

public interface IPushNotifier
{
    void SendPushNotification(string deviceId, string message);
}

public interface ISlackNotifier
{
    void SendSlackMessage(string channel, string message);
}

// interface complète pour les implémentations qui gèrent tous les canaux
public interface INotificationService : IEmailNotifier, ISmsNotifier, IPushNotifier, ISlackNotifier
{
}
