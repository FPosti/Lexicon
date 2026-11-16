USE School;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE SID = 6)
BEGIN
    INSERT INTO dbo.Students (SID, FName, LName)
    VALUES (6, 'Mark', 'Berg');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE SID = 7)
BEGIN
    INSERT INTO dbo.Students (SID, FName, LName)
    VALUES (7, 'Astrid', 'Lind');
END;
GO

-- Övning 5: Hämta Mark och Astrid i fallande ordning efter förnamn
CREATE OR ALTER PROCEDURE dbo.GetMarkOrAstrid
AS
BEGIN
    SELECT SID, FName, LName
    FROM dbo.Students
    WHERE FName IN ('Mark', 'Astrid')
    ORDER BY FName DESC;
END;
GO

EXEC dbo.GetMarkOrAstrid;
GO

-- Övning 6A: Sök på förnamn med Anna som stdvärde
CREATE OR ALTER PROCEDURE dbo.GetStudentsByFirstName
    @FirstName VARCHAR(20) = 'Anna'
AS
BEGIN
    SELECT SID, FName, LName
    FROM dbo.Students
    WHERE FName = @FirstName;
END;
GO

EXEC dbo.GetStudentsByFirstName;
EXEC dbo.GetStudentsByFirstName @FirstName = 'Fredrik';
GO

-- Övning 6B: Sök på början av efternamnet med P som stdvärde
CREATE OR ALTER PROCEDURE dbo.GetStudentsByLastNamePrefix
    @Prefix VARCHAR(20) = 'P'
AS
BEGIN
    SELECT SID, FName, LName
    FROM dbo.Students
    WHERE LName LIKE @Prefix + '%';
END;
GO

EXEC dbo.GetStudentsByLastNamePrefix;
GO