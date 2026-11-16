IF DB_ID('School') IS NULL
    EXEC('CREATE DATABASE School');
GO

USE School;
GO

IF OBJECT_ID('dbo.Students', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students
    (
        SID       INT         NOT NULL PRIMARY KEY,
        FName     VARCHAR(20) NOT NULL,
        LName     VARCHAR(20) NOT NULL,
        [Comment] TEXT        NULL
    );

    INSERT INTO dbo.Students (SID, FName, LName, [Comment])
    VALUES
        (1, 'Anna', 'Lind', 'Studerar SQL'),
        (2, 'Erik', 'Berg', NULL),
        (3, 'Sara', 'Lund', 'Gillar programmering');
END;

SELECT SID, FName, LName, [Comment]
FROM dbo.Students
ORDER BY SID;