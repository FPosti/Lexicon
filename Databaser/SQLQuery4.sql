use master;
CREATE DATABASE Employee;
GO

USE Employee;
GO


IF OBJECT_ID('dbo.Avdelning', 'U') IS NULL
CREATE TABLE Avdelning
(
    Avdelningskod CHAR(1) NOT NULL,
    Namn NVARCHAR(12),
    Ansvarig INT NULL,
    CONSTRAINT PK_Avdelning PRIMARY KEY (Avdelningskod)
);
GO


IF OBJECT_ID('dbo.Anställd', 'U') IS NULL
CREATE TABLE Anställd
(
    Nummer INT NOT NULL,
    Namn NVARCHAR(6),
    Telefon VARCHAR(10),
    Lön INT,
    Chef INT NULL,
    Avdelning CHAR(1),
    CONSTRAINT PK_Anställd PRIMARY KEY (Nummer)
);
GO

-- FK
ALTER TABLE Anställd
ADD CONSTRAINT FK_Anställd_Chef
FOREIGN KEY (Chef)
REFERENCES Anställd (Nummer);
GO

ALTER TABLE Anställd
ADD CONSTRAINT FK_Anställd_Avdelning
FOREIGN KEY (Avdelning)
REFERENCES Avdelning (Avdelningskod);
GO

ALTER TABLE Avdelning
ADD CONSTRAINT FK_Avdelning_Ansvarig
FOREIGN KEY (Ansvarig)
REFERENCES Anställd (Nummer);
GO


INSERT INTO Avdelning (Avdelningskod, Namn)
VALUES ('H', N'Högkvarteret');

INSERT INTO Avdelning (Avdelningskod, Namn)
VALUES ('S', N'Säkerhet');

INSERT INTO Avdelning (Avdelningskod, Namn)
VALUES ('C', N'Data');
GO

-- fyller data
INSERT INTO Anställd (Nummer, Namn, Telefon, Lön, Avdelning)
VALUES (2, N'Stina', '2677', 30000, 'H');

INSERT INTO Anställd (Nummer, Namn, Telefon, Lön, Chef, Avdelning)
VALUES (3, N'Saddam', '1088', 22000, 2, 'S');

INSERT INTO Anställd (Nummer, Namn, Telefon, Lön, Chef, Avdelning)
VALUES (4, N'Lotta', '4590', 28000, 2, 'H');

INSERT INTO Anställd (Nummer, Namn, Telefon, Lön, Chef, Avdelning)
VALUES (1, N'Olle', '2688', 20000, 3, 'S');

INSERT INTO Anställd (Nummer, Namn, Telefon, Lön, Chef, Avdelning)
VALUES (8, N'Maria', '2690', 25000, 4, 'C');

INSERT INTO Anställd (Nummer, Namn, Telefon, Lön, Chef, Avdelning)
VALUES (9, N'Ulrik', '2698', 26000, 8, 'C');

INSERT INTO Anställd (Nummer, Namn, Telefon, Lön, Chef, Avdelning)
VALUES (10, N'Petter', '2645', 22000, 8, 'C');
GO


UPDATE Avdelning
SET Ansvarig = 2
WHERE Avdelningskod = 'H';

UPDATE Avdelning
SET Ansvarig = 3
WHERE Avdelningskod = 'S';

UPDATE Avdelning
SET Ansvarig = 8
WHERE Avdelningskod = 'C';
GO


SELECT *
FROM Anställd;

SELECT *
FROM Avdelning;
GO

SELECT * FROM Avdelning;

