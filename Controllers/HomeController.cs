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
        
        var model = new HomeModel();
        model.TopAnime = topAnime;
        model.TopManga = topManga;
        model.TopAiringAnime = airingAnime;
        
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