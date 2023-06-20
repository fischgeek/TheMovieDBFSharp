namespace TheMovieDBFSharp

open System
open System.IO
open System.Text.RegularExpressions
open Requesty.Requesty

module Main =
    let apiKey = File.ReadAllText @"apikey.txt"
    let baseMovieUrl = "https://api.themoviedb.org/3"
    let posterBaseUrl = "https://image.tmdb.org/t/p/original"
    let movieAuth = $@"api_key={apiKey}&language=en-US&page=1&include_adult=false"

    let GetMovieQueryURL movieTitle =
        let xmatch = Regex.Match(movieTitle, @"\(\d{4}\)");
        let xTitle,xYear =
            if xmatch.Success then
                let y = xmatch.Value.Replace("(", "");
                let year = y.Replace(")", "");
                printfn $"year is {year}"
                movieTitle.Replace(xmatch.Value, ""),year
            else movieTitle,""
        $@"{baseMovieUrl}/search/movie?query={xTitle}&{movieAuth}&year={xYear}"

    let GetMovie movieName = 
        let movieResult = 
            HRB.Create()
            |> HRB.Url (GetMovieQueryURL movieName)
            |> HRB.StockFns.RunWithBasicJsonResponse<MovieResult>
            |> function
            | Ok (mr: MovieResult) -> 
                if mr.results.Length > 0 then
                    mr.results.Head |> Some
                else None
            | Error x -> failwith (x.ToString())
        movieResult

    let private parseStringtoMD str : MovieDetails.MovieDetail = MovieDetails.Parse str

    let GetMovieById (id: int) = 
        let movieResult = 
            HRB.Create()
            |> HRB.Url $@"{baseMovieUrl}/movie/{id}?{movieAuth}&append_to_response=credits"
            |> HRB.StockFns.RunWithTextResponse
            |> function
            | Ok (md: string) -> md |> parseStringtoMD
            | Error x -> failwith (x.ToString())
        movieResult