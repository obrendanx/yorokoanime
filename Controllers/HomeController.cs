using System.Diagnostics;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using yorokoanime.DataAccessLayer;
using yorokoanime.Models;
using yorokoanime.Services;
using yorokoanime.ViewModels;

namespace yorokoanime.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AnimeService _animeService;
    private readonly DatabaseMethods _databaseMethods;

    public HomeController(ILogger<HomeController> logger, AnimeService animeService, DatabaseMethods databaseMethods)
    {
        _logger = logger;
        _animeService = animeService;
        _databaseMethods = databaseMethods;
    }

    public async Task<IActionResult> Index()
    {
        List<AnimeModel> topAnime = await _animeService.GetTopAnime();
        List<MangaModel> topManga = await _animeService.GetTopManga();
        List<AnimeModel> airingAnime = await _animeService.GetTopAiringAnime();
        List<MangaModel> favoriteMangaModels = new();

        if (User.Identity.IsAuthenticated)
        {
            string username = User.Identity.Name;

            // Get user's manga favorites
            List<UserFavorite> mangaFav = await _databaseMethods.GetFavoriteManga(username);
            
            if (mangaFav != null)
            {
                foreach (var favorite in mangaFav)
                {
                    var mangaEntity = _databaseMethods.GetManga(favorite.malID);

                    if (mangaEntity != null)
                    {
                        var mangaViewModel = new MangaModel
                        {
                            MalId = mangaEntity.MalId ?? 0,
                            Url = mangaEntity.Url,
                            Approved = mangaEntity.Approved,
                            MainTitle = mangaEntity.MainTitle,
                            TitleEnglish = mangaEntity.TitleEnglish,
                            TitleJapanese = mangaEntity.TitleJapanese,
                            Titles = new List<TitleData>
                            {
                                new TitleData { Type = "Default", Title = mangaEntity.MainTitle }
                            },
                            Images = new ImageContainer
                            {
                                Jpg = new MangaImageData
                                {
                                    ImageUrl = mangaEntity.ImageUrl
                                }
                            },
                            Chapters = mangaEntity.Chapters,
                            Volumes = mangaEntity.Volumes,
                            Status = mangaEntity.Status,
                            Publishing = mangaEntity.Publishing ?? false,
                            Published = new Published
                            {
                                From = mangaEntity.PublishedFrom,
                                To = mangaEntity.PublishedTo,
                                String = mangaEntity.PublishedString,
                                Prop = new PublishedProp
                                {
                                    From = new DateParts
                                    {
                                        Day = mangaEntity.PublishedPropFromDay,
                                        Month = mangaEntity.PublishedPropFromMonth,
                                        Year = mangaEntity.PublishedPropFromYear
                                    },
                                    To = new DateParts
                                    {
                                        Day = mangaEntity.PublishedPropToDay,
                                        Month = mangaEntity.PublishedPropToMonth,
                                        Year = mangaEntity.PublishedPropToYear
                                    }
                                }
                            },
                            Score = (double?)mangaEntity.Score,
                            Scored = (double?)mangaEntity.Scored,
                            ScoredBy = mangaEntity.ScoredBy,
                            Rank = mangaEntity.Rank,
                            Popularity = mangaEntity.Popularity,
                            Members = mangaEntity.Members,
                            Favorites = mangaEntity.Favorites,
                            Synopsis = mangaEntity.Synopsis,
                            Background = mangaEntity.Background,
                            Authors = mangaEntity.Authors?.Split(',').Select(a => new Person { Name = a.Trim() }).ToList(),
                            Serializations = mangaEntity.Serializations?.Split(',').Select(s => new SerializationInfo { Name = s.Trim() }).ToList(),
                            Genres = mangaEntity.Genres?.Split(',').Select(g => new GenreType { Name = g.Trim() }).ToList(),
                            ExplicitGenres = mangaEntity.ExplicitGenres?.Split(',').Select(g => new GenreType { Name = g.Trim() }).ToList(),
                            Themes = mangaEntity.Themes?.Split(',').Select(t => new GenreType { Name = t.Trim() }).ToList(),
                            Demographics = mangaEntity.Demographics?.Split(',').Select(d => new GenreType { Name = d.Trim() }).ToList(),
                            userChapters = favorite.chapters,
                            userVolumes = favorite.volumes,
                            userRating = favorite.userRating
                        };

                        favoriteMangaModels.Add(mangaViewModel);
                    }
                }
            }
        }
        
        var model = new HomeModel();
        model.TopAnime = topAnime;
        model.TopManga = topManga;
        model.TopAiringAnime = airingAnime;
        model.UserManga = favoriteMangaModels;
        
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}