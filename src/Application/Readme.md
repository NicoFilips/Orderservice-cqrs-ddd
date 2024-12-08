# Application Layer

Die **Application-Schicht** ist das Herzstück der Geschäftslogik und koordiniert die Abläufe zwischen der API-Schicht und der Domain-Schicht. Sie sorgt dafür, dass Anfragen orchestriert und die Geschäftsregeln eingehalten werden.

## Aufgaben der Application-Schicht

- **Koordination:**  
  Vermittlung zwischen der API und der Domain, um Anfragen und deren Verarbeitung zu steuern.

- **Command- und Query-Handling:**  
  Verarbeitung von Befehlen (Commands) und Anfragen (Queries) über dedizierte Handler.

- **Integration von Validierungen:**  
  Sicherstellung der Eingabevalidierung, z. B. mit Bibliotheken wie `FluentValidation`.

- **Abstraktion der Infrastruktur:**  
  Zugriff auf Infrastruktur-Komponenten erfolgt ausschließlich über Abstraktionen wie Repositories und Services.

- **Trennung von Verantwortlichkeiten:**  
  Keine Geschäftslogik (gehört in die Domain) und keine Infrastruktur-Details (gehören in die Infrastruktur).

## Bestandteile der Application-Schicht

### Commands
Befehle, die aus der API-Schicht kommen und eine spezifische Aktion auslösen, z. B. das Erstellen oder Bearbeiten von Daten.

### Command-Handler
Handler, die für die Ausführung von Commands zuständig sind. Sie koordinieren die Geschäftslogik in Zusammenarbeit mit der Domain-Schicht.

### Queries
Anfragen, die Daten aus der Anwendung abrufen, ohne dabei den Zustand der Anwendung zu ändern.

### Query-Handler
Handler, die Queries bearbeiten und sicherstellen, dass die benötigten Daten abgerufen werden.

### Services
Zusätzliche Funktionen innerhalb der Application-Schicht, z. B. Validierungen oder andere unterstützende Aufgaben.

## Prinzipien der Application-Schicht

1. **Unabhängigkeit:**  
   Die Application-Schicht ist unabhängig von der Infrastruktur und greift nur über definierte Schnittstellen wie Repositories und Services darauf zu.

2. **Kein Zustand:**  
   Die Application-Schicht speichert keinen Zustand; sie agiert als Vermittler.

3. **Fokus auf Orchestrierung:**  
   Sie enthält keine komplexe Geschäftslogik, sondern ruft die Domain-Schicht für diese Aufgaben auf.

---

Die Application-Schicht ist zentral für die korrekte Ausführung der Geschäftslogik und gewährleistet eine saubere Trennung der Verantwortlichkeiten in der Anwendung.
