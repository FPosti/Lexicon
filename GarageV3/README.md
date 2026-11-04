# GarageV3

Ett konsolprogram i C# där man kan hantera ett garage. Övningen handlar om arv och generiska klasser samt interfaces och samlingar.

## Funktioner

Man väljer hur många platser garaget ska ha när programmet startar. Det går också att fylla det med några exempelfordon.

I menyn kan man parkera och hämta ut fordon. Man kan lista alla fordon och se hur många det finns av varje typ.

Det går att söka på regnummer och färg samt antal hjul och fordonstyp. Flera filter kan användas samtidigt. Tomma filter hoppas över.

Garaget stoppar dubbla regnummer och fler fordon när det är fullt. Man kan parkera bilar och motorcyklar. Det finns även flygplan samt bussar och båtar.

## Hur det fungerar

Garage är en generisk klass där T bestämmer vilken typ av fordon som får sparas. T måste vara Vehicle eller ärva från Vehicle. Programmet använder Vehicle så olika fordonstyper kan ligga i samma garage. Fordonen sparas i en privat List.

Kapaciteten sätts när garaget skapas och Count visar hur många fordon som finns i listan. TryPark kollar att regnumret inte redan finns och att det finns plats. Om något inte stämmer returneras false och en feltext. Annars läggs fordonet till. Remove letar upp fordonet med regnumret och tar bort det från listan.

Vehicle är en abstrakt basklass för alla fordon. Där finns regnummer och färg samt antal hjul. Det går inte att skapa en Vehicle direkt utan man skapar en underklass som Car. Varje fordonstyp har en egen egenskap som bränsletyp för bilen. ToString sätter ihop dom gemensamma uppgifterna med fordonets egna detaljer vid utskrift.

Program skapar en GarageHandler och en GarageConsole. Menyerna använder interfacet IHandler för att anropa handlern. Handlern skapar garaget och skickar vidare anrop för parkering och sökning. Den räknar också antal fordon per typ med en Dictionary där typnamnet är nyckeln och antalet är värdet.

Search använder LINQ Where för att välja ut fordon som matchar sökningen. Alla ifyllda filter måste stämma samtidigt. Tomma filter hoppas över och stora och små bokstäver räknas som samma. För hjulantal betyder null att filtret inte används medan 0 betyder fordon utan hjul. Träffarna samlas i en ny lista.

Garaget implementerar IEnumerable så det går att gå igenom fordonen med foreach. GetEnumerator använder listans iterator som håller reda på vilket fordon som är aktuellt. Det finns en version med T och en utan T. Båda använder samma iterator.

ConsoleInput läser in svaren och frågar igen om något är fel. RegistrationNumberValidator kollar att regnumret följer formatet ABC123 eller ABC12A. Godkända regnummer sparas med stora bokstäver. GarageConsole håller igång menyn tills man väljer att avsluta.

