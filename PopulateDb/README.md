## Populate Db

Een tool om een database te vullen met test data.
Erg nuttig voor testen.

## Requirements
- Microsoft.EntityFrameworkCore

## Compileren
Dit programma is afhankelijk van code in ../AirplaneApp, daarom moet u hier eerst een referentie naar deze map maken voordat u verder kunt.
Dit kunt u doen met deze command: `dotnet add reference ../AirplaneApp`.

## Gebruik
U kunt het programma als normaal uitvoeren, maar het heeft wel twee argumenten nodig:
- data (pad naar directory met json bestanden.)
- db (pad naar een sqlite database bestand.)

In deze repository staat al een map met test data die u kunt gebruiken: TestData.

Een voorbeeld hoe u het programma kan uitvoeren: `dotnet run -- ../AirplaneApp/airport.db ../TestData`.
