# JSON-oriented rest API server in Lua

## Tech stack 🚀:
- Lua
- Lapis
- Moonscript
- MySQL
- Nginx

## What is it?
A really basic REST API server that has:
- Basic JSON-oriented CRUD endpoints (potatoes and species)
- Two data models (Potatoes and Species) based on the `lapis.db.model` package.
- Written with Moonscript and compiled to lua

## Data
![image](https://user-images.githubusercontent.com/75375838/213319968-270a9eca-4da8-4e11-9667-d73c3ee86346.png)

## Endpoints showcase
Attached you can find screenshots of various `GET` requests made with `Insomnia`.
<br/>
(Only `GET`s out of sheer convienience, there are obviously also `POST`s, `DELETE`s etc.)

- `"/potatoes"` - Gets all available potatoes.
![image](https://user-images.githubusercontent.com/75375838/213321654-787205bf-d6c3-4ab2-b7d6-4e45968d4212.png)

- `"/species"` - Gets all available species.
![image](https://user-images.githubusercontent.com/75375838/213321163-37e6e7f4-e92d-4e47-92d2-0158af18fd8c.png)

- `"/potatoes/:id"` - Gets a potato with the given `id`.
![image](https://user-images.githubusercontent.com/75375838/213321500-0812e17c-8b61-49e7-b0a7-9295e92b90a1.png)

- `"/species/:id"` - Gets all potatoes with the given `SpeciesID`(id).
![image](https://user-images.githubusercontent.com/75375838/213321326-9dbb567f-037c-462a-b100-a4276e2a1d45.png)
