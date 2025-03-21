-- Create the database
CREATE DATABASE yorokoanime;
GO

-- Use the database
USE yorokoanime;
GO

-- Create the Users table
CREATE TABLE Users (
                       username NVARCHAR(255) NOT NULL PRIMARY KEY,
                       password NVARCHAR(255) NOT NULL,
                       email NVARCHAR(255) NOT NULL,
                       isAdmin BIT DEFAULT 0
);
GO

-- Create the Manga table
CREATE TABLE Manga (
                       id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                       name NVARCHAR(50),
                       image NVARCHAR(MAX),
                       totalNoChapters FLOAT,
    [desc] NVARCHAR(MAX)
    );
GO

-- Create the Anime table
CREATE TABLE Anime (
                       id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                       name NVARCHAR(50),
                       image NVARCHAR(MAX),
                       totalNoEpisodes INT,
    [desc] NVARCHAR(MAX)
    );
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegisterUser]
    @Username NVARCHAR(50),
    @Email NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    -- Insert the user values into the Users table
INSERT INTO Users (Username, Email, [Password])
VALUES (@Username, @Email, @Password);
END;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUser]
    @Username NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
SELECT *
FROM Users
WHERE Username = @Username
END
GO
