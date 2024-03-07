using NLog;

Console.WriteLine("Movie Application");
Console.WriteLine("-----------------");
string path = Directory.GetCurrentDirectory() + "\\nlog.config";
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

// logger.Trace("Sample Trace message");
// logger.Debug("Sample Debug message");
// logger.Info("Sample Information message");
// logger.Warn("Sample Warning message");
// logger.Error("Sample Error message");
// logger.Fatal("Sample Fatal message");

string file = "movies.csv";
List<Movie> movies = new List<Movie>();
while (true) {
    try {
        Console.WriteLine("1. View all movies");
        Console.WriteLine("2. Add a movie");
        Console.WriteLine("3. Check for duplicate movies");
        Console.WriteLine("4: Exit");
        Console.WriteLine("-----------------");
        Console.WriteLine("Enter a number: ");

        var input = Console.ReadLine();
        if (input == "1"){
            movies.Clear();
            using (StreamReader sr = new StreamReader(file)) {
                string? line;
                while ((line = sr.ReadLine()) != null){
                    var values = line.Split(",");
                    var movie = new Movie(values[0], values[1], values[2]);
                    movies.Add(movie);
                }
            }
            foreach (var movie in movies) {
                Console.WriteLine($"MovieId: {movie.MovieId}, Title: {movie.Title}, Genres: {movie.Genres}");
            }
        }
        if (input == "2") {
            Console.WriteLine("Enter a movie id: ");
            var movieId = Console.ReadLine();
            foreach (var existingMovie in movies) {
                if (existingMovie.MovieId == movieId) {
                    Console.WriteLine("A movie with this ID already exists. Please try again.");
                    continue;
                }
            }
            Console.WriteLine("Enter a movie title: ");
            var title = Console.ReadLine();
            Console.WriteLine("Enter genres: ");
            var genres = Console.ReadLine();
            if (movieId == null || title == null || genres == null) {
                throw new ArgumentNullException(nameof(movieId) + ", " + nameof(title) + ", and " + nameof(genres), "MovieId, Title, and Genres cannot be null. Please try again.");
            }
            var movie = new Movie(movieId, title, genres);
            movies.Add(movie);
            using (StreamWriter sw = new StreamWriter(file, true)) {
                sw.WriteLine($"{movieId},{title},{genres}");
            }
        } 
        if (input == "3") {
            Console.WriteLine("Enter a movie title: ");
            var title = Console.ReadLine();
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title), "Title cannot be null.");
            }
            Movie? duplicate = null;
            foreach (var movie in movies)
            {
                if (movie.Title == title)
                {
                    duplicate = movie;
                    break;
                }
            }
            if (duplicate != null) {
                Console.WriteLine($"Duplicate movie found: {duplicate.Title} with ID: {duplicate.MovieId} and Genres: {duplicate.Genres}");
            } else {
                Console.WriteLine("No duplicate movie");
            }
        }
        if (input == "4") {
            break;
        }
    } catch (Exception ex) {
        logger.Error(ex, "An error occurred while processing your request. Please try again.");
    }
}

    