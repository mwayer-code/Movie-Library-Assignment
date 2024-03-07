public class Movie {
    public string MovieId { get; set; }
    public string Title { get; set; }
    public string Genres { get; set; }
    public Movie(string movieId, string title, string genres) {
        this.MovieId = movieId;
        this.Title = title;
        this.Genres = genres;
    }
    public override string ToString() {
        return $"MovieId: {MovieId}| Title: {Title}| Genres: {Genres}";
    }
}

