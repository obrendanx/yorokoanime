namespace yorokoanime.ViewModels;

public class HomeModel
{
    public List<AnimeModel> TopAnime { get; set; }
    public List<MangaModel> TopManga { get; set; }
    public List<MangaModel> UserManga { get; set; }
    public List<AnimeModel> TopAiringAnime { get; set; }
}