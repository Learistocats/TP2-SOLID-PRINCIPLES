# JUSTIFICATION.md

## SRP

`ReservationService` mélangeait trois niveaux : l'accès aux données, les règles métier (disponibilité, capacité, prix) et l'orchestration du workflow. J'ai séparé ça en trois : `ReservationDomainService` pour les règles métier, `InMemoryReservationStore` pour la persistance, et `ReservationService` qui orchestre. Les acteurs sont distincts : le comptable demande des changements sur `BillingCalculator`, la gouvernante sur `HousekeepingScheduler`, le réceptionniste sur `Reservation`. Chacun a son propre endroit à modifier sans risquer de casser les autres.

## OCP

Les bons exemples déjà en place : le pattern Observer dans `ReservationEventDispatcher` (ajouter un handler = nouvelle classe, pas de modif), le Decorator avec `SeasonalSurchargeDecorator` (nouvelle règle de prix = nouveau décorateur), et le Strategy avec `ICleaningPolicy` (nouvelle stratégie = nouvelle implémentation). Pour `CancellationService`, le switch sur la politique forçait à modifier la classe à chaque nouvelle politique. J'ai extrait `ICancellationPolicy` avec quatre implémentations (`FlexiblePolicy`, `ModeratePolicy`, `StrictPolicy`, `NonRefundablePolicy`). Maintenant ajouter une politique "SuperFlexible" = nouvelle classe, rien d'autre à toucher.

## LSP

`NonRefundableReservation` implémentait `ICancellable` mais levait une exception sur `Cancel()`. N'importe quel code qui manipulait un `ICancellable` pouvait exploser au runtime. J'ai séparé l'interface en `IReservation` (base, sans Cancel) et `ICancellableReservation` (avec Cancel). `NonRefundableReservation` implémente seulement `IReservation`, le compilateur interdit maintenant l'appel invalide. Pour `CachedRoomRepository`, il ignorait les paramètres de date et retournait des données périmées, cassant le contrat de `IRoomRepository`. Corrigé : `GetAvailableRooms` délègue toujours au repo interne, et `Save` invalide le cache.

## ISP

`IReservationRepository` avait 9 méthodes alors que `BillingService` n'utilisait que `GetTotalRevenue` et `InMemoryRoomRepository` que `GetByDateRange`. J'ai découpé en trois interfaces focalisées : `IReservationReader`, `IReservationWriter`, `IReservationStats`. Chaque consommateur déclare uniquement ce dont il a besoin. Même chose pour `INotificationService` qui regroupait 4 canaux de notification alors que chaque consommateur n'en utilisait qu'un. Découpé en `IEmailNotifier`, `ISmsNotifier`, `IPushNotifier`, `ISlackNotifier`.

## DIP

`BookingService` instanciait directement `InMemoryReservationStore` et `FileLogger`, rendant impossible tout changement de stockage ou de logging sans modifier la classe. J'ai défini `IReservationStore` et `ILogger` dans le namespace métier, les implémentations infrastructure implémentent ces interfaces, et `BookingService` les reçoit par constructeur. Pour `HousekeepingService`, même problème avec `EmailSender`. J'ai défini `ICleaningNotifier` côté domaine et créé `EmailCleaningNotifier` comme adaptateur dans l'infrastructure. La règle : l'interface appartient au consommateur, pas au fournisseur.
