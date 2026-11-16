USE School;
GO

IF OBJECT_ID('dbo.testTrigger', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.testTrigger
    (
        FirstName VARCHAR(50),
        LastName VARCHAR(50)
    );
END;
GO

IF OBJECT_ID('dbo.stopInsert', 'TR') IS NOT NULL
BEGIN
    EXEC('DISABLE TRIGGER dbo.stopInsert ON dbo.testTrigger;');
END;
GO

CREATE OR ALTER TRIGGER dbo.info
ON dbo.testTrigger
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    PRINT 'En ny post har lagts in';

    SELECT 'Ny post' AS Beskrivning, FirstName, LastName
    FROM inserted;

    SELECT FirstName, LastName
    FROM dbo.testTrigger;
END;
GO

INSERT INTO dbo.testTrigger (FirstName, LastName)
VALUES ('Michael', 'Svensson');
GO

CREATE OR ALTER TRIGGER dbo.modify
ON dbo.testTrigger
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    PRINT 'En post har uppdaterats';

    SELECT 'Gamla värden' AS Beskrivning, FirstName, LastName
    FROM deleted;

    SELECT 'Nya värden' AS Beskrivning, FirstName, LastName
    FROM inserted;

    SELECT FirstName, LastName
    FROM dbo.testTrigger;
END;
GO

UPDATE dbo.testTrigger
SET LastName = 'Persson'
WHERE FirstName = 'Michael' AND LastName = 'Svensson';
GO

CREATE OR ALTER TRIGGER dbo.stopInsert
ON dbo.testTrigger
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    PRINT 'Du får inte lägga in nya poster';

    SELECT FirstName, LastName
    FROM dbo.testTrigger;
END;
GO

INSERT INTO dbo.testTrigger (FirstName, LastName)
VALUES ('Michael', 'Svensson');
GO

EXEC('DISABLE TRIGGER dbo.stopInsert ON dbo.testTrigger;');
GO