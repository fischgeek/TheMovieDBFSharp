namespace TheMovieDBFSharp

open FSharp.Data
open System
open System.IO
open System.Net.Http
open System.Collections.Generic

[<AutoOpen>]

module Types = 
    type MovieDetails = JsonProvider<"https://raw.githubusercontent.com/fischgeek/FSharpDataProviderSampleFiles/master/json/themoviedb/moviedetails.json", SampleIsList = true, RootName = "MovieDetails">

    type Genres = 
        | Action = 28
        | Adventure = 12
        | Animation = 16
        | Comedy = 35
        | Crime = 80
        | Documentary = 99
        | Drama = 18
        | Family = 10751
        | Fantasy = 14
        | History = 36
        | Horror = 27
        | Music = 10402
        | Mystery = 9648
        | Romance = 10749
        | ScienceFiction = 878
        | TVMovie = 10770
        | Thriller = 53
        | War = 10752
        | Western = 37
    
    let IdGenreMap =  
        [
            28, "Action"
            12, "Adventure"
            16, "Animation"
            35, "Comedy"
            80, "Crime"
            99, "Documentary"
            18, "Drama"
            10751, "Family"
            14, "Fantasy"
            36, "History"
            27, "Horror"
            10402, "Music"
            9648, "Mystery"
            10749, "Romance"
            878, "SciFi"
            10770, "TVMovie"
            53, "Thriller"
            10752, "War"
            37, "Western"
        ]
        |> Map.ofList
    
    type MatchResult = 
        {
            poster_path: string
            adult: bool
            overview: string
            release_date: string
            genre_ids: int[]
            id: int
            original_title: string
            original_language: string
            title: string
            backdrop_path: string
            popularity: float
            vote_count: int
            vote_average: float
        }
        static member GetPosterFullUrl x size = $"https://image.tmdb.org/t/p/{size}{x.poster_path}"
        static member GetBackdropFullUrl x = $"https://image.tmdb.org/t/p/original{x.backdrop_path}"
        static member GetPosterBase64 x =
            let fullPath = MatchResult.GetPosterFullUrl x "w500"
            let client = new HttpClient()
            let res = client.GetByteArrayAsync fullPath
            $"data:image/jpg;{Convert.ToBase64String res.Result}"
            
    type MovieResult = 
        {
            page: int
            results: MatchResult list
            total_results: int
            total_pages: int
        }