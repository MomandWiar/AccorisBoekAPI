# .NET 9 API Project - Books & Authors

Dit project is een Web API gebouwd met **.NET 9** voor het beheren van boeken en auteurs. De API biedt functionaliteit om boeken en auteurs te lezen, toe te voegen, bij te werken en te verwijderen.

### Technologieën gebruikt in dit project:
- **C# (.NET 9)**
- **ASP.NET Core Web API**
- **Entity Framework Core** (SQLite of InMemory als database)
- **CQRS (Command Query Responsibility Segregation)** met **MediatR**
- **Async Programming & LINQ** voor asynchrone dataoperaties
- **Dependency Injection** voor het beheer van afhankelijkheden
- **Unit Testing** met **xUnit** en **Moq**

## API Endpoints

### Auteurs Endpoints

1. **Get All Authors**
   - **Endpoint**: `GET /api/authors`
   - **Beschrijving**: Haal een lijst van alle auteurs op.
   - **Antwoord**:
     - 200 OK: Een lijst van auteurs in JSON-formaat.

2. **Get Author By ID**
   - **Endpoint**: `GET /api/authors/{id}`
   - **Beschrijving**: Haal een auteur op via het ID.
   - **Parameters**: `id` (int) - Het ID van de auteur.
   - **Antwoord**:
     - 200 OK: De auteur in JSON-formaat.
     - 404 Not Found: Als de auteur niet wordt gevonden.

3. **Create Author**
   - **Endpoint**: `POST /api/authors`
   - **Beschrijving**: Voeg een nieuwe auteur toe.
   - **Body**:
     ```json
     {
       "name": "Auteur Naam",
       "birthdate": "1990-01-01"
     }
     ```
   - **Antwoord**:
     - 201 Created: De aangemaakte auteur in JSON-formaat.
     - 400 Bad Request: Bij validatiefouten.

4. **Update Author**
   - **Endpoint**: `PUT /api/authors/{id}`
   - **Beschrijving**: Werk de gegevens van een auteur bij.
   - **Parameters**: `id` (int) - Het ID van de auteur.
   - **Body**:
     ```json
     {
       "id": 1,
       "name": "Nieuwe Naam",
       "birthdate": "1990-01-01"
     }
     ```
   - **Antwoord**:
     - 204 No Content: Als de auteur succesvol is bijgewerkt.
     - 400 Bad Request: Als de ID in de URL en body niet overeenkomen.
     - 404 Not Found: Als de auteur niet wordt gevonden.

5. **Delete Author**
   - **Endpoint**: `DELETE /api/authors/{id}`
   - **Beschrijving**: Verwijder een auteur.
   - **Parameters**: `id` (int) - Het ID van de auteur.
   - **Antwoord**:
     - 204 No Content: Als de auteur succesvol is verwijderd.
     - 404 Not Found: Als de auteur niet wordt gevonden.

---

### Boeken Endpoints

1. **Get All Books**
   - **Endpoint**: `GET /api/books`
   - **Beschrijving**: Haal een lijst van alle boeken op.
   - **Antwoord**:
     - 200 OK: Een lijst van boeken in JSON-formaat.

2. **Get Book By ID**
   - **Endpoint**: `GET /api/books/{id}`
   - **Beschrijving**: Haal een boek op via het ID.
   - **Parameters**: `id` (int) - Het ID van het boek.
   - **Antwoord**:
     - 200 OK: Het boek in JSON-formaat.
     - 404 Not Found: Als het boek niet wordt gevonden.

3. **Create Book**
   - **Endpoint**: `POST /api/books`
   - **Beschrijving**: Voeg een nieuw boek toe.
   - **Body**:
     ```json
     {
       "title": "Boektitel",
       "authorId": 1,
       "publicationDate": "2023-01-01"
     }
     ```
   - **Antwoord**:
     - 201 Created: Het aangemaakte boek in JSON-formaat.
     - 400 Bad Request: Bij validatiefouten.

4. **Update Book**
   - **Endpoint**: `PUT /api/books/{id}`
   - **Beschrijving**: Werk de gegevens van een boek bij.
   - **Parameters**: `id` (int) - Het ID van het boek.
   - **Body**:
     ```json
     {
       "id": 1,
       "title": "Nieuwe Titel",
       "authorId": 1,
       "publicationDate": "2023-02-01"
     }
     ```
   - **Antwoord**:
     - 204 No Content: Als het boek succesvol is bijgewerkt.
     - 400 Bad Request: Als de ID in de URL en body niet overeenkomen.
     - 404 Not Found: Als het boek niet wordt gevonden.

5. **Delete Book**
   - **Endpoint**: `DELETE /api/books/{id}`
   - **Beschrijving**: Verwijder een boek.
   - **Parameters**: `id` (int) - Het ID van het boek.
   - **Antwoord**:
     - 204 No Content: Als het boek succesvol is verwijderd.
     - 404 Not Found: Als het boek niet wordt gevonden.

---

## Foutafhandelingscodes

- **400 Bad Request**: De client heeft een ongeldig verzoek gestuurd, bijvoorbeeld door onjuiste parameters.
- **404 Not Found**: De gevraagde resource bestaat niet.
- **500 Internal Server Error**: Er is een onverwachte serverfout opgetreden.

---

## Toekomstige verbeteringen

- Toevoegen van paginering aan de `GET`-requests.
- Meer gedetailleerde foutmeldingen en validatie voor gebruikersinvoer.

