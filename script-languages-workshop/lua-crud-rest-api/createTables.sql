CREATE TABLE Species (
    id int NOT NULL,
    SpeciesName VARCHAR(255),
    PRIMARY KEY (id)
);

CREATE TABLE Potatoes (
    id int,
    Price FLOAT,
    PotatoesName VARCHAR(255),
    SpeciesID int NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (SpeciesID) REFERENCES Species(id)
);