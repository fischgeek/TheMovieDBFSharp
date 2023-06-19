namespace TheMovieDBFSharp

module Main =
    let apiKey = ""
    let baseMovieUrl = "https://api.themoviedb.org/3"
    let posterBaseUrl = "https://image.tmdb.org/t/p/original"
    let movieAuth = $@"api_key={apiKey}&language=en-US&page=1&include_adult=false"

    let GetMovieTitle movieName = ()
        //let movieResult = QueryMovieByTitle movieName
        //if movieResult.total_results == 0 then
        //    movieName
        //else
        //    movieResult.results[0].title
