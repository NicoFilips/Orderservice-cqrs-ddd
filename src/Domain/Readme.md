# Domain Layer

Die **Domain-Schicht** ist das Herzstück der Clean Architecture und enthält die **Kernlogik** und die **Geschäftsregeln** der Anwendung. Sie ist vollständig unabhängig von äußeren Schichten (z. B. API oder Infrastructure) und stellt sicher, dass die Logik der Anwendung nicht durch technische Details beeinflusst wird.

## Aufgaben der Domain-Schicht

- **Definiert Geschäftsregeln und Invarianten**:
    - Zentrale Logik, die die Integrität der Anwendung sicherstellt.
- **Unabhängig von Frameworks**:
    - Keine Abhängigkeiten von technischen Details oder externen Bibliotheken.
- **Enthält die grundlegenden Bausteine**:
    - Entitäten, Value Objects, Domain-Events und Aggregate Roots.
- **Domänensprache**:
    - Implementiert eine klar definierte Sprache (Ubiquitous Language) in Zusammenarbeit mit Domain-Experten.

## Struktur

Die Domain-Schicht könnte wie folgt aufgebaut sein:

- `Entities/`: Repräsentieren die Kernobjekte der Domäne mit Identität und Geschäftslogik.
- `ValueObjects/`: Repräsentieren Objekte ohne eigene Identität, die durch ihre Werte definiert sind (z. B. `Money`, `Address`).
- `Aggregates/`: Gruppieren zusammengehörige Entitäten, um Konsistenzgrenzen sicherzustellen.
- `DomainEvents/`: Ereignisse, die wichtige Zustandsänderungen in der Domäne beschreiben.
- `Exceptions/`: Domänenspezifische Fehler.
- `Interfaces/`: Abstraktionen, die von anderen Schichten implementiert werden (z. B. Repositories).

## Beispiel

### **Entität**
```csharp
public class Order
{
    public Guid Id { get; private set; }
    public string CustomerName { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
    }
}
