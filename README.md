# NextStop

## Das Projekt

Die guten, alten Fahrplanaushänge in den Bushaltestellen haben langsam ausgedient: Apps, APIs und digitale Anzeigetafeln übernehmen. Sie entwickeln in diesem Projekt einen Fahrplan für öffentliche Verkehrsmittel und benachrichtigen die wartenden Fahrga ste über etwaige Verspätungen.

## Dokumentation

1. Für welches Datenmodell haben Sie sich entschieden? ER-Diagramm, etwaige Besonderheiten erklären.
2. Dokumentieren Sie auf Request-Ebene den gesamten Workflow anhand eines möglichst durchgängigen Beispiels (vom Einpflegen der Haltestellen und Feiertage bis zur Planung und Durchführung einer Fahrt). Sie können ein Tool Ihrer Wahl einsetzen, z. B. Postman Workflows, VS Code, etc. HTTP-Requests inkl. HTTP-Verb, URL, Parametern, Body und Headern
3. Wie stellen Sie sicher, dass das Einchecken der Busse nur mit einem gültigen API-Key möglich ist?
4. Ein Angreifer hat Zugriff auf ein Datenbank-Backup Ihres Produktivsystems bekommen. Welchen Schaden kann er anrichten?
5. Bei welchen Teilen Ihres Systems ist eine korrekte Funktionsweise aus Ihrer Sicht am kritischsten? Welche Maßnahmen haben Sie getroffen, um sie zu gewärleisten?
6. Wie haben Sie die Berechnung passender Routen bei Fahrplanabfragen modular umgesetzt? Welche Teile Ihres Codes müssen Sie ändern, um eine andere Variante einzusetzen?
7. Welche Pru fungen fu hren Sie bei einem Check-in (der Standortdaten eines Busses) durch, um die Korrektheit der Daten zu gewa hrleisten?
8. Wie stellen Sie sicher, dass Ihre API auch in außergewöhnlichen Konstellationen (Jahreswechsel, Zeitumstellung, Feiertag in den Schulferien, etc.) stets korrekte Fahrplandaten liefert?
9. Bei der Übertragung eines API-Keys auf einen Bus ist etwas schiefgelaufen, der Bus liefert mangels gültiger Authentifizierung keine Check-in-Daten mehr. Überlegen Sie, wie Sie:
        a. dieses Problem im Betrieb mo glichst rasch erkennen ko nnen und
        b. Auskunft geben ko n-nen, seit wann das Problem besteht.
10. Denken Sie an die Skalierbarkeit Ihres Projekts: Die Wiener Linien möchten Ihr Produkt mit über 1.000 Fahrzeugen nutzen. Was macht Ihnen am meisten Kopfzerbrechen?
11. Wenn Sie das Projekt neu anfangen würden – was würden Sie anders machen?
