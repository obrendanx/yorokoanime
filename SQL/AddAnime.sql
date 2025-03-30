CREATE PROCEDURE AddAnime
    @malId INT = NULL,
    @name NVARCHAR(255) = NULL,
    @image NVARCHAR(MAX) = NULL,
    @totalNoEpisodes INT = NULL,
    @synopsis NVARCHAR(MAX) = NULL,
    @score DECIMAL(4,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;

INSERT INTO [dbo].[Anime] (malId, name, image, totalNoEpisodes, synopsis, score)
VALUES (@malId, @name, @image, @totalNoEpisodes, @synopsis, @score);
END
GO