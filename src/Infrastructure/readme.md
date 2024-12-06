# Infrastructure Layer

Die **Infrastructure-Schicht** ist Teil der Clean Architecture und verantwortlich für die Implementierung technischer Details, die von der Anwendung genutzt werden. Sie stellt sicher, dass die Domain- und Application-Schichten unabhängig von externen Systemen bleiben.

## Aufgaben der Infrastructure-Schicht

- **Datenbankzugriff**: Implementiert Repositories und verwaltet den Zugriff auf Datenbanken (z. B. mit Entity Framework Core).
- **Externe Systeme**: Integration mit APIs, Nachrichtensystemen (z. B. RabbitMQ, Kafka) oder Drittanbieter-Diensten.
- **Logging**: Bereitstellung eines zentralisierten Loggings (z. B. mit Serilog).
- **Caching**: Verwaltung von Caching-Mechanismen (z. B. MemoryCache, Redis).
- **Dependency Injection**: Registrierung aller technischen Abhängigkeiten für die Verwendung in der API- und Application-Schicht.

## Struktur

Die Infrastructure-Schicht enthält folgende Module:

- `Persistence/`: Implementierungen für Datenbankzugriff, z. B. Repositories und DbContext.
- `Logging/`: Logging-Mechanismen, z. B. Serilog oder eigene Logger.
- `Messaging/`: Integration von Nachrichtensystemen (z. B. RabbitMQ).
- `Caching/`: Implementierungen für Caching-Strategien.

## Abhängigkeiten

- Referenziert die **Application-Schicht**, um Interfaces zu implementieren.
- Wird von der **API-Schicht** genutzt, um technische Details wie Datenbankzugriffe oder Logging bereitzustellen.

## Ziel

Die Infrastructure-Schicht abstrahiert alle technischen Details und hält die Kernlogik der Anwendung sauber und unabhängig von externen Systemen.
