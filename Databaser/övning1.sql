IF OBJECT_ID('dbo.Bilregister', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Bilregister
    (
        Fnamn   VARCHAR(50)  NOT NULL,
        Enamn   VARCHAR(50)  NOT NULL,
        Adress  VARCHAR(100) NOT NULL,
        Postnr  CHAR(5)      NOT NULL,
        Stad    VARCHAR(100) NOT NULL,
        Telefon VARCHAR(20)  NOT NULL,
        Email   VARCHAR(254) NOT NULL,
        Märke   VARCHAR(50)  NOT NULL,
        Modell  VARCHAR(50)  NOT NULL
    );

    INSERT INTO dbo.Bilregister
        (Fnamn, Enamn, Adress, Postnr, Stad, Telefon, Email, Märke, Modell)
    VALUES
        ('Lars', 'Persson',  'Skolg. 2', '23456', 'Hässleholm', '07212344', 'lars@live.se', 'Ferrari',  'F40'),
        ('Anna', 'Nilsson',  'Borgv. 4', '14577', 'Stockholm',  '07012345', 'anna@live.se', 'Bugatti',  'Veyron'),
        ('Anna', 'Nilsson',  'Borgv. 4', '14577', 'Stockholm',  '07012345', 'anna@live.se', 'Mercedes', 'C112'),
        ('Lars', 'Persson',  'Skolg. 2', '23456', 'Hässleholm', '07212344', 'lars@live.se', 'Toyota',   'Corolla'),
        ('Bo',   'Karlsson', 'Furuv. 8', '12388', 'Göteborg',   '01055223', 'bo@live.se',   'Nissan',   'Pulsar'),
        ('Lars', 'Persson',  'Skolg. 2', '23456', 'Hässleholm', '07212344', 'lars@live.se', 'Ford',     'Mustang');

    -- 1. Lägg till två bilar för Bo Karlsson.
    INSERT INTO dbo.Bilregister
        (Fnamn, Enamn, Adress, Postnr, Stad, Telefon, Email, Märke, Modell)
    VALUES
        ('Bo', 'Karlsson', 'Furuv. 8', '12388', 'Göteborg', '01055223', 'bo@live.se', 'Batmobilen1', 'Batmobil2'),
        ('Bo', 'Karlsson', 'Furuv. 8', '12388', 'Göteborg', '01055223', 'bo@live.se', 'batmobilen2', 'batmobil2');
END;
GO

-- 2. tex om bo flyttar måste vi ändra adressen på alla hans bilrader.
-- När vi skriver in uppgifterna flera gånger kan vi också råka skriva
-- olika mejladresser för samma person.
--onödiga dupliceringar.

-- 3. En primary key identifierar en viss rad och måste vara unik.
-- En foreign key  kopplar ihop tabeller.

-- 4. Jag skulle välja att ha AgareID som primärnyckel iom då kan vi registrera en ägare även om personen saknar svenskt personnummer.

-- 5. En främmande nyckel behöver inte vara unik. Samma AgareID får finnas på flera bilrader eftersom en ägare kan ha flera bilar.

-- 6. Data är enskilda värden, till exempel 'Bo', 'Nissan' och 'Pulsar'. När vi kopplar ihop värdena får vi information om att Bo äger en Nissan Pulsar.

-- 7. 1 - N: En bilägare kan äga flera bilar. Bo äger tre bilar, och varje bil i registret är kopplad till en ägare.
-- N - N: En ägare kan äga flera bilmodeller, och flera ägare kan ha varsin bil av samma modell.

-- 8.
IF OBJECT_ID('dbo.carowner', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.carowner
    (
        AgareID INT IDENTITY(1,1) PRIMARY KEY,
        Fnamn   VARCHAR(50)  NOT NULL,
        Enamn   VARCHAR(50)  NOT NULL,
        Adress  VARCHAR(100) NOT NULL,
        Postnr  CHAR(5)      NOT NULL,
        Stad    VARCHAR(100) NOT NULL,
        Telefon VARCHAR(20)  NOT NULL,
        Email   VARCHAR(255) NOT NULL UNIQUE
    );

    INSERT INTO dbo.carowner (Fnamn, Enamn, Adress, Postnr, Stad, Telefon, Email)
    SELECT DISTINCT Fnamn, Enamn, Adress, Postnr, Stad, Telefon, Email
    FROM dbo.Bilregister;
END;
GO

-- Spara varje bil med en hänvisning till ägaren.
IF OBJECT_ID('dbo.car', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.car
    (
        BilID   INT IDENTITY(1,1) PRIMARY KEY,
        Märke   VARCHAR(50) NOT NULL,
        Modell  VARCHAR(50) NOT NULL,
        AgareID INT NOT NULL REFERENCES dbo.carowner(AgareID)
    );
INSERT INTO dbo.car (Märke, Modell, AgareID)
SELECT bil.Märke, bil.Modell, agare.AgareID
FROM dbo.Bilregister AS bil
JOIN dbo.carowner AS agare ON bil.Email = agare.Email;
END;
GO
-- Visa varje bil och vem som äger den.
SELECT agare.Fnamn, agare.Enamn, bil.Märke, bil.Modell
FROM dbo.car AS bil
JOIN dbo.carowner AS agare ON bil.AgareID = agare.AgareID
ORDER BY agare.Fnamn, bil.Märke;
GO


