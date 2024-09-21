# Webalapú Jegyzetelő Alkalmazás

Ez a projekt egy webalapú jegyzetelő alkalmazás, amely lehetővé teszi a felhasználók számára jegyzetek létrehozását, szerkesztését és törlését. \
Az alkalmazás egy ASP.NET WebAPI-t használ a háttérben, Blazor webalkalmazásként van megvalósítva, és az adatokat SQLite adatbázisban tárolja.

## Funkciók

- **Jegyzetek létrehozása**: Új jegyzetek hozzáadása egy egyszerű űrlapon keresztül.
- **Jegyzetek szerkesztése**: Már meglévő jegyzetek módosítása a felhasználói felületen.
- **Jegyzetek megtekintése**: A felhasználók böngészhetnek az összes elérhető jegyzet között.
- **Jegyzetek törlése**: Jegyzetek eltávolítása a rendszerből.
- **Felhasználói azonosítás**: A felhasználókat jelenleg a rendszer generálja. A generált felhasználókkal lehet belépni a rendszerbe.

## Adatbázis

![image](https://github.com/user-attachments/assets/e7a33313-91f8-4951-b0c2-3ef3e57fb80f)


## Végpontok (API)

### GET /api/notes
- **Leírás**: Az összes jegyzet lekérése. \
- **Válasz**: JSON tömb, amely tartalmazza a jegyzeteket.
  ```json
  [
      {
        "title": "string",
        "content": "string",
        "id": "string",
        "createdAt": "2024-09-21T11:27:51.717Z",
        "userId": "string"
      }
  ]

### GET /api/notes/{id}
**Leírás**: Egy jegyzet lekérése azonosító alapján. \
**Paraméterek**: id - A jegyzet egyedi azonosítója. \
**Válasz**: JSON objektum a kért jegyzettel.
```json
{
  "title": "string",
  "content": "string",
  "id": "string",
  "createdAt": "2024-09-21T11:27:51.717Z",
  "userId": "string"
}
```
### POST /api/notes
**Leírás**: Új jegyzet létrehozása. \
**Request Body**: JSON objektum, amely tartalmazza az új jegyzet adatait.
```json
{
  "title": "Új Jegyzet",
  "content": "Ez az új jegyzet tartalma."
}
```
**Válasz**: Az újonnan létrehozott jegyzet azonosítója és adatai.
### PUT /api/notes/{id}
**Leírás**: Egy meglévő jegyzet frissítése. \
**Paraméterek**: id - A jegyzet azonosítója.\
**Request Body**: JSON objektum a frissített jegyzet adataival.
```json
{
  "title": "Frissített Jegyzet",
  "content": "Ez a jegyzet frissített tartalma."
}
```
### DELETE /api/notes/{id}
**Leírás**: Egy jegyzet törlése azonosító alapján. \
**Paraméterek**: id - A jegyzet azonosítója.\
**Válasz**: Üres válasz sikeres törlés esetén.

## Technológiák
**ASP.NET Core WebAPI**: A jegyzetek kezelése és az adatbázissal való kapcsolattartás. \
**Blazor WebApp**: A felhasználói felület megvalósítása egy modern webalkalmazásban. \
**SQLite**: Könnyű, fájl alapú adatbázis, amely a jegyzetek tárolására szolgál. \
**Entity Framework Core**: ORM (Object-Relational Mapping) réteg, amely megkönnyíti az adatbázis műveleteket az alkalmazásban.
