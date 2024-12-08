# **OrderService - Clean Architecture, CQRS, and DDD**

## 🚀 **Vorteile und Nachteile der Clean Architecture**

### **Vorteile**
- **Klar definierte Schichten**: Die Trennung von Geschäftslogik (Domain) und technischen Details (API, Infrastructure) sorgt für eine unabhängige, flexible Architektur.
- **Einfach testbar**: Geschäftslogik und Anwendungslogik können unabhängig getestet werden.
- **Flexibilität durch Dependency Inversion**: Technische Details wie Datenbanken hängen von abstrakten Schnittstellen ab, nicht umgekehrt.
- **Clean Code Prinzipien:**
  - **SRP (Single Responsibility Principle)**: Jede Schicht hat ihre eigene Verantwortung.
  - **DIP (Dependency Inversion Principle)**: Abhängigkeiten zeigen immer zur Domänenlogik.

### **Nachteile**
- **Komplexität**: Der initiale Aufbau benötigt zusätzliche Planung und Zeit.
- **Overhead für kleine Projekte**: Die Struktur kann bei einfachen Projekten überdimensioniert wirken.
- **Steile Lernkurve**: Neue Entwickler müssen die Schichten und Prinzipien verstehen.

---

## 🧩 **Komponenten des Projekts**

### **1. Domain-Schicht**
- **Kern der Anwendung**: Enthält die Geschäftslogik und die Regeln der Domäne.
- **Bestandteile**:
  - **Entities**: Geschäftsobjekte wie `Order` und `OrderItem`.
  - **Value Objects**: Objekte, die nur durch ihre Eigenschaften identifiziert werden (z. B. `Money`).
  - **Domain Events**: Ereignisse wie `OrderCreated`, die Änderungen innerhalb der Domäne kommunizieren.
- **Bounded Context**: Die Domäne wird in klar abgegrenzte Kontexte aufgeteilt, z. B. Bestellverwaltung (`Order`) und Benutzerverwaltung (`User`).

### **2. Application-Schicht**
- **Orchestriert die Geschäftslogik**: Führt Anwendungsfälle aus (z. B. `CreateOrderCommand`).
- **Bestandteile**:
  - **Use Cases**: Kapseln spezifische Aktionen der Anwendung.
  - **MediatR**: Verarbeitet Commands und Queries, um die Anwendung modular und testbar zu halten.
  - **Validatoren**: Validieren Eingaben (z. B. mit FluentValidation).

### **3. API-Schicht**
- **Vermittler zwischen Außenwelt und Anwendung**:
  - Definiert HTTP-Endpunkte (z. B. `POST /orders`).
  - Führt einfache Validierungen durch und delegiert die Verarbeitung an die Application-Schicht.

### **4. Infrastructure-Schicht**
- **Technische Implementierungen**:
  - Datenbankzugriff (z. B. `OrderRepository`).
  - Integration mit externen Systemen.
  - Verwendung einer In-Memory-Datenbank für Tests.

### **5. Tests**
- **Unit-Tests**: Fokus auf die Domain- und Application-Schicht.
- **Integrationstests**: Überprüfen das Zusammenspiel zwischen den Schichten.

---

## ⚙️ **Warum MediatR?**

- **Entkoppelte Logik**: Commands und Queries werden von MediatR verarbeitet, wodurch direkte Abhängigkeiten vermieden werden.
- **Modularität**: Jeder Command oder Query hat einen eigenen Handler.
- **Erweiterbarkeit**: Zusätzliche Pipelines (z. B. für Logging oder Validierung) können einfach integriert werden.

---

## 📜 **Vor- und Nachteile von CQRS**

### **Vorteile**
- **Trennung von Lesen und Schreiben**: Queries und Commands sind klar voneinander getrennt.
- **Optimierung**: Lese- und Schreibmodelle können unabhängig optimiert werden.
- **Flexibilität**: Änderungen im Lese- oder Schreibmodell beeinflussen die andere Seite nicht.

### **Nachteile**
- **Erhöhte Komplexität**: Separate Modelle für Lesen und Schreiben erfordern mehr Aufwand.
- **Konsistenzprobleme**: In verteilten Systemen kann es zu Verzögerungen bei der Konsistenz kommen.

---

## 🔗 **CAP-Theorem und Event Sourcing**

### **CAP-Theorem**
- **Consistency (Konsistenz)**: Daten müssen nach jeder Operation konsistent sein.
- **Availability (Verfügbarkeit)**: Jede Anfrage erhält eine Antwort, unabhängig von Netzwerkfehlern.
- **Partition Tolerance (Partitionstoleranz)**: Das System bleibt trotz Ausfall einzelner Netzwerke funktionsfähig.

### **Event Sourcing**
- **Vorteile**:
  - Historische Änderungen können vollständig nachvollzogen werden.
  - Events können für zusätzliche Verarbeitung genutzt werden.
- **Nachteile**:
  - Komplexere Implementierung und Speicherung der Daten.

---

## 📚 **Die Domain-Schicht und DDD**

### **1. Fachliche Domäne**
Die Domain-Schicht repräsentiert die Geschäftslogik und regelt die Prozesse, z. B. das Erstellen oder Stornieren von Bestellungen.

### **2. Bounded Context**
Jeder Teil der Domäne wird in klar abgegrenzte Verantwortlichkeiten unterteilt:
- **Order-Kontext**: Verantwortlich für die Bestellverwaltung.
- **User-Kontext**: Verantwortlich für die Benutzerverwaltung.

### **3. Aggregates**
- **Order**: Ein Aggregat, das zusammenhängende Entitäten wie `OrderItem` kapselt.
- **Regeln**: Aggregates gewährleisten, dass Domänenregeln eingehalten werden (z. B. keine negativen Bestellmengen).

### **4. Domain Events**
- **Definition**: Ereignisse wie `OrderCreated` oder `OrderCancelled`, die Änderungen in der Domäne kommunizieren.
- **Verwendung**: Andere Teile der Anwendung können auf diese Events reagieren (z. B. Benachrichtigungen).

---

## ✨ **Zusammenfassung**

Dieses Projekt demonstriert die Umsetzung von **Clean Architecture**, **CQRS**, und **DDD** mit ASP.NET Core. Es bietet eine skalierbare, wartbare und flexible Basis für Anwendungen mit klar definierten Schichten und entkoppelter Logik.

---

## 📖 **Weiterführende Themen**
- [Clean Architecture von Uncle Bob](https://github.com/ardalis/CleanArchitecture)
- [MediatR-Dokumentation](https://github.com/jbogard/MediatR)
- [Domain-Driven Design von Eric Evans](https://www.domainlanguage.com/)