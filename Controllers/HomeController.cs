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
        List<AnimeModel> favoriteAnimeModels = new();

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
            
            List<UserFavorite> animeFav = _databaseMethods.GetFavoriteAnime(username);
            
            if (animeFav != null)
            {
                foreach (var animeFavorite in animeFav)
                {
                    var animeEntity = _databaseMethods.GetAnime(animeFavorite.malID);

                    if (animeEntity != null)
                    {
                        var animeViewModel = new AnimeModel
                        {
                            MalId = animeEntity.MalId,
                            Url = $"https://myanimelist.net/anime/{animeEntity.MalId}",
                            Approved = true,
                            Title = animeEntity.Title,
                            TitleEnglish = animeEntity.TitleEnglish,
                            TitleJapanese = animeEntity.TitleJapanese,
                            Titles = new List<TitleInfo>
                            {
                                new TitleInfo { Type = "Default", Title = animeEntity.Title }
                            },
                            Type = animeEntity.Type,
                            Source = animeEntity.Source,
                            Episodes = animeEntity.Episodes,
                            Status = animeEntity.Status,
                            Airing = animeEntity.Airing,
                            Duration = animeEntity.Duration,
                            Rating = animeEntity.Rating,
                            Score = (double?)animeEntity.Score,
                            ScoredBy = animeEntity.ScoredBy,
                            Rank = animeEntity.Rank,
                            Popularity = animeEntity.Popularity,
                            Members = animeEntity.Members,
                            Favorites = animeEntity.Favorites,
                            Synopsis = animeEntity.Synopsis,
                            Background = animeEntity.Background,
                            Season = animeEntity.Season,
                            Year = animeEntity.Year,
                            Aired = new AiredData
                            {
                                From = animeEntity.AiredFrom,
                                To = animeEntity.AiredTo,
                                DateString = animeEntity.AiredString
                            },
                            Broadcast = new BroadcastData
                            {
                                String = animeEntity.BroadcastString
                            },
                            Trailer = new TrailerData
                            {
                                Url = animeEntity.TrailerUrl,
                                EmbedUrl = animeEntity.TrailerEmbedUrl
                            },
                            Images = new ImageData
                            {
                                Jpg = new JpgData
                                {
                                    ImageUrl = animeEntity.ImageUrl
                                },
                                Webp = new JpgData
                                {
                                    ImageUrl = animeEntity.ImageUrl
                                }
                            },
                            Producers = animeEntity.Producer?.Split(',').Select(p => new Producer
                            {
                                Name = p.Trim()
                            }).ToList() ?? new List<Producer>(),

                            Studios = animeEntity.Studio?.Split(',').Select(s => new Studio
                            {
                                Name = s.Trim()
                            }).ToList() ?? new List<Studio>(),

                            Licensors = animeEntity.Licensors?.Split(',').Select(l => new Licensor
                            {
                                Name = l.Trim()
                            }).ToList() ?? new List<Licensor>(),

                            Genres = animeEntity.Genres?.Split(',').Select(g => new Genre
                            {
                                Name = g.Trim()
                            }).ToList() ?? new List<Genre>(),

                            ExplicitGenres = animeEntity.ExplicitGenres?.Split(',').Select(e => new Genre
                            {
                                Name = e.Trim()
                            }).ToList() ?? new List<Genre>(),

                            Themes = animeEntity.Themes?.Split(',').Select(t => new Genre
                            {
                                Name = t.Trim()
                            }).ToList() ?? new List<Genre>(),

                            Demographics = animeEntity.Demographics?.Split(',').Select(d => new Demographic
                            {
                                Name = d.Trim()
                            }).ToList() ?? new List<Demographic>(),

                            userRating = animeFavorite.userRating,
                            userEpisodes = animeFavorite.episodes
                        };

                        // Optional: populate SelectLists here if needed

                        favoriteAnimeModels.Add(animeViewModel);
                    }
                }
            }
        }
        
        var model = new HomeModel();
        model.TopAnime = topAnime;
        model.TopManga = topManga;
        model.TopAiringAnime = airingAnime;
        model.UserManga = favoriteMangaModels;
        model.UserAnime = favoriteAnimeModels;
        
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