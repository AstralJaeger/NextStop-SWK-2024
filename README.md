# NextStop

## Das Projekt

Die guten, alten Fahrplanaushänge in den Bushaltestellen haben langsam ausgedient: Apps, APIs und digitale Anzeigetafeln übernehmen. Sie entwickeln in diesem Projekt einen Fahrplan für öffentliche Verkehrsmittel und benachrichtigen die wartenden Fahrga ste über etwaige Verspätungen.

## Setup

Dieses Projekt ist über 2 Repositories verteilt. 
Aufgrund der GitHub Classroom limitation von GitHub Actions lebt der ursprüngliche Code in diesem Repo: [AstralJaeger/NextStop-SWK-2024](https://github.com/AstralJaeger/NextStop-SWK-2024). 
Die Abgabe lebt hier: [swk-20242ws/nextstop-bb-g1-kamarauli-g2-hillebrand](https://github.com/swk5-2024ws/nextstop-bb-g1-kamarauli-g2-hillebrand) und wird von GitHub Actions bei jedem merge auf den Master von dem ausgangsrepository gepusht.

## Testdaten

Die Testdaten des Projektes wurden aus den Fahrplänen der LinzAG für die Stadt Linz erstellt:
- [LinzAG Netzlinienplan](https://www.linzag.at/media/dokumente/linien_1/infomaterial_1/linien-linienfahrplan.pdf)
- [LinzAG Fahrplan](https://www.linzag.at/media/dokumente/linien_1/infomaterial_1/linien-linienfahrplan.pdf)

## Dokumentation

1. Für welches Datenmodell haben Sie sich entschieden? ER-Diagramm, etwaige Besonderheiten erklären.
2. Dokumentieren Sie auf Request-Ebene den gesamten Workflow anhand eines möglichst durchgängigen Beispiels (vom Einpflegen der Haltestellen und Feiertage bis zur Planung und Durchführung einer Fahrt). Sie können ein Tool Ihrer Wahl einsetzen, z. B. Postman Workflows, VS Code, etc. HTTP-Requests inkl. HTTP-Verb, URL, Parametern, Body und Headern
3. Wie stellen Sie sicher, dass das Einchecken der Busse nur mit einem gültigen API-Key möglich ist?
4. Ein Angreifer hat Zugriff auf ein Datenbank-Backup Ihres Produktivsystems bekommen. Welchen Schaden kann er anrichten?
5. Bei welchen Teilen Ihres Systems ist eine korrekte Funktionsweise aus Ihrer Sicht am kritischsten? Welche Maßnahmen haben Sie getroffen, um sie zu gewährleisten?
6. Wie haben Sie die Berechnung passender Routen bei Fahrplanabfragen modular umgesetzt? Welche Teile Ihres Codes müssen Sie ändern, um eine andere Variante einzusetzen?
7. Welche Prüfungen führen Sie bei einem Check-in (der Standortdaten eines Busses) durch, um die Korrektheit der Daten zu gewährleisten?
8. Wie stellen Sie sicher, dass Ihre API auch in außergewöhnlichen Konstellationen (Jahreswechsel, Zeitumstellung, Feiertag in den Schulferien, etc.) stets korrekte Fahrplandaten liefert?
9. Bei der Übertragung eines API-Keys auf einen Bus ist etwas schiefgelaufen, der Bus liefert mangels gültiger Authentifizierung keine Check-in-Daten mehr. Überlegen Sie, wie Sie:
        a. dieses Problem im Betrieb mo glichst rasch erkennen können und
        b. Auskunft geben können, seit wann das Problem besteht.
10. Denken Sie an die Skalierbarkeit Ihres Projekts: Die Wiener Linien möchten Ihr Produkt mit über 1.000 Fahrzeugen nutzen. Was macht Ihnen am meisten Kopfzerbrechen?
11. Wenn Sie das Projekt neu anfangen würden – was würden Sie anders machen?
