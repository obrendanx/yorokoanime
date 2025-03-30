CREATE PROCEDURE AddAnime
    @malId INT = NULL,
    @titleSynonyms NVARCHAR(MAX) = NULL, -- Can store JSON or comma-separated values
    @imageUrl NVARCHAR(MAX) = NULL,
    @episodes INT = NULL,
    @synopsis NVARCHAR(MAX) = NULL,
    @score DECIMAL(4,2) = NULL,
    @rank INT = NULL,
    @popularity INT = NULL,
    @members INT = NULL,
    @favorites INT = NULL,
    @airedFrom DATE = NULL,
    @airedTo DATE = NULL,
    @year INT = NULL,
    @trailerUrl NVARCHAR(MAX) = NULL,
    @background NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

INSERT INTO [dbo].[Anime] (
    malId, titleSynonyms, imageUrl, episodes, synopsis, score, rank, popularity,
    members, favorites, airedFrom, airedTo, year, trailerUrl, background
)
VALUES (
    @malId, @titleSynonyms, @imageUrl, @episodes, @synopsis, @score, @rank,
    @popularity, @members, @favorites, @airedFrom, @airedTo, @year, @trailerUrl, @background
    );
END
GO