using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using yorokoanime.DataAccessLayer;
using yorokoanime.Models;
using yorokoanime.Services;
using yorokoanime.ViewModels;

namespace yorokoanime.Controllers;

public class CardController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AnimeService _animeService;
    private readonly DatabaseMethods _databaseMethods;

    public CardController(ILogger<HomeController> logger, AnimeService animeService, DatabaseMethods databaseMethods)
    {
        _logger = logger;
        _animeService = animeService;
        _databaseMethods = databaseMethods;
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> AnimeCard(int malID)
    {
        var anime = await _animeService.GetAnime(malID);
        
        anime.RatingOptions = new SelectList(
            Enumerable.Range(1, 5).Select(i => new { Value = i, Text = i.ToString() }),
            "Value",
            "Text",
            anime.userRating
        );
        
        anime.EpisodeOptions = new SelectList(
            Enumerable.Range(0, (int)anime.Episodes + 1) // +1 to include the last episode
                .Select(i => new { Value = i, Text = i.ToString() }),
            "Value",
            "Text",
            anime.userEpisodes // pre-selected episode (optional)
        );
        
        string username = User.Identity.Name;

        if (User.Identity.IsAuthenticated)
        {
            UserFavorite favorite = _databaseMethods.GetUserFavorite(anime?.MalId, username, "Anime");

            if (favorite != null)
            {
                anime.userEpisodes = favorite.episodes;
                anime.userRating = favorite.userRating;
                anime.isLiked = favorite.hasLiked;
            }
        }

        return View(anime);
    }
    
    public async Task<IActionResult> MangaCard(int malID)
    {
        var manga = await _animeService.GetManga(malID);
        
        manga.RatingOptions = new SelectList(
            Enumerable.Range(1, 5).Select(i => new { Value = i, Text = i.ToString() }),
            "Value",
            "Text",
            manga.userRating
        );
        
        manga.VolumeOptions = new SelectList(
            Enumerable.Range(0, (int)manga.Volumes + 1) // +1 to include the last episode
                .Select(i => new { Value = i, Text = i.ToString() }),
            "Value",
            "Text",
            manga.userVolumes // pre-selected episode (optional)
        );
        
        manga.ChapterOptions = new SelectList(
            Enumerable.Range(0, (int)manga.Chapters + 1) // +1 to include the last episode
                .Select(i => new { Value = i, Text = i.ToString() }),
            "Value",
            "Text",
            manga.userChapters // pre-selected episode (optional)
        );
        
        string username = User.Identity.Name;

        if (User.Identity.IsAuthenticated)
        {
            UserFavorite favorite = _databaseMethods.GetUserFavorite(manga?.MalId, username, "Manga");

            if (favorite != null)
            {
                manga.userVolumes = favorite.volumes;
                manga.userChapters = favorite.chapters;
                manga.userRating = favorite.userRating;
                manga.isLiked = favorite.hasLiked;
            }
        }
        
        byte[]? imageBytes = await _animeService.DownloadImageAsByteArray(manga.ImageUrl);
        _databaseMethods.AddManga(manga, imageBytes);
        
        return View(manga);
    }

    [HttpPost]
    public async Task<IActionResult> SaveUserAnimePreferences(AnimeModel model)
    {
        var username = User.Identity.Name;
        _databaseMethods.SaveUserAnimePreference(model, username);

        return RedirectToAction("AnimeCard", new { malID = model.MalId });
    }
    
    [HttpPost]
    public async Task<IActionResult> SaveUserMangaPreferences(MangaModel model)
    {
        var username = User.Identity.Name;
        _databaseMethods.SaveUserMangaPreference(model, username);

        return RedirectToAction("MangaCard", new { malID = model.MalId });
    }
}